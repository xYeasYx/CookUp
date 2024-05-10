using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine;

public class DistanceToRim : MonoBehaviour
{
    public Transform rim; // Reference to the rim object
    public float threePointDistance = 7.24f; // The standard distance for a 3-point shot in basketball

    private LineRenderer lineRenderer;

    void Start()
    {}
        

    void Update()
    {
        if (rim == null)
        {
            Debug.LogError("Rim reference is not set!");
            return;
        }

       // UpdateLinePositions();

        // Calculate the distance between the player (this object) and the rim
        float distanceToRim = Vector3.Distance(transform.position, rim.position);
        Debug.Log(distanceToRim);
        // Check if the player is within the 3-point distance
        if (distanceToRim >= threePointDistance)
        {
            Debug.Log("You are within 3-point range!");
            // You can perform additional actions here, such as enabling shooting mechanics
        }
        else
        {
            Debug.Log("You are not within 3-point range.");
            // You can perform additional actions here, such as disabling shooting mechanics
        }
    }

    void UpdateLinePositions()
    {
        // Update the Line Renderer positions to draw the 3-point line from the rim to the player
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, rim.position);
    }
}

