using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cornerLine : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public Transform startPoint;
    public Transform endPoint;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.enabled = true;
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.5f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
    }
    void Update()
    {
        if (startPoint != null && endPoint != null)
        {
            // Set positions of the line renderer
            lineRenderer.SetPosition(0, startPoint.position);
            lineRenderer.SetPosition(1, endPoint.position);
        }
    }
}
