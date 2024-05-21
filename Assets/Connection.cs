using UnityEngine;
using WebSocketSharp;
public class Connection : MonoBehaviour
{
    private WebSocket ws;
    public string data;

    public string ip = "192.168.90.203:8080";

    private void Start()
    {
        //string url = "ws://192.168.232.130:8088/sensor/connect?type=android.sensor.gyroscope";
        string url = "ws://" + ip + "/sensors/connect?types=[\"android.sensor.accelerometer\",\"android.sensor.orientation\"]";
        Debug.Log("Connecting to: " + url);

        ws = new WebSocket(url);
        ws.OnMessage += OnMessageReceived;

        // Attempting connection
        Debug.Log("Attempting connection...");
        ws.Connect();
    }

    private void OnDestroy()
    {
        if (ws != null)
        {
            ws.OnMessage -= OnMessageReceived;
            ws.Close();
            Debug.Log("WebSocket connection closed.");
        }
    }

    private void OnMessageReceived(object sender, MessageEventArgs e)
    {
        Debug.Log("Received message: " + e.Data);
        data = e.Data;

    }

    // You can call this method from elsewhere in your code to send a message
    public new void SendMessage(string message)
    {
        if (ws != null && ws.IsAlive)
        {
            ws.Send(message);
            Debug.Log("Sent message: " + message);
        }
        else
        {
            Debug.LogWarning("WebSocket connection is not available.");
        }
    }
}