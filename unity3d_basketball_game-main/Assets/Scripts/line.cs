using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line : MonoBehaviour
{
    public Transform Point1;
    public Transform Point2;
    public Transform Point3;
    public Transform Point4;
    public Transform Point5;
    public LineRenderer lineRenderer;
    public float vertexCount = 12;
    public float Point2Ypositio = 2;
    Vector3 tangent3;
    Vector3 tangent4;

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

    // Update is called once per frame
    void Update()
    {
        //Point2.transform.position = new Vector3((Point1.transform.position.x + Point3.transform.position.x), Point2Ypositio, (Point1.transform.position.z + Point3.transform.position.z) / 2);
        if (startPoint != null && endPoint != null)
        {
            // Set positions of the line renderer
            lineRenderer.SetPosition(0, startPoint.position);
            lineRenderer.SetPosition(1, endPoint.position);
        }
        var pointList = new List<Vector3>();

        for(float ratio = 0;ratio<=1;ratio+= 1/vertexCount)
        {
            var tangent1 = Vector3.Lerp(Point1.position, Point2.position, ratio);
            var tangent2 = Vector3.Lerp(Point2.position, Point3.position, ratio);
            tangent3 = Vector3.Lerp(Point3.position, Point4.position, ratio);
            tangent4 = Vector3.Lerp(Point2.position, Point5.position, ratio);
            var curve = Vector3.Lerp(tangent1, tangent2, ratio);

            
            pointList.Add(curve);
        }
        //pointList.Add(Point4.position);
        //pointList.Add(Point3.position);
        //pointList.Add(Point5.position);

        lineRenderer.positionCount = pointList.Count;
        lineRenderer.SetPositions(pointList.ToArray());
    }
}