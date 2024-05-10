using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 ControllerVelocity { get; private set; }

    private int rotateSpeed;
    private CharacterController controller;
    private Vector3 movementDirection;
    private Vector3 lastPosition;
    private Vector3 distanceTraveled;
    private Rigidbody rb;

    public bool useGamepadInput;
    public bool useTouchControls;
    public bool useWASD;
    bool isGrounded = true;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        lastPosition = this.transform.position;
    }
    void CheckGound(){
        if(this.transform.position.y > 1){
            isGrounded = false;
        }
        else{
            isGrounded = true;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {   CheckGound(); 
        if (isGrounded){
        
        distanceTraveled = this.transform.position - lastPosition;
        lastPosition = this.transform.position;
        ControllerVelocity = distanceTraveled / Time.fixedDeltaTime;

        if (useGamepadInput)
        {
            movementDirection.z = -Input.GetAxis("Horizontal")  ;
            movementDirection.x = Input.GetAxis("Vertical")  ;
        }

        if (useWASD)
        {
            if (Input.GetButton("a"))
            {
                movementDirection.z = 1  ;
            }
            else if (Input.GetButton("d"))
            {
                movementDirection.z = -1  ;
            }
            else if(Input.GetButton("w"))
            {
                movementDirection.x = 1  ;
            }
            else if(Input.GetButton("s"))
            {
                movementDirection.x = -1  ;
            }
            else if (Input.GetButton("d") && Input.GetButton("w"))
            {
                movementDirection.z = -0.5f  ;
                movementDirection.z = 0.5f  ;
            }
            else if (Input.GetButton("w") && Input.GetButton("a"))
            {
                movementDirection.x = 0.5f  ;
                movementDirection.z = 0.5f  ;
            }
            else if (Input.GetButton("s") && Input.GetButton("d"))
            {
                movementDirection.x = -0.5f  ;
                movementDirection.z = -0.5f  ;
            }
            else if (Input.GetButton("s") && Input.GetButton("a"))
            {
                movementDirection.x = -0.5f  ;
                movementDirection.z = 0.5f  ;
            }
            //no keys are pressed
            else
            {
                movementDirection = Vector3.zero;
            }
        }


        //if movementDirection is not at zero, character controller is moving
        if (movementDirection != Vector3.zero)
        {
            //move character controller in axis direction
            controller.Move(movementDirection);
            //Vector3 movementVelocity = movementDirection * 1f;
            //rb.velocity = new Vector3(movementVelocity.x, rb.velocity.y, movementVelocity.z);
            //rotate character controller over time
            Quaternion direction = Quaternion.LookRotation(movementDirection);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, direction, rotateSpeed * Time.deltaTime);
            } 
        }
    }
}
