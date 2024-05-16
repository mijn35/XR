using UnityEngine;

public class BodyMover : MonoBehaviour
{
    public float moveDistanceMultiplier = 5.0f; // Distance to move each time
    public float moveSpeed = 2.0f;    // Speed of the movement

    private Vector3 targetPosition;   // Target position for movement
    private bool isMoving = false;    // Flag to check if the object is moving
    public StepCounter StepCounter = GetComponent<StepCounter>();
    private float moveDistance = StepCounter.newSteps * moveDistanceMultiplier;
    void Start()
    {
        // Initialize the target position to the current position
        targetPosition = transform.position;
    }

    void Update()
    {
        if (!isMoving)
        {
            targetPosition = transform.position + transform.forward * moveDistance;
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
    }
}
