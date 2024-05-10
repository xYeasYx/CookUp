using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{   public Transform Char;

    public Transform rightHand;
    public Transform leftHand;

    public Transform Ball;
    public Transform PoundBottomR;
    public Transform PoundMidFront;
    public Transform PoundBottomL;
    public Transform PoundMidTween;
    public Transform PoundMidBack;
    //public Transform PoundTopL;
    float TopBottom;
    public float Speed ;
    public Transform[] TopDribblePoints; // Array of Transform variables
    public int BallHand;
    bool isSkill;

    public Animator animator;

    private bool canshoot = true; // Flag to check if the player can jump
    public float shootCooldown = .5f; // Cooldown time for jump in seconds
    public float shootTimer = 0f;

    private bool canDrib = true; // Flag to check if the player can jump
    public float dribCooldown = 1f; // Cooldown time for jump in seconds
    public float dribTimer = 0f; // Timer to track the cooldown
    // Start is called before the first frame update
    void Start()
    {
         animator.SetBool("RightHand", true);

        //animator = GetComponent<Animator>();
    }

    //0 = right hand
    //1 = left hand

    // Update is called once per frame
    void FixedUpdate()
    {   
        if (Input.GetKey(KeyCode.LeftControl)){
            isSkill = true;
        }
        if (Input.GetKey(KeyCode.Space) && canshoot){
            animator.SetBool("shootHold", true);
            canshoot = false;}

            //animator.SetBool("shootHold", false);
            shootTimer += Time.deltaTime;    // If jump timer exceeds cooldown time, allow jumping again
        if (shootTimer >= shootCooldown)
        {
            canshoot = true;
            shootTimer = 0f; // Reset the timer
        }
        dribTimer += Time.deltaTime;    // If jump timer exceeds cooldown time, allow jumping again
        if (dribTimer >= dribCooldown)
        {
            canDrib = true;
            dribTimer = 0f; // Reset the timer
        }

        Ball.position = TopDribblePoints[BallHand].position;    // start with ball in right hand

        //pound
        if(Input.GetKey(KeyCode.RightArrow) && BallHand == 0 && canDrib){// if you press right dribble button and ball is in right hand
            //Ball.position = Vector3.MoveTowards(Ball.position, PoundBottomR.position, Speed * Time.deltaTime);// regualar right pound dribble
            animator.SetTrigger("RightPressed");
            canDrib = false; 
        }
        if(Input.GetKey(KeyCode.LeftArrow)&& BallHand == 1 && canDrib ){// if you press left dribble button and ball is in left hand
            //Ball.position = Vector3.MoveTowards(Ball.position, PoundBottomL.position, Speed * Time.deltaTime);// regualar right pound dribble
            animator.SetTrigger("LeftPressed");
            canDrib = false; 
        }

        //cross
        if(Input.GetKey(KeyCode.UpArrow)&& !isSkill && BallHand == 0 && canDrib){ // press up and have ball in right hand
            BallHand = 1;
            canDrib = false;  
            animator.SetBool("RightHand", false);
            animator.SetBool("LeftHand", true);
            animator.SetTrigger("UpPressed");
        }
        if(Input.GetKey(KeyCode.UpArrow)&& !isSkill && BallHand == 1 && canDrib){ // press up and have ball in left hand
            //Ball.position = Vector3.MoveTowards(Ball.position, PoundMidFront.position, Speed * Time.deltaTime);
            //Char.position = Vector3.MoveTowards(Char.position, Char.position+new Vector3(0, 0.05f, -.05f), Speed * Time.deltaTime);
            animator.SetBool("RightHand", true);
            animator.SetBool("LeftHand", false);
            animator.SetTrigger("UpPressed");

            BallHand = 0;
            canDrib = false;
        }
        
       


        //behind the back
         if(Input.GetKey(KeyCode.DownArrow)&& !isSkill && BallHand == 0 && canDrib){ // press up and have ball in right hand
            //Ball.position = Vector3.MoveTowards(Ball.position, PoundMidBack.position, Speed * Time.deltaTime);
            //Char.position = Vector3.MoveTowards(Char.position, Char.position+new Vector3(0, 0.05f, .05f), Speed * Time.deltaTime);
            BallHand = 1;
            canDrib = false;  
            animator.SetBool("RightHand", false);
            animator.SetBool("LeftHand", true);
            animator.SetTrigger("DownPressed");
        }
        if(Input.GetKey(KeyCode.DownArrow)&& !isSkill && BallHand == 1 && canDrib){ // press up and have ball in left hand
            //Ball.position = Vector3.MoveTowards(Ball.position, PoundMidBack.position, Speed * Time.deltaTime);
            //Char.position = Vector3.MoveTowards(Char.position, Char.position+new Vector3(0, 0.05f, -.05f), Speed * Time.deltaTime);
            BallHand = 0;
            canDrib = false;
             animator.SetBool("RightHand", true);
            animator.SetBool("LeftHand", false);
            animator.SetTrigger("DownPressed");
        }


         //tween

        /*if(Input.GetKey(KeyCode.UpArrow) && isSkill && BallHand == 0 && canDrib){ // press up and have ball in right hand
            Ball.position = Vector3.MoveTowards(Ball.position, PoundMidTween.position, Speed * Time.deltaTime);
            Char.position = Vector3.MoveTowards(Char.position, Char.position+new Vector3(0, 0.05f, .05f), Speed * Time.deltaTime);
            BallHand = 1;
            canDrib = false;
            isSkill = false;  
        }
        if(Input.GetKey(KeyCode.UpArrow)&& isSkill && BallHand == 1 && canDrib){ // press up and have ball in left hand
            Ball.position = Vector3.MoveTowards(Ball.position, PoundMidTween.position, Speed * Time.deltaTime);
            Char.position = Vector3.MoveTowards(Char.position, Char.position+new Vector3(0, 0.05f, -.05f), Speed * Time.deltaTime);
            BallHand = 0;
            canDrib = false;
            isSkill = false;
        }*/
            //hesi
        if(Input.GetKey(KeyCode.RightArrow) && isSkill && BallHand == 0 && canDrib){// if you press right dribble button and ball is in right hand
            //Ball.position = Vector3.MoveTowards(Ball.position, PoundBottomR.position, Speed * Time.deltaTime);// regualar right pound dribble
            //Char.position = Vector3.MoveTowards(Char.position, Char.position+new Vector3(0, 0.2f, -.1f), Speed * Time.deltaTime);
            isSkill = false;
            canDrib = false;
            Debug.Log("Hesi");
        }
        if(Input.GetKey(KeyCode.LeftArrow)&& isSkill && BallHand == 1 && canDrib){// if you press left dribble button and ball is in left hand
            //Ball.position = Vector3.MoveTowards(Ball.position, PoundBottomL.position, Speed * Time.deltaTime);// regualar right pound dribble
            //Char.position = Vector3.MoveTowards(Char.position, Char.position+new Vector3(0, 0.2f, .1f), Speed * Time.deltaTime);
            isSkill = false;
            canDrib = false;
            Debug.Log("Hesi");
        }
        
    }
     IEnumerator MoveBallToTarget()
    {
        while (Vector3.Distance(Ball.position, PoundBottomR.position) > 0.1f)
        {
            Ball.position = Vector3.MoveTowards(Ball.position, PoundBottomR.position, Speed * Time.deltaTime);
            yield return null; // Wait for the next frame
            
            
        }
        Debug.Log("Dribble done");
    }
}


/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{   public Transform Char;
    public Transform Ball;
    public Transform PoundBottomR;
    public Transform PoundMidFront;
    public Transform PoundBottomL;
    public Transform PoundMidTween;
    public Transform PoundMidBack;
    //public Transform PoundTopL;
    float TopBottom;
    public float Speed ;
    public Transform[] TopDribblePoints; // Array of Transform variables
    public int BallHand;
    bool isSkill;

    public Animator animator;

    private bool canDrib = true; // Flag to check if the player can jump
    public float dribCooldown = 1f; // Cooldown time for jump in seconds
    public float dribTimer = 0f; // Timer to track the cooldown
    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
    }

    //0 = right hand
    //1 = left hand

    // Update is called once per frame
    void FixedUpdate()
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

        Ball.position = TopDribblePoints[BallHand].position;    // start with ball in right hand

        //pound
        if(Input.GetKey(KeyCode.RightArrow) && BallHand == 0 && canDrib){// if you press right dribble button and ball is in right hand
            Ball.position = Vector3.MoveTowards(Ball.position, PoundBottomR.position, Speed * Time.deltaTime);// regualar right pound dribble
            animator.SetTrigger("RightPressed");
            canDrib = false; 
        }
        if(Input.GetKey(KeyCode.LeftArrow)&& BallHand == 1 && canDrib ){// if you press left dribble button and ball is in left hand
            Ball.position = Vector3.MoveTowards(Ball.position, PoundBottomL.position, Speed * Time.deltaTime);// regualar right pound dribble
            canDrib = false; 
        }

        //cross
        if(Input.GetKey(KeyCode.UpArrow)&& !isSkill && BallHand == 0 && canDrib){ // press up and have ball in right hand
            Ball.position = Vector3.MoveTowards(Ball.position, PoundMidFront.position, Speed * Time.deltaTime);
            Char.position = Vector3.MoveTowards(Char.position, Char.position+new Vector3(0, 0.05f, .05f), Speed * Time.deltaTime);
            BallHand = 1;
            canDrib = false;  
        }
        if(Input.GetKey(KeyCode.UpArrow)&& !isSkill && BallHand == 1 && canDrib){ // press up and have ball in left hand
            Ball.position = Vector3.MoveTowards(Ball.position, PoundMidFront.position, Speed * Time.deltaTime);
            Char.position = Vector3.MoveTowards(Char.position, Char.position+new Vector3(0, 0.05f, -.05f), Speed * Time.deltaTime);
            BallHand = 0;
            canDrib = false;
        }
        
       


        //behind the back
         if(Input.GetKey(KeyCode.DownArrow)&& !isSkill && BallHand == 0 && canDrib){ // press up and have ball in right hand
            Ball.position = Vector3.MoveTowards(Ball.position, PoundMidBack.position, Speed * Time.deltaTime);
            Char.position = Vector3.MoveTowards(Char.position, Char.position+new Vector3(0, 0.05f, .05f), Speed * Time.deltaTime);
            BallHand = 1;
            canDrib = false;  
        }
        if(Input.GetKey(KeyCode.DownArrow)&& !isSkill && BallHand == 1 && canDrib){ // press up and have ball in left hand
            Ball.position = Vector3.MoveTowards(Ball.position, PoundMidBack.position, Speed * Time.deltaTime);
            Char.position = Vector3.MoveTowards(Char.position, Char.position+new Vector3(0, 0.05f, -.05f), Speed * Time.deltaTime);
            BallHand = 0;
            canDrib = false;
        }


         //tween

        if(Input.GetKey(KeyCode.UpArrow) && isSkill && BallHand == 0 && canDrib){ // press up and have ball in right hand
            Ball.position = Vector3.MoveTowards(Ball.position, PoundMidTween.position, Speed * Time.deltaTime);
            Char.position = Vector3.MoveTowards(Char.position, Char.position+new Vector3(0, 0.05f, .05f), Speed * Time.deltaTime);
            BallHand = 1;
            canDrib = false;
            isSkill = false;  
        }
        if(Input.GetKey(KeyCode.UpArrow)&& isSkill && BallHand == 1 && canDrib){ // press up and have ball in left hand
            Ball.position = Vector3.MoveTowards(Ball.position, PoundMidTween.position, Speed * Time.deltaTime);
            Char.position = Vector3.MoveTowards(Char.position, Char.position+new Vector3(0, 0.05f, -.05f), Speed * Time.deltaTime);
            BallHand = 0;
            canDrib = false;
            isSkill = false;
        }
            //hesi
        if(Input.GetKey(KeyCode.RightArrow) && isSkill && BallHand == 0 && canDrib){// if you press right dribble button and ball is in right hand
            Ball.position = Vector3.MoveTowards(Ball.position, PoundBottomR.position, Speed * Time.deltaTime);// regualar right pound dribble
            Char.position = Vector3.MoveTowards(Char.position, Char.position+new Vector3(0, 0.2f, -.1f), Speed * Time.deltaTime);
            isSkill = false;
            canDrib = false;
            Debug.Log("Hesi");
        }
        if(Input.GetKey(KeyCode.LeftArrow)&& isSkill && BallHand == 1 && canDrib){// if you press left dribble button and ball is in left hand
            Ball.position = Vector3.MoveTowards(Ball.position, PoundBottomL.position, Speed * Time.deltaTime);// regualar right pound dribble
            Char.position = Vector3.MoveTowards(Char.position, Char.position+new Vector3(0, 0.2f, .1f), Speed * Time.deltaTime);
            isSkill = false;
            canDrib = false;
            Debug.Log("Hesi");
        }
        
    }
     IEnumerator MoveBallToTarget()
    {
        while (Vector3.Distance(Ball.position, PoundBottomR.position) > 0.1f)
        {
            Ball.position = Vector3.MoveTowards(Ball.position, PoundBottomR.position, Speed * Time.deltaTime);
            yield return null; // Wait for the next frame
            
            
        }
        Debug.Log("Dribble done");
    }
}*/

