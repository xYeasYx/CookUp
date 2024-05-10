using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDAnimationStateController : MonoBehaviour
{

    Animator animator;
    float velocityZ =0f;
    float velocityX = 0f;
    public float acceleration = 2.0f;
    public float decceleration = 2.0f;
    public float maxWalkVelocity = .5f;
    public float maxRunVelocity = 2f;
    // Start is called before the first frame update
    int VelocityZHash;
    int VelocityXHash;
    void Start()
    {
        animator = GetComponent<Animator>();
        VelocityZHash = Animator.StringToHash("velocityz");
        VelocityXHash = Animator.StringToHash("velocityx");
    }

    //handles acceleration and deceleration
    void changeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity){
        // if player presses forward, increase velocity in z direction
        if(forwardPressed && velocityZ < currentMaxVelocity){
            
            velocityZ += Time.deltaTime * acceleration;
            //Debug.Log(velocity);
        }
        // increase velocity in left direction
        if(leftPressed&& velocityX > -currentMaxVelocity){
            
            velocityX -= Time.deltaTime * acceleration;
            //Debug.Log(velocity);
        }
        //increase velocity in right dircetion
        if(rightPressed && velocityX < currentMaxVelocity){
            
            velocityX += Time.deltaTime * acceleration;
            //Debug.Log(velocity);
        }
        //decrease velocityz
        if (!forwardPressed && velocityZ > 0f){
            velocityZ -= Time.deltaTime * decceleration;
        }
        // increase velocityx if left is not pressed and velocityX < 0
        if (!leftPressed && velocityX < 0f){
            velocityX += Time.deltaTime * decceleration;
        }
        // decrease velocityx if irght is not pressed and velocityx > 0
        if (!rightPressed && velocityX > 0f){
            velocityX -= Time.deltaTime * decceleration;
        }
    }

    //handles reset and locking of velocity
    void lockOrResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity){
        //reset velocityz
        if (!forwardPressed && velocityZ < 0f){
            velocityZ = 0f;
        }

        //reset velocityx
        if(!leftPressed && !rightPressed && velocityX != 0f && (velocityX > -0.05f && velocityX < 0.05f)){
            velocityX = 0f;
        }
        //lock forword
        if(forwardPressed && runPressed && velocityZ > currentMaxVelocity){
                velocityZ = currentMaxVelocity;
        }
        //decelertate to the maxikmum walk velocity
        else if(forwardPressed && velocityZ > currentMaxVelocity){
            if(velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + .05f)){
            velocityZ = currentMaxVelocity;}
        }
        //round to the currentmaxVel if within the offset
        else if(forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - .05f)){
                velocityZ = currentMaxVelocity;
        }


         //lock left
        if(leftPressed && runPressed && velocityX < -currentMaxVelocity){
                velocityX = -currentMaxVelocity;
        }
        //decelertate to the maxikmum walk velocity
        else if(leftPressed && velocityX < -currentMaxVelocity){
            if (velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity - .05f)){
                velocityX = -currentMaxVelocity;}
        }
        //round to the currentmaxVel if within the offset
        else if(leftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + .05f)){
                velocityX = -currentMaxVelocity;
        }


         //lock right
        if(rightPressed && runPressed && velocityX > currentMaxVelocity){
                velocityX = currentMaxVelocity;
        }
        //decelertate to the maxikmum walk velocity
        else if(runPressed && velocityX > currentMaxVelocity){
            if(velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + .05f)){
            velocityX = currentMaxVelocity;}
        }
        //round to the currentmaxVel if within the offset
        else if(rightPressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - .05f)){
                velocityX = currentMaxVelocity;
        }

    }
    void Update()
    {   
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        //set current maxVelocity
        float currentMaxVelocity = runPressed ? maxRunVelocity : maxWalkVelocity;
        // handle changes in velocity
        changeVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
        lockOrResetVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
        
        
        //set the parameters to our local variable values
        animator.SetFloat(VelocityZHash, velocityZ);
        animator.SetFloat(VelocityXHash, velocityX);
    }
}
