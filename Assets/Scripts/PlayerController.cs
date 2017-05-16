using UnityEngine;
using System;

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
    public bool harvesting;
    public static bool hasSpringCore;
    private Animator animator;

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
        animator = this.GetComponent<Animator>();
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

        updateBearing();


    }


    // Update is called once per frame
    void FixedUpdate()
    {
        
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

        //Apply player movement
        moveCharacter();

    }

    /* 
     * Update the players facing direction, favour up/down
     */
    void updateBearing()
    {
        if (rb.velocity.x > 0 && Math.Abs(rb.velocity.x) > Math.Abs(rb.velocity.y))
        {
            faceDirection = Vector2.right;
            animator.SetInteger("Direction", 3);
        }

        if (rb.velocity.x < 0 && Math.Abs(rb.velocity.x) > Math.Abs(rb.velocity.y))
        {
            faceDirection = Vector2.left;
            animator.SetInteger("Direction", 1);
        }

        if (rb.velocity.y > 0 && Math.Abs(rb.velocity.y) > Math.Abs(rb.velocity.x))
        {
            faceDirection = Vector2.up;
            animator.SetInteger("Direction", 2);
        }

        if (rb.velocity.y < 0 && Math.Abs(rb.velocity.y) > Math.Abs(rb.velocity.x))
        {
            faceDirection = Vector2.down;
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
        watering = false;
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
        harvesting = false;
        Harvestable harvestableObject = null;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, faceDirection, interactDistance, interactableObjects);
        if ((harvestableObject = hit.collider.GetComponentInChildren<Plant>()) != null)
            harvestableObject.harvest();
    }
}
