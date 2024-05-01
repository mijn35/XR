// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PhoneRotationInput : MonoBehaviour
// {
//     // These public fields can be adjusted in the Unity editor or dynamically via code.
//     public float yaw;
//     public float pitch;
//     public float roll;

//     void Update()
//     {
//         // Assuming the data is updated somewhere externally in these variables:
//         ApplyRotation();
//         AdjustPosition();
//     }

//     private void ApplyRotation()
//     {
//         // Convert angles from degrees to quaternion for proper rotation
//         Quaternion targetRotation = Quaternion.Euler(pitch, yaw, roll);
//         transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5);
//     }

//     private void AdjustPosition()
//     {
//         // Update the position based on the distance from center.
//         // Assuming 'distanceFromCenter' affects the z-axis position.
//         // You can modify the axis according to your coordinate system.
//         Vector3 currentPosition = transform.position;
//         transform.position = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z);
//     }
// }
