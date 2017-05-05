using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Unity Refs")]
    public Rigidbody2D rb;

    [Header("Movement")]
    
    public float moveSpeed;
    public float speedBoost;
    private float baseSpeed;

    // Use this for initialization
    void Start () {

    }

    private void Awake()
    {
        baseSpeed = moveSpeed;
    }

    void Update()
    {
        if (Input.GetKeyUp("w") || Input.GetKeyUp("s"))
            rb.velocity = new Vector2(rb.velocity.x, 0);

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
            rb.velocity = new Vector2(0, rb.velocity.y);

        if (Input.GetKeyUp(KeyCode.LeftShift))
            moveSpeed = baseSpeed;

        if (Input.GetKeyDown(KeyCode.LeftShift))
            moveSpeed *= speedBoost;
    }


	// Update is called once per frame
	void FixedUpdate () {

        if (Input.GetKey("w"))
            rb.AddForce(Vector2.up * moveSpeed, ForceMode2D.Impulse);

        if (Input.GetKey("a"))
            rb.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);

        if (Input.GetKey("s"))
            rb.AddForce(Vector2.down * moveSpeed, ForceMode2D.Impulse);

        if (Input.GetKey("d"))
            rb.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);

        if (rb.velocity.sqrMagnitude > moveSpeed)
        {
            //smoothness of the slowdown is controlled by the float value, 
            //0.5f is less smooth, 0.9999f is almost non-existent
            rb.velocity *= 0.5f;
        }
        Debug.Log(rb.velocity);
    }
}
