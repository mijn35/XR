using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneRotationController : MonoBehaviour
{
    void Update()
    {
        // Use the static variables directly every frame
        ApplyRotation();
    }

    private void ApplyRotation()
    {
        // Convert angles from degrees to quaternion for proper rotation
        Quaternion targetRotation = Quaternion.Euler(WebSocketClient.pitch, WebSocketClient.yaw, WebSocketClient.roll); //data straight from WebSocektClient
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5);
    }
}
