using UnityEngine;

public class BodyMover : MonoBehaviour
{
    public float moveDistanceMultiplier = 5.0f; // Distance to move each step
    public float moveSpeed = 1.0f;    // Speed of the movement

    private Vector3 targetPosition;   // Target position for movement
    private bool isMoving = false;    // Flag to check if the object is moving
    public stepCounterScript StepCounter;
    //private float moveDistance = StepCounter.newSteps * moveDistanceMultiplier;
    public float moveDistance = 0.0001f;
    public Rigidbody body;
    void Start()
    {
        // Initialize the target position to the current position
        targetPosition = transform.position;
        // StepCounter = GetComponent<stepCounterScript>();
    }

    void Update()
    {
        if (StepCounter.step)
        {
            moveDistanceMultiplier = 1;
            targetPosition = targetPosition + body.transform.forward * moveDistance;
        }
        else
        {
            moveDistanceMultiplier = 0;
        }
        

        body.transform.position = Vector3.Lerp(body.transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(body.transform.position, targetPosition) < 0.01f)
        {
            body.transform.position = targetPosition;
            isMoving = false;
        }
    }
}
