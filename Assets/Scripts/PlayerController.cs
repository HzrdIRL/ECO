using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Unity Refs")]
    public Rigidbody2D rb;
    public LayerMask interactableObjects;
    public static float bioMatter;

    [Header("Movement")]
    
    public float moveSpeed;
    public float speedBoost;
    private float baseSpeed;
    private Vector2 move;
    private Vector2 faceDirection;
    public bool interacting;
    public bool watering;
    public static bool hasSpringCore;

    [Header("Testing")]
    private float interactDistance;
    

    // Use this for initialization
    void Start()
    {
        faceDirection = Vector2.down;
        interacting = false;
        bioMatter = 0f;
        interactDistance = 1f;
        watering = false;
        hasSpringCore = false;
    }

    private void Awake()
    {
        baseSpeed = moveSpeed;
    }

    void Update()
    {
        //Get player interaction key press
        if (Input.GetKey(KeyCode.E))
        {
            interacting = true;
        }

        //Get player watering key press
        if (Input.GetKey(KeyCode.Space))
        {
            watering = true;
        }

        //Disable speedBoost modifier
        if (Input.GetKeyUp(KeyCode.LeftShift))
            moveSpeed = baseSpeed;

        //Enable speedBoost modifier
        if (Input.GetKeyDown(KeyCode.LeftShift))
            moveSpeed *= speedBoost;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        
        //Apply player movement
        moveCharacter(rb);

        //Update faceDirection;
        updateBearing();

        /*
         * Interact with object in front of player
         */
        if (interacting)
        {
            interact();
        }

        if (watering)
        {
            water();
        }

        

    }

    /* 
     * Update the players facing direction, favour up/down
     */
    void updateBearing()
    {
        if (Input.GetKey(KeyCode.D))
        {
            faceDirection = Vector2.right;
        }

        if (Input.GetKey(KeyCode.A))
        {
            faceDirection = Vector2.left;
        }

        if (Input.GetKey(KeyCode.W))
        {
            faceDirection = Vector2.up;
        }

        if (Input.GetKey(KeyCode.S))
        {
            faceDirection = Vector2.down;
        }
    }

    /*Get the input axis from the controller
     * and apply it to a Vector 2, 
     * normalise to handle diagonal speedup
     * and set the players velocity.
     */
    void moveCharacter(Rigidbody2D rb)
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        move = move.normalized * Time.deltaTime * moveSpeed;
        rb.velocity = move;
    }

    /*
     * Invoke the interact() function from an interactable object in front of the player
     * E.G. PowerCore - pickup(), Plant - harvest(), WaterReclaimer - refillWaterTank()
     */
    void interact()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, faceDirection, interactDistance, interactableObjects);
        hit.collider.GetComponentInChildren<Interactable>().interact();
        interacting = false;
    }

    void water()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, faceDirection, interactDistance, interactableObjects);
        hit.collider.GetComponentInChildren<Plant>().watered = true;
        watering = false;
    }
}
