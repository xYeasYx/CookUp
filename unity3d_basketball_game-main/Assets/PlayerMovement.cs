using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed of the character
    private Vector3 movementDirection;
    private Rigidbody rb;

    public Transform target; // The target to jump towards
    public float jumpForce = 10f; // The force applied to jump
    public float jumpHeight = 2f; // The height the character jumps

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input from player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
            if (Input.GetKeyDown(KeyCode.Q)){
            JumpTowardsTarget();}

        // Calculate movement direction
        if (Input.GetButton("a"))
            {
                movementDirection.z = 1 ;
            }
            else if (Input.GetButton("d"))
            {
                movementDirection.z = -1 ;
            }
            else if(Input.GetButton("w"))
            {
                movementDirection.x = 1 ;
            }
            else if(Input.GetButton("s"))
            {
                movementDirection.x = -1 ;
            }
            else if (Input.GetButton("d") && Input.GetButton("w"))
            {
                movementDirection.z = -0.5f ;
                movementDirection.z = 0.5f ;
            }
            else if (Input.GetButton("w") && Input.GetButton("a"))
            {
                movementDirection.x = 0.5f ;
                movementDirection.z = 0.5f ;
            }
            else if (Input.GetButton("s") && Input.GetButton("d"))
            {
                movementDirection.x = -0.5f ;
                movementDirection.z = -0.5f ;
            }
            else if (Input.GetButton("s") && Input.GetButton("a"))
            {
                movementDirection.x = -0.5f ;
                movementDirection.z = 0.5f ;
            }
            //no keys are pressed
            else
            {
                movementDirection = Vector3.zero;
            }
        //movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Move the character
        MoveCharacter(movementDirection);
    }
     void JumpTowardsTarget()
    {   //Debug.Log(transform.position);
        if (target.position == null)
        {
            Debug.LogWarning("Target not assigned!");
            return;
        }

        // Calculate the direction to the target
        Vector3 direction = target.position - transform.position;
        direction.y = 0; // Ignore the y-component for horizontal movement

        // Calculate the jump direction
        Vector3 jumpDirection = Vector3.up * Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics.gravity.y)) + direction; //.normalized * jumpForce
        
        // Apply the jump force
        rb.velocity = jumpDirection;
        rb.AddForce(jumpDirection, ForceMode.Impulse);
        Debug.Log(rb.velocity);

    }
    void MoveCharacter(Vector3 direction)
    {
        // Calculate movement velocity
        Vector3 movementVelocity = direction * moveSpeed;

        // Apply movement velocity to the rigidbody
        rb.velocity = new Vector3(movementVelocity.x, rb.velocity.y, movementVelocity.z);
    }
}

