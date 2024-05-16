using UnityEngine;

public class BodyMover : MonoBehaviour
{
    public float moveDistanceMultiplier = 5.0f; // Distance to move each step
    public float moveSpeed = 2.0f;    // Speed of the movement

    private Vector3 targetPosition;   // Target position for movement
    private bool isMoving = false;    // Flag to check if the object is moving
    public stepCounterScript StepCounter;
    //private float moveDistance = StepCounter.newSteps * moveDistanceMultiplier;
    private float moveDistance = 1;
    void Start()
    {
        // Initialize the target position to the current position
        targetPosition = transform.position;
        StepCounter = GetComponent<stepCounterScript>();
    }

    void Update()
    {
        if (StepCounter.step)
        {
            moveDistance = 1;
        }
        else
        {
            moveDistance = 0;
        }
        if (!isMoving)
        {
            targetPosition = targetPosition + transform.forward * moveDistance;
            isMoving = true;
        }

        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }




        if (StepCounter.step)
        {
            transform.position = transform.position + transform.forward * 0.05f;
        }
    }
}
