using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDribbleController : MonoBehaviour
{

    public Transform[] TopDribblePoints; // Array of Transform variables
    public int BallHand;
    bool isSkill;

    private bool canDrib = true; // Flag to check if the player can jump
    public float dribCooldown = 1f; // Cooldown time for jump in seconds
    public float dribTimer = 0f;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdateUpdate()
    {
        if (Input.GetKey(KeyCode.LeftControl)){
            isSkill = true;
        }
            
        dribTimer += Time.deltaTime;    // If jump timer exceeds cooldown time, allow jumping again
        if (dribTimer >= dribCooldown)
        {
            canDrib = true;
            dribTimer = 0f; // Reset the timer
        }

        //Ball.position = TopDribblePoints[BallHand].position;    // start with ball in right hand

        //pound
        if(Input.GetKey(KeyCode.RightArrow) && BallHand == 0){// if you press right dribble button and ball is in right hand
           // Trigger the animation by setting the "RightPressed" trigger
            
            
        }
        if(Input.GetKey(KeyCode.LeftArrow)&& BallHand == 1){// if you press left dribble button and ball is in left hand
           
        }

        //cross
        if(Input.GetKey(KeyCode.UpArrow)&& !isSkill && BallHand == 0 && canDrib){ // press up and have ball in right hand
           
            BallHand = 1;
            canDrib = false;  
        }
        if(Input.GetKey(KeyCode.UpArrow)&& !isSkill && BallHand == 1 && canDrib){ // press up and have ball in left hand
            
            BallHand = 0;
            canDrib = false;
        }
        
       


        //behind the back
         if(Input.GetKey(KeyCode.DownArrow)&& !isSkill && BallHand == 0 && canDrib){ // press up and have ball in right hand
            
            BallHand = 1;
            canDrib = false;  
        }
        if(Input.GetKey(KeyCode.DownArrow)&& !isSkill && BallHand == 1 && canDrib){ // press up and have ball in left hand
            
            BallHand = 0;
            canDrib = false;
        }


         //tween

        if(Input.GetKey(KeyCode.UpArrow) && isSkill && BallHand == 0 && canDrib){ // press up and have ball in right hand
           
            BallHand = 1;
            canDrib = false;
            isSkill = false;  
        }
        if(Input.GetKey(KeyCode.UpArrow)&& isSkill && BallHand == 1 && canDrib){ // press up and have ball in left hand
            
            BallHand = 0;
            canDrib = false;
            isSkill = false;
        }
            //hesi
        if(Input.GetKey(KeyCode.RightArrow) && isSkill && BallHand == 0 && canDrib){// if you press right dribble button and ball is in right hand
            
            isSkill = false;
            canDrib = false;
            Debug.Log("Hesi");
        }
        if(Input.GetKey(KeyCode.LeftArrow)&& isSkill && BallHand == 1 && canDrib){// if you press left dribble button and ball is in left hand
            
            isSkill = false;
            canDrib = false;
            Debug.Log("Hesi");
        }
    }
}
