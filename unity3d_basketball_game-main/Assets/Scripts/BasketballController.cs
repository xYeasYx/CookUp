using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballController : MonoBehaviour
{
    public float MoveSpeed = 10; 
    public Transform Ball;
    public Transform target; // The target to jump towards
    public Transform player;
    public Transform ArmR;
    public Transform ArmL;
    public Transform ShootingPosition;
    public Transform dribblePosL;
    public Transform dribblePosR;
    public bool BallInHands = true;
    private bool BallInL = true;
    private bool BallInR = false;
    private bool isshooting = false;


    public float dunkForce = 10f; // The force applied to jump
    public float dunkHeight = 2f; // The height the character jumps

    Vector3 VLeft ;
    Vector3 VRight ;

    public float jumpForce = 10f;
    public float crossForce;
    public Rigidbody rb;

    private bool canJump = true; // Flag to check if the player can jump
    public float jumpCooldown = 10f; // Cooldown time for jump in seconds
    public float jumpTimer = 0f; // Timer to track the cooldown

    void Start(){
        rb = GetComponent<Rigidbody>();
        
    }
     void FixedUpdate()
    {
        VLeft = new Vector3(0f, 0f,-crossForce);
        VRight = new Vector3(0f, 0f, crossForce);
        // Update the jump timer
        jumpTimer += Time.deltaTime;    // If jump timer exceeds cooldown time, allow jumping again
        if (jumpTimer >= jumpCooldown)
        {
            canJump = true;
            jumpTimer = 0f; // Reset the timer
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (BallInHands){   // offense

            //Ball.position = dribblePosL.position + Vector3.up * Mathf.Abs(Mathf.Sin(Time.time*5));
            if(BallInL){ // if ball is in left hand
                Ball.position = dribblePosL.position + Vector3.up * Mathf.Abs(Mathf.Sin(Time.time*5));
                ArmL.localEulerAngles = Vector3.right * Mathf.Abs(Mathf.Sin(Time.time*5)) * -90;    // dribble with left
                ArmR.localEulerAngles = Vector3.right * 0;}                                           // do nothing with right
            else{
                Ball.position = dribblePosR.position + Vector3.up * Mathf.Abs(Mathf.Sin(Time.time*5));
                ArmR.localEulerAngles = Vector3.right * Mathf.Abs(Mathf.Sin(Time.time*5)) * -90;  
                ArmL.localEulerAngles = Vector3.right * 0;}
                //ArmL.localEulerAngles = Vector3.right * 0;


            if(Input.GetKey(KeyCode.Space)){
                
                Ball.position = ShootingPosition.position;
                ArmR.localEulerAngles = Vector3.right * 220;
                ArmL.localEulerAngles = Vector3.right * 220;
            }
             
            if (Input.GetKeyDown(KeyCode.C)){
                Debug.Log("crossover");
                BallInL = BallInR;
                BallInR = !BallInR;

                // Calculate direction based on BallInL and BallInR
                Vector3 direction = BallInL ? VRight : VLeft ;
                Debug.Log(direction);

                // Apply force in the calculated direction
                player.position = player.position + direction;
            }

            /*if(Input.GetKeyDown(KeyCode.C) ){
                Debug.Log("crossover");
                BallInL = BallInR;
                BallInR = !(BallInR);
            }*/
            //dribble
        
            //rb.velocity = new Vector2(rb.velocity.z, crossForce);
               // crossForce = crossForce * -1;
                
        
        }
        else{       //defense
            if (Input.GetKey(KeyCode.B))
    {
        ArmL.localEulerAngles = Vector3.right * -70;
    }
    else if (Input.GetKey(KeyCode.V))
    {
        ArmR.localEulerAngles = Vector3.right * -70;
    }
    else if (Input.GetKey(KeyCode.N) && canJump)
    {
        Jump();
    }
    else if (Input.GetKey(KeyCode.M))
    {
        ArmR.localEulerAngles = Vector3.right * 180;
        ArmL.localEulerAngles = Vector3.right * 180;
    }
    else
    {
        // Reset arm angles if no key is pressed
        ArmR.localEulerAngles = Vector3.right * 0;
        ArmL.localEulerAngles = Vector3.right * 0;
    }
            
            }
            
            
        }
    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.y, jumpForce);
        canJump = false; // Prevent jumping until cooldown is over
    }
    public void Dunk()
    {
        rb.velocity = new Vector2(rb.velocity.y, jumpForce);
        canJump = false; // Prevent jumping until cooldown is over
    }
}

/*if(Input.GetKey(KeyCode.B) ){   //left arm reach
                ArmL.localEulerAngles = Vector3.right * -70;
            }
            else if(Input.GetKey(KeyCode.V) ){   //Right arm reach
                ArmR.localEulerAngles = Vector3.right * -70;
            }else{
            ArmR.localEulerAngles = Vector3.right * 0;
            ArmL.localEulerAngles = Vector3.right * 0;}

            if(Input.GetKey(KeyCode.N) &&  canJump){    //block
                Jump();}
            
            if(Input.GetKey(KeyCode.M)){
                ArmR.localEulerAngles = Vector3.right * 180;
                ArmL.localEulerAngles = Vector3.right * 180;
            }
            else{
            ArmR.localEulerAngles = Vector3.right * 0;
            ArmL.localEulerAngles = Vector3.right * 0;
                }*/