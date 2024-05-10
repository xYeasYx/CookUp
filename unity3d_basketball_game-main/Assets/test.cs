using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private int rotateSpeed;
    public Rigidbody rb;
    private Vector3 movementDirection;
    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        CheckGround();

        // Reset movement direction
        movementDirection = Vector3.zero;

        if (isGrounded)
        {
            if (Input.GetButton("a"))
            {
                movementDirection += transform.right;
            }
            else if (Input.GetButton("d"))
            {
                movementDirection -= transform.right;
            }
            if (Input.GetButton("w"))
            {
                movementDirection += transform.forward;
            }
            else if (Input.GetButton("s"))
            {
                movementDirection -= transform.forward;
            }

            // Normalize movement direction to prevent faster diagonal movement
            if (movementDirection != Vector3.zero)
            {
                movementDirection.Normalize();
            }

            // Apply movement direction to the Rigidbody's velocity
            rb.velocity  = movementDirection;
        }
    }

    void CheckGround()
    {
        // Perform a raycast to check if the player is grounded
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f);
    }
}
