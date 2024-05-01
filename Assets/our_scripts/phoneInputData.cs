// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using WebSocketSharp;

// public class WebSocketClient : MonoBehaviour
// {
//     private WebSocket ws;

//     private void Start()
//     {
//         string url = "ws://192.168.90.203:8080/sensor/connect?type=android.sensor.accelerometer";
//         Debug.Log("Connecting to: " + url);

//         ws = new WebSocket(url);
//         ws.OnMessage += OnMessageReceived;

//         // Attempting connection
//         Debug.Log("Attempting connection...");
//         ws.Connect();
//     }

//     private void OnDestroy()
//     {
//         if (ws != null)
//         {
//             ws.OnMessage -= OnMessageReceived;
//             ws.Close();
//             Debug.Log("WebSocket connection closed.");
//         }
//     }

//     private void OnMessageReceived(object sender, MessageEventArgs e)
//     {
//         Debug.Log("Received message: " + e.Data);
//     }

//     // You can call this method from elsewhere in your code to send a message
//     public void SendMessage(string message)
//     {
//         if (ws != null && ws.IsAlive)
//         {
//             ws.Send(message);
//             Debug.Log("Sent message: " + message);
//         }
//         else
//         {
//             Debug.LogWarning("WebSocket connection is not available.");
//         }
//     }
// }
