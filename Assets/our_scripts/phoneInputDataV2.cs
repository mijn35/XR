using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using System;
using JetBrains.Annotations;


//Upgraded version of the data collection script that allows us to get the accelerometer data to objects. We can work on that to
//add more functions

public class WebSocketClient : MonoBehaviour
{
    private WebSocket ws;

    // Public static variables to hold the latest sensor data
    public static float yaw = 0.0f;
    public static float pitch = 0.0f;
    public static float roll = 0.0f;
    public static float theta = 0.0f;

    public float multiplier = 1.0f;

    public string ip = "192.168.90.203:8080"; //{ get => ip; set => ip;}
    public string sensor = "game_rotation_vector";


    private void Start()
    {
        
        string url = "ws://" + ip + "/sensor/connect?type=android.sensor." + sensor;
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
        ProcessMessageGameVector(e.Data);
    }

    /*private void ProcessMessageOrientation(string data)
    {
        SensorData sensorData = JsonUtility.FromJson<SensorData>(data);
        if (sensorData.values.Length == 3)
        {
            yaw = sensorData.values[0]; // Assign X to yaw
            pitch = sensorData.values[1]; // Assign Y to pitch
            roll = sensorData.values[2]; // Assign Z to roll
        }
    }*/

    private void ProcessMessageGameVector(string data)
    {
        SensorData sensorData = JsonUtility.FromJson<SensorData>(data);
        if (sensorData.values.Length == 4)
        {
            yaw = sensorData.values[0] * multiplier; // Assign X to yaw
            pitch = sensorData.values[1] * multiplier; // Assign Y to pitch
            roll = sensorData.values[2] * multiplier; // Assign Z to roll
            theta = sensorData.values[3]; // Assign theta
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
