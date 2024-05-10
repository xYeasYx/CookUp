using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Basketball : MonoBehaviour
{
    public Transform targetTransform; // Target position for the ball
    public float moveSpeed = 5f; // Speed at which the ball moves towards the target

    void Update()
    {
        // Example: Triggering the ball movement when pressing the arrow keys
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(MoveBallToTarget());
        }
    }

    IEnumerator MoveBallToTarget()
    {
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = targetTransform.position;

        float distance = Vector3.Distance(initialPosition, targetPosition);
        float duration = distance / moveSpeed;
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            yield return null;
        }

        // Ensure the ball reaches the exact target position
        transform.position = targetPosition;
    }
}




/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBall : MonoBehaviour
{   
    public Transform Ball;
    public Transform PoundBottomR;
    public Transform PoundTopR;
    public Transform PoundBottomL;
    public Transform PoundTopL;
    float TopBottom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {   //TopBottom= Mathf.Abs(Mathf.Sin(Time.time*5));
        //Ball.position = PoundTopR.position;
        if(Input.GetKey(KeyCode.RightArrow)){
            Ball.position = PoundBottomR.position;
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            Ball.position = PoundBottomL.position;
        }
        //Ball.position = PoundBottomR.position + Vector3.up * Mathf.Abs(Mathf.Sin(Time.time*5));
    }
}*/
