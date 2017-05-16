using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Unity Refs")]
    public Rigidbody2D rb;
    public LayerMask interactableObjects;
    public int bioMatter;

    [Header("Movement")]

    public float moveSpeed;
    public float speedBoost;
    private float baseSpeed;
    private Vector2 move;
    private Vector2 faceDirection;
    public bool interacting;
    public bool watering;
    public bool harvesting;
    public static bool[] cores;
    public bool usingTool;
    private Animator animator;
    public int equippedTool;
    public GameObject plantBlueprint;

    private enum Tools { Hydrater, Harvester, Cultivator };

    [Header("Testing")]
    private float interactDistance;


    // Use this for initialization
    void Start()
    {
        
        faceDirection = Vector2.down;
        interacting = false;
        bioMatter = 5;
        interactDistance = 1f;
        watering = false;
        cores = new bool[4];
        animator = this.GetComponent<Animator>();
        animator.speed = 0.2f;
        equippedTool = 0;
        usingTool = false;
    }

    private void Awake()
    {
        baseSpeed = moveSpeed;
    }

    void Update()
    {
        //Get player interaction key press
        if (Input.GetKeyDown(KeyCode.E))
        {
            interacting = true;
        }

        //Get player watering key press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            usingTool = true;
        }

        //Disable speedBoost modifier
        if (Input.GetKeyUp(KeyCode.LeftShift))
            moveSpeed = baseSpeed;

        //Enable speedBoost modifier
        if (Input.GetKeyDown(KeyCode.LeftShift))
            moveSpeed *= speedBoost;

        updateBearing();
        
        getEquippedTool();
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        //Interact with object in front of player
        if (interacting)
        {
            interact();
        }

        // Use the equipped tool
        if (usingTool)
        {
            usingTool = false;
            if(equippedTool == (int)Tools.Hydrater)
                water();

            if (equippedTool == (int)Tools.Harvester)
                harvest();

            if (equippedTool == (int)Tools.Cultivator)
                cultivate();
        }
         
        //Update the players facing direction, for animation purposes
        updateBearing();

        //Apply player movement
        moveCharacter();

    }

    public void increaseBiomatter(int value)
    {
        bioMatter += value;
    }

    /*
     * Switch tools: Watering, Harvesting, Planting
     */
     void getEquippedTool()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            equippedTool = (int)Tools.Hydrater;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            equippedTool = (int)Tools.Harvester;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            equippedTool = (int)Tools.Cultivator;
        }
    }

    /*
     * Update the players facing direction, favour up/down
     */
    void updateBearing()
    {
        if (Input.GetKey(KeyCode.D) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)))
        {
            faceDirection = Vector2.right;
            if(Input.GetKeyDown(KeyCode.D))
                animator.CrossFade("Astro_Walk_East", 0.0f);
            animator.SetInteger("Direction", 3);
        }

        if (Input.GetKey(KeyCode.A) && !(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W)))
        {
            faceDirection = Vector2.left;
            if (Input.GetKeyDown(KeyCode.A))
                animator.CrossFade("Astro_Walk_West", 0.0f);
            animator.SetInteger("Direction", 1);
        }

        if (Input.GetKey(KeyCode.W) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S)))
        {
            faceDirection = Vector2.up;
            if (Input.GetKeyDown(KeyCode.W))
                animator.CrossFade("Astro_Walk_North", 0.0f);
            animator.SetInteger("Direction", 2);
        }

        if (Input.GetKey(KeyCode.S) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W)))
        {
            faceDirection = Vector2.down;
            if (Input.GetKeyDown(KeyCode.S))
                animator.CrossFade("Astro_Walk_South", 0.0f);
            animator.SetInteger("Direction", 0);
        }
    }

    /*Get the input axis from the controller
     * and apply it to a Vector 2,
     * normalise to handle diagonal speedup
     * and set the players velocity.
     */
    void moveCharacter()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        move = move.normalized * Time.deltaTime * moveSpeed;

        if (move.x != 0 || move.y != 0)
        {
            animator.SetBool("IsWalking", true);
            animator.enabled = true;
        }
        else
        {
            animator.SetBool("IsWalking", false);
            animator.enabled = false;
        }

;        rb.velocity = move;
    }

    /*
     * Invoke the interact() function from an interactable object in front of the player
     * E.G. PowerCore - pickup(), Bed - sleep(), WaterReclaimer - refillWaterTank()
     */
    void interact()
    {
        interacting = false;
        Interactable interactableObject = null;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, faceDirection, interactDistance, interactableObjects);
        if((interactableObject = hit.collider.GetComponentInChildren<Interactable>()) != null)
            interactableObject.interact();
    }

    /*
     * Invoke the water() function from a Soil object in front of the player
     * A plant will determine if it is watered by ageing up based on wether or not the soil is watered
     */
    void water()
    {
        Soil soil = null;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, faceDirection, interactDistance, interactableObjects);
        if ((soil = hit.collider.GetComponentInChildren<Soil>()) != null)
            soil.water();
    }

    /*
     * Invoke the harvest() function from a harvestable object in front of the player
     */
    void harvest()
    {
        Harvestable harvestableObject = null;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, faceDirection, interactDistance, interactableObjects);
        if ((harvestableObject = hit.collider.GetComponentInChildren<Plant>()) != null)
        {
            harvestableObject.harvest();
            bioMatter += ((Plant)harvestableObject).value;
        }
            
    }

    void cultivate()
    {
        Soil soilObject = null;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, faceDirection, interactDistance, interactableObjects);
        if (((soilObject = hit.collider.GetComponentInChildren<Soil>()) != null) && bioMatter >= plantBlueprint.GetComponent<Plant>().cost)
        {
            soilObject.cultivate(plantBlueprint);
            bioMatter -= plantBlueprint.GetComponent<Plant>().cost;
        }
    }
}
