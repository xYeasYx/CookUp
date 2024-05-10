using UnityEngine;

public class TeamAI : MonoBehaviour
{
    public Transform player; // Reference to the player
    public Transform hoop;   // Reference to the hoop
    public Transform planeBounds; // Reference to the plane defining the bounds
    public float yPosition = 0f; // Y position for the teammates
    public float distanceFromPlayer = 5f; // Distance the team members should maintain from the player
    public float minDistanceFromPlayer = 3f; // Minimum distance from the player
    public float maxDistanceFromPlayer = 8f; // Maximum distance from the player
    public float threePointDistance = 7.24f; // The standard distance for a 3-point shot in basketball
    public float moveSpeed = 5f; // Speed at which the team members move

    void Update()
    {
        if (player == null || hoop == null || planeBounds == null)
        {
            Debug.LogError("Player, hoop, or plane bounds references are not set!");
            return;
        }

        // Calculate the direction vector from the player to the hoop
        Vector3 playerToHoopDir = (hoop.position - player.position).normalized;

        // Calculate the desired distance from the player (within the specified range)
        float desiredDistance = Mathf.Clamp(distanceFromPlayer, minDistanceFromPlayer, maxDistanceFromPlayer);

        // Calculate the target position for the team members
        Vector3 targetPosition = player.position + playerToHoopDir * (threePointDistance + desiredDistance);

        // Project the target position onto the plane bounds
        Vector3 projectedPosition = Vector3.ProjectOnPlane(targetPosition - planeBounds.position, planeBounds.up) + planeBounds.position;

        // Set the Y position for the teammates
        projectedPosition.y = yPosition;

        // Clamp the projected position to the bounds of the plane
        Vector3 clampedPosition = projectedPosition;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, planeBounds.position.x - (planeBounds.localScale.x / 2), planeBounds.position.x + (planeBounds.localScale.x * 1.8f));
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, planeBounds.position.z - (planeBounds.localScale.z * 2), planeBounds.position.z + (planeBounds.localScale.z * 3.4f));

        // Move the team members towards the clamped position
        transform.position = Vector3.MoveTowards(transform.position, clampedPosition, moveSpeed * Time.deltaTime);
    }
}
