using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using System;

//Upgraded version of the data collection script that allows us to get the accelerometer data to objects. We can work on that to
//add more functions

public class WebSocketClient : MonoBehaviour
{
    private WebSocket ws;

    // Public static variables to hold the latest sensor data
    public static float yaw = 0.0f;
    public static float pitch = 0.0f;
    public static float roll = 0.0f;

    private void Start()
    {
        string url = "ws://192.168.90.203:8080/sensor/connect?type=android.sensor.accelerometer";
        Debug.Log("Connecting to: " + url);

        ws = new WebSocket(url);
        ws.OnMessage += OnMessageReceived;

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
        ProcessMessage(e.Data);
    }

    private void ProcessMessage(string data)
    {
        SensorData sensorData = JsonUtility.FromJson<SensorData>(data);
        if (sensorData.values.Length == 3)
        {
            yaw = sensorData.values[0]; // Assign X to yaw
            pitch = sensorData.values[1]; // Assign Y to pitch
            roll = sensorData.values[2]; // Assign Z to roll
        }
    }
}


[System.Serializable] //Public class that enables JSON data reading
public class SensorData
{
    public float[] values;
    public long timestamp;
    public int accuracy;
}
