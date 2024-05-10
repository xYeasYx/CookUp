using UnityEngine;

public class TeamateAI : MonoBehaviour
{
    public Transform hoop;   // Reference to the hoop
    public float threePointDistance = 7.24f; // The standard distance for a 3-point shot in basketball
    public float roamRadius = 1f; // Radius within which the teammate roams
    public float minStopDuration = 1f; // Minimum duration to stop at each position
    public float maxStopDuration = 3f; // Maximum duration to stop at each position

    private Vector3 targetPosition;
    private bool isMoving = true;
    private float stopDuration;

    void Start()
    {
        // Set initial target position
        SetRandomTargetPosition();
        // Start moving immediately
        stopDuration = Random.Range(minStopDuration, maxStopDuration);
    }

    void Update()
    {
        if (hoop == null)
        {
            Debug.LogError("Hoop reference is not set!");
            return;
        }

        // Move only if isMoving is true
        if (isMoving)
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);

            // Check if reached the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                // Stop moving and start stop duration
                isMoving = false;
                Invoke("ResumeMoving", stopDuration);
            }
        }
    }

    void SetRandomTargetPosition()
    {
        // Calculate random angle around the hoop
        float randomAngle = Random.Range(0f, 360f);
        // Calculate random position within roam radius around the hoop
        Vector3 randomPosition = hoop.position + Quaternion.Euler(0f, randomAngle, 0f) * (Vector3.forward * roamRadius);

        // Ensure the random position is within the 3-point line distance
        randomPosition = hoop.position + (randomPosition - hoop.position).normalized * threePointDistance;

        // Set the target position
        targetPosition = randomPosition;
    }

    void ResumeMoving()
    {
        // Start moving again
        isMoving = true;
        // Set new random target position
        SetRandomTargetPosition();
        // Randomize stop duration for the next stop
        stopDuration = Random.Range(minStopDuration, maxStopDuration);
    }
}


