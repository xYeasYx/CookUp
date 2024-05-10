using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    float velocity = 0f;
    public float acceleration;
    public float decceleration;
    int VelocityHash;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        VelocityHash = Animator.StringToHash("velocity");
        
    }

    // Update is called once per frame
    void Update()
    {   
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");

        if(forwardPressed && velocity < 1f){
            
            velocity += Time.deltaTime * acceleration;
            Debug.Log(velocity);
        }
        if(!forwardPressed && velocity > 0f){
            
            velocity -= Time.deltaTime * decceleration;
            Debug.Log(velocity);
        }
        if(!forwardPressed && velocity < 0f){
            
            velocity = 0f;
        }
        animator.SetFloat(VelocityHash, velocity);
        
    }
}
/*if(!isWalking && forwardPressed){
            animator.SetBool(isWalkingHash, true);
        }
        if(isWalking && !forwardPressed){
            animator.SetBool(isWalkingHash, false);
        }
         if(!isRunning && (forwardPressed && runPressed)){
            animator.SetBool(isRunningHash, true);
        }
        if(isRunning && (!forwardPressed || !runPressed)){
            animator.SetBool(isRunningHash, false);
        }*/