using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Unity Refs")]
    public Rigidbody2D rb;
    public LayerMask interactableObjects;
    public int bioMatter; //should be private
    public int waterLevel;
    public GameObject plantBlueprint;
    public static bool[] cores;
    

    [Header("Movement")]
    public float moveSpeed;
    public float speedBoost;
    private float baseSpeed;
    private Vector2 move;
    private Vector2 faceDirection;

    private bool interacting;
    private bool watering;
    private bool harvesting;
    
    private bool usingTool;
    private float toolTimer;
    public int equippedTool;
    private LineRenderer line;

    private Animator animator;

    public enum Tools { Hydrater, Harvester, Cultivator };

    [Header("Testing")]
    private float interactDistance;


    // Use this for initialization
    void Start()
    {
        line = gameObject.GetComponentInChildren<LineRenderer>();
        line.enabled = false;
        faceDirection = Vector2.down;
        interacting = false;
        bioMatter = 5;
        waterLevel = 100;
        interactDistance = 0.5f;
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

        switchTool();
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        //Interact with object in front of player
        if (interacting)
        {
            interact();
        }

        if((Time.time - toolTimer) >= 0.1f)
        {
            line.enabled = false;
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

    public int getWaterLevel()
    {
        return waterLevel;
    }

    public void setWaterLevel(int value)
    {
        waterLevel += value;
    }

    public void setBioMatterLevel(int value)
    {
        bioMatter += value;
    }

    public int getBioMatterLevel()
    {
        return bioMatter;
    }

    public string getEquippedTool()
    {
        return ((Tools)equippedTool).ToString();
    }

    /*
     * Switch tools: Watering, Harvesting, Planting
     */
    void switchTool()
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
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            animator.SetBool("IsWalking", true);
            animator.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
            faceDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        }

        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            animator.SetBool("IsWalking", true);
            animator.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
            animator.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
            faceDirection = new Vector2(0f, Input.GetAxisRaw("Vertical"));
        }

        animator.SetFloat("LastMoveX", faceDirection.x);
        animator.SetFloat("LastMoveY", faceDirection.y);
    }

    /*Get the input axis from the controller
     * and apply it to a Vector 2,
     * normalise to handle diagonal speedup
     * and set the players velocity.
     */
    void moveCharacter()
    {
        Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        move = move.normalized * Time.deltaTime * moveSpeed;

        if (move.x == 0 && move.y == 0)
        {
            animator.SetBool("IsWalking", false); 
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

    // Draw a line to represent the direction/distance of the equipped tool, when used.
    void visualiseTool(Color col)
    {
        line.enabled = true;
        line.SetPosition(1, faceDirection * interactDistance);
        line.startColor = col;
        line.endColor = col;
        toolTimer = Time.time;
    }

    /*
     * Invoke the water() function from a Soil object in front of the player
     * A plant will determine if it is watered by ageing up based on wether or not the soil is watered
     */
    void water()
    {
        Soil soil = null;
        RaycastHit2D[] hits = Physics2D.RaycastAll(this.transform.position, faceDirection, interactDistance, interactableObjects);

        foreach (RaycastHit2D hit in hits)
        {
            if ((soil = hit.collider.GetComponentInChildren<Soil>()) != null
                && !soil.watered
                && soil.activated
                && soil.plantedObject != null)
            {
                visualiseTool(Color.blue);
                soil.water();
                setWaterLevel(-5);
            }
        }
    }

    /*
     * Invoke the harvest() function from a harvestable object in front of the player
     */
    void harvest()
    {
        Harvestable harvestableObject = null;
        RaycastHit2D[] hits = Physics2D.RaycastAll(this.transform.position, faceDirection, interactDistance, interactableObjects);

        foreach (RaycastHit2D hit in hits)
        {
            if ((harvestableObject = hit.collider.GetComponentInChildren<Harvestable>()) != null)
            {
                if (harvestableObject.harvest())
                {
                    visualiseTool(Color.magenta);
                    setBioMatterLevel(((Plant)harvestableObject).value);
                }
            }
        }
            
    }

    void cultivate()
    {
        Soil soilObject = null;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, faceDirection, interactDistance, interactableObjects);
            //Physics2D.Raycast(transform.position, faceDirection, interactDistance, interactableObjects);
        foreach(RaycastHit2D hit in hits)
        {
            if (((soilObject = hit.collider.GetComponentInChildren<Soil>()) != null)
            && bioMatter >= plantBlueprint.GetComponent<Plant>().cost
            && soilObject.activated
            && soilObject.plantedObject == null)
            {
                visualiseTool(Color.green);
                soilObject.cultivate(plantBlueprint);
                setBioMatterLevel(-plantBlueprint.GetComponent<Plant>().cost); ;
            }
        }
        
    }
}
