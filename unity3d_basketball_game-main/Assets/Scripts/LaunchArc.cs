using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleBallistic;

[ExecuteInEditMode]
//[RequireComponent(typeof(Rigidbody), typeof(LineRenderer))]
public class LaunchArc : MonoBehaviour
{
    public Animator animator;

    public float shootInc;

    public Rigidbody rigidbody;
    public Rigidbody rb;
    public Transform target;
    public Transform passtarget;
    public Transform shootingPosition;
    public Vector3 InitialVelocity;
    public LineRenderer lineRenderer;

    private float maximumHeightOfArc;
    public float gravity;
    public int pathResolution;
    public bool showDebugPath;

    public float releaseOffset;
    public float dunkForce = 10f;
    private bool isLaunching;
    private bool ispassing;
    private Vector3 savedPosition;

    private bool prepareToShoot = false;
    private bool prepareToDunk = false;

    public GameObject teammateGameObject;
    private float teammateVelocity;

    private bool prepareToPass = false;

    private Vector3 oldPosition = new Vector3(0, 0, 0);
    private Vector3 newPosition = new Vector3(0, 0, 0);
    private Vector3 passTargetPosition;


    Vector3 charachterVelocity;
    Vector3 lastPosition;
    Vector3 distanceTraveled;

    
    public float jumpForce = 10f; // The force applied to jump
    public float jumpHeight = 2f; // The height the character jumps

    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float durationTime;

        public LaunchData(Vector3 velocity, float time)
        {
            this.initialVelocity = velocity;
            this.durationTime = time;
        }
    }


    LaunchData CalculateLaunchData()
    {

        float displacementY = target.position.y - this.transform.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - shootingPosition.transform.position.x, 0, target.position.z - shootingPosition.transform.position.z);
        float targetDistance = Vector3.Distance(this.transform.position, target.transform.position);

        float curveHeight = Mathf.Clamp(targetDistance, 0.5f, 3);

        float value = (displacementY - curveHeight) / gravity;
        float clampedCurve = Mathf.Clamp(value, 0, value);
        float time = Mathf.Sqrt(-2 * curveHeight / gravity) + Mathf.Sqrt(2 * clampedCurve);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * curveHeight);

        //multiply the XZ displacement by the release offset (which is set in the update loop)
        Vector3 velocityXZ = (displacementXZ * releaseOffset) / time;
        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }
    LaunchData CalculateDunkData()
    {

        float displacementY = target.position.y - this.transform.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - shootingPosition.transform.position.x, 0, target.position.z - shootingPosition.transform.position.z);
        float targetDistance = Vector3.Distance(this.transform.position, target.transform.position);

        //float curveHeight = Mathf.Clamp(targetDistance, 0.5f, 3);

        float value = (displacementY) / gravity;
        float clampedCurve = Mathf.Clamp(value, 0, value);
        float time = Mathf.Sqrt(-2 / gravity) ;
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity );

        //multiply the XZ displacement by the release offset (which is set in the update loop)
        Vector3 velocityXZ = (displacementXZ) / time;
        return new LaunchData(velocityXZ*dunkForce + velocityY , time);
    }
    void dunk(){

    }

    LaunchData CalculatePass()
    {
        charachterVelocity = teammateGameObject.GetComponent<Movement>().ControllerVelocity;

        print(charachterVelocity);

        float displacementY = passtarget.position.y - this.transform.position.y;
        float targetDistanceP = Vector3.Distance(this.transform.position, (passtarget.transform.position + charachterVelocity));
        Vector3 displacementXZ = new Vector3((passtarget.position.x + charachterVelocity.x) - shootingPosition.transform.position.x, 0, (passtarget.position.z + charachterVelocity.z) - shootingPosition.transform.position.z);

        float curveHeight = Mathf.Clamp((passtarget.position.magnitude + charachterVelocity.magnitude) * 0.1f, 0, 1);

        float value = (displacementY - curveHeight) / gravity;
        float clampedCurve = Mathf.Clamp(value, 0, value);

        float time = Mathf.Sqrt(-2 * curveHeight / gravity) + Mathf.Sqrt(2 * clampedCurve);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * curveHeight);
        Vector3 velocityXZ = displacementXZ  / time;
        

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    void Launch()
    {

        this.isLaunching = true;
        shootingPosition.transform.LookAt(target.transform);


        Rigidbody clone = Instantiate(rigidbody, shootingPosition.transform.position, Quaternion.identity);
        clone.useGravity = true;

        if (prepareToPass)
        {  Debug.Log("passing");
            clone.velocity = CalculatePass().initialVelocity;
        }

        else if (prepareToShoot)
        {   Debug.Log("Shooting");
            clone.velocity = CalculateLaunchData().initialVelocity;
        }
        else if (prepareToDunk)
        {   Debug.Log("Dunking");
            clone.velocity = CalculateDunkData().initialVelocity;
        }

        Destroy(clone.gameObject, 5);
    }
    void pass()
    {

        this.ispassing = true;
        shootingPosition.transform.LookAt(target.transform);


        Rigidbody clone = Instantiate(rigidbody, shootingPosition.transform.position, Quaternion.identity);
        clone.useGravity = true;

        if (prepareToPass)
        {
            clone.velocity = CalculatePass().initialVelocity;
        }

        else if (prepareToShoot)
        {
            clone.velocity = CalculateLaunchData().initialVelocity;
        }

        Destroy(clone.gameObject, 5);
    }

    void DrawPath()
    {
        LaunchData launchData = CalculateLaunchData();
        if (float.IsNaN(launchData.initialVelocity.y) || float.IsInfinity(launchData.initialVelocity.y))
        {
            return;
        }
        //Vector3 originalPosition = rigidbody.position;

        Vector3 originalPosition = this.transform.position;

        Vector3[] positions = new Vector3[this.pathResolution + 1];
        for (int i = 0; i <= this.pathResolution; i++)
        {
            float simulationTime = (i / (float)this.pathResolution) * launchData.durationTime;
            Vector3 displacement = launchData.initialVelocity * simulationTime + (Vector3.up * gravity) * simulationTime * simulationTime / 2f;
            positions[i] = originalPosition + displacement;
        }
        this.lineRenderer.positionCount = positions.Length;
        this.lineRenderer.SetPositions(positions);

        this.InitialVelocity = launchData.initialVelocity;
    }

    void Awake()
    {
        //Rigidbody rigidbody = GetComponent<Rigidbody>();
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
    }

    void Start()
    {
        //rigidbody.useGravity = false;
        this.isLaunching = false;
        Physics.gravity = Vector3.up * this.gravity;

        //lastPosition = teammateGameObject.transform.position;
    }
    public PlayerStateMachine playerStateMachine;
    void Update()
    {

        if (Input.GetButton("ShootBB") || Input.GetKey(KeyCode.Space))
        {

            DrawPath();

            prepareToPass = false;
            prepareToShoot = true;

            releaseOffset += shootInc; // amount of power put into shot when holding button

            if (releaseOffset >= 1.1f)
            {
                Debug.Log("not shooting that");
                prepareToShoot = false;

            }
            if (releaseOffset <= 1.05f && releaseOffset >= .995f ){ // change color
                lineRenderer.startColor = Color.green;
                lineRenderer.endColor = Color.green;
            }
            else{
                lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;
            }
            
            

        }

        if ( Input.GetButtonUp("ShootBB")|| Input.GetKeyUp(KeyCode.Space))
        {
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;
            if (prepareToShoot)
            {
                Launch();
                releaseOffset = .95f;
            }

            if (!prepareToShoot)
            {
                releaseOffset = .95f;
            }
            prepareToShoot = false;
        }
        /*if (Input.GetKeyDown(KeyCode.Space)){
            prepareToPass = false;
            prepareToShoot = false;
            prepareToDunk = true;
            JumpTowardsTarget();
            
        }
        if (Input.GetKeyUp(KeyCode.Space)){
            Launch();
            prepareToDunk = false;
        }*/
        if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Pass"))
        {
            //this.target = teammatePassTarget;
            playerStateMachine.playerState = PlayerStateMachine.PlayerState.NoBB;
            prepareToPass = true;
            prepareToShoot = false;
            pass();
        }
        /*
        if (showDebugPath && !this.isLaunching)
        {
            DrawPath();
        }*/

        if (showDebugPath)
        {
            DrawPath();
        }
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
            //rb.velocity = jumpDirection;
            rb.AddForce(jumpDirection, ForceMode.Impulse);
            
            Debug.Log(rb.velocity);

    }
    void OnValidate()
    {
        if (showDebugPath && !this.isLaunching)
        {
            DrawPath();
        }
    }
}