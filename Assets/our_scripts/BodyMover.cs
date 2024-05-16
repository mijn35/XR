using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using System;

public class BodyMover : MonoBehaviour
{

     [System.Serializable]
    public class Data
    {
        public List<float> values;
        public string timestamp;
        public string accuracy;
        public string type;
    }

    public float moveDistanceMultiplier = 5.0f; // Distance to move each step
    public float moveSpeed = 1.0f;    // Speed of the movement

    public Rigidbody body;

    private Vector3 targetPosition;   // Target position for movement
    private bool isMoving = false;    // Flag to check if the object is moving
    public stepCounterScript StepCounter;
    //private float moveDistance = StepCounter.newSteps * moveDistanceMultiplier;
    public string data;
    public Connection Connect;
    public Data d;
    public float moveDistance = 0.01f;
    public Quaternion rotation;
    public float rot;
    public float rotX;
    public float rotY;

    public IKHipsTargetFollowPhoneRotation hips;
    void Start()
    {
        // Initialize the target position to the current position
        targetPosition = transform.position;
        data = Connect.data;
        // StepCounter = GetComponent<stepCounterScript>();
    }

    public static Data readJson(string data)
    {
        return JsonUtility.FromJson<Data>(data);
    }
    void Update()
    {
        rotation = hips.forward;        

        if (StepCounter.step)
        {
            Vector3 added = new Vector3(0, 0, 1);
            moveDistanceMultiplier = 1;
            //targetPosition = targetPosition + body.transform.forward * moveDistance;
            targetPosition = targetPosition + rotation * added * moveDistance;
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
