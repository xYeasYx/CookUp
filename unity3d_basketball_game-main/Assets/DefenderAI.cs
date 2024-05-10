using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.UI;

public class DefenderAI : MonoBehaviour
{
    public Transform player; // Reference to the player
    public Transform hoop;   // Reference to the hoop
    public float moveSpeed = 5f; // Speed at which the defender moves
    public float distanceFromPlayer ; // Distance the defender should maintain from the player
    float dist;
    public Transform Defender;
    public float reactionDelay = 0.5f; // Delay in seconds for the defender's reaction

    private Vector3 previousPlayerPosition;
    private Vector3 targetPosition;

    public GameObject WinCanvas;

    public Transform ArmR;
    public Transform ArmL;

    public Slider healthSlider;

    private Rigidbody rb;
    public float jumpForce;

     private float distance; // This will store the calculated distance

     public  float DefenderPosture;
     public  float MaxHealth = 10000f;


    void Start()
    {
         // Initialize the previous player position to the current position
        previousPlayerPosition = player.position;
        targetPosition = transform.position; // Start with the defender's current position
        dist = distanceFromPlayer;
        rb = GetComponent<Rigidbody>();
        //yield return new WaitForSeconds(5f);

    }
    void Win(){
        WinCanvas.SetActive(true);
        Time.timeScale = 0f;

    }
    void Update()
    {   
        distance = Vector3.Distance(Defender.position, player.position);

        if(DefenderPosture >= 10000f ){
            Win();
        }
        
        CalculateDmg(distance);
        transform.position = new Vector3(transform.position.x, 1.4f, transform.position.z);
        if(Input.GetKey(KeyCode.Space)){
            Babyjump();}
        if(Input.GetKey(KeyCode.Space)){
            Debug.Log("hands up!");
            //ArmR.localEulerAngles = Vector3.right * 180;        // put hands up to contest shot
            //ArmL.localEulerAngles = Vector3.right * 180;
            distanceFromPlayer = .5f;
        }
        if(Input.GetKey(KeyCode.Space)){
            //ArmR.localEulerAngles = Vector3.right * 0 ;     //put hands down
            //ArmL.localEulerAngles = Vector3.right * 0;
            
            distanceFromPlayer = dist;          //brings distance back to normal
        }
           
        if (player == null || hoop == null)
        {
            Debug.LogError("Player or hoop references are not set!");
            return;
        }

        // Update the target position with a delay based on the player's movement
        targetPosition = CalculateTargetPosition();

        // Move the defender towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Rotate the defender to face the player
        transform.LookAt(player);

        // Update the previous player position
        previousPlayerPosition = player.position;
    }
    public void CalculateDmg(float distance){
        if(distance >= 7f){DefenderPosture= DefenderPosture + 5f;}
        else if(distance < 7f && distance >= 4f){DefenderPosture= DefenderPosture + 3f;}
        else if(distance <= 3.1f){
            //nothing
            DefenderPosture= DefenderPosture - 1.2f;
        }
        else{DefenderPosture = DefenderPosture + 1f;}
        healthSlider.value = DefenderPosture;
        //Debug.Log(DefenderPosture);
    }
    Vector3 CalculateTargetPosition()
    {
        // Calculate the direction vector from the player to the hoop
        Vector3 playerToHoopDir = (hoop.position - player.position).normalized;

        // Calculate the target position for the defender
        Vector3 targetPos;

        // Check if the player is moving away from the defender
        if ((player.position - previousPlayerPosition).magnitude > 0.1f)
        {
            // If the player is moving away, move the defender towards the hoop position
            targetPos = hoop.position;
        }
        else
        {
            // If the player is not moving away, maintain the distance from the player
            targetPos = player.position + playerToHoopDir * distanceFromPlayer;
        }

        // Gradually move towards the calculated target position based on reaction delay
        return Vector3.Lerp(targetPosition, targetPos, reactionDelay * Time.deltaTime);
    }
    void Babyjump(){
        
        // Check if the defender is grounded before jumping
        //if (Physics.Raycast(transform.position, Vector3.down, 0.1f))
        
            // Apply vertical force to make the defender jump
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        
    }
}
