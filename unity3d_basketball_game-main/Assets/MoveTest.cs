using UnityEngine;

public class MoveTest : MonoBehaviour
{
    public float moveSpeed = 2.0f; // Speed of movement
    public float rotateSpeed = 180.0f; // Speed of rotation
    bool forwardPressed;

    void Update()
    {
        // Rotate the character based on mouse input
        forwardPressed = Input.GetKey("w");
        RotateCharacter();
        

        // Move the character forward when pressing the W key
        if (Input.GetKey(KeyCode.W))
        {
            MoveForward();
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey("left shift"))
        {
            MoveForwardFast();
        }
        if (Input.GetKey(KeyCode.A))    //move left
        {
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.D))    // move right
        {
            MoveRight();
        }
        if(Input.GetKey(KeyCode.S)){
            MoveBack();
        }
    }

    void RotateCharacter()
    {
        if (Input.GetKey(KeyCode.D) && forwardPressed )
        {
            //transform.position += Vector3.right * speed * Time.deltaTime;
            //RotateCharacter(Vector3.right);
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) && forwardPressed)
        {
            //transform.position += Vector3.left * speed * Time.deltaTime;
            transform.Rotate(Vector3.up, -(rotateSpeed * Time.deltaTime));
        }
    }

    void MoveForward()
    {
        // Calculate forward direction based on the character's rotation
        Vector3 forwardDirection = transform.forward;

        // Move the character forward in the direction it's facing
        transform.position += forwardDirection * moveSpeed * Time.deltaTime;
    }
    void MoveForwardFast()
    {
        // Calculate forward direction based on the character's rotation
        Vector3 forwardDirection = transform.forward;

        // Move the character forward in the direction it's facing
        transform.position += forwardDirection * moveSpeed*2  * Time.deltaTime;
    }
        void MoveRight()
    {
        // Calculate right direction based on the character's rotation
        Vector3 rightDirection = transform.right;

        // Move the character to the right
        transform.position += rightDirection * (moveSpeed/4) * Time.deltaTime;
    }
    void MoveLeft()
    {
        // Calculate left direction based on the character's rotation
        Vector3 leftDirection = -transform.right; // Using negative right for left direction

        // Move the character to the left
        transform.position += leftDirection * (moveSpeed/4) * Time.deltaTime;
    }
    void MoveBack()
    {
        // Calculate backward direction based on the character's rotation
        Vector3 backwardDirection = -transform.forward; // Using negative forward for backward direction

        // Move the character backward
        transform.position += backwardDirection * (moveSpeed/6) * Time.deltaTime;
    }
}




       /* if (Input.GetButton("a"))
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


            if (movementDirection != Vector3.zero)
        {
            //move character controller in axis direction
            //controller.Move(movementDirection);
            //Vector3 movementVelocity = movementDirection * 1f;
            //rb.velocity = new Vector3(movementVelocity.x, rb.velocity.y, movementVelocity.z);
            //rotate character controller over time
            Quaternion direction = Quaternion.LookRotation(movementDirection);
            
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, direction, rotateSpeed * Time.deltaTime);
            } */
        

