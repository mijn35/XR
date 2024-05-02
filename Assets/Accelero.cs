using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using System.Diagnostics;
using System;
using System.Xml.Schema;
using System.Security.Cryptography;

public class Accelero : MonoBehaviour
{
    public class Data
    {
        public List<float> values;
        public string timestamp;
        public string accuracy;
        public string type;
    }
    public string data;

    public float difftrig = 4f;
    //public float totaltrig = 50;
    public float diffOtrig = 100;

    public Connection Connect;
    public Data d;
    public float prevX;
    //public float prevY;
    //public float prevZ;
    public float prevO;
    public float accelerationX;
    //public float accelerationY;
    //public float accelerationZ;
    //public float diffX;
    //public float diffY;
    //public float diffZ;
    public float prevStampX;
    public float prevStampO;
    public float oriantation;
    public float tempStampX;
    public float tempStampO;

    public float delta;
    //public float test2;
    //public float test3;
    //public float test4;

    public float diffOPublic;
    public float stepPublic;

    public static Data readJson(string data)
    {
        return JsonUtility.FromJson<Data>(data);
    }
    /* * * *
    * 
    *   [DebugGUIGraph]
    *   Renders the variable in a graph on-screen. Attribute based graphs will updates every Update.
    *    Lets you optionally define:
    *        max, min  - The range of displayed values
    *        r, g, b   - The RGB color of the graph (0~1)
    *        group     - Graphs can be grouped into the same window and overlaid
    *        autoScale - If true the graph will readjust min/max to fit the data
    *   
    *   [DebugGUIPrint]
    *    Draws the current variable continuously on-screen as 
    *    $"{GameObject name} {variable name}: {value}"
    *   
    *   For more control, these features can be accessed manually.
    *    DebugGUI.SetGraphProperties(key, ...) - Set the properties of the graph with the provided key
    *    DebugGUI.Graph(key, value)            - Push a value to the graph
    *    DebugGUI.LogPersistent(key, value)    - Print a persistent log entry on screen
    *    DebugGUI.Log(value)                   - Print a temporary log entry on screen
    *    
    *   See DebugGUI.cs for more info
    * 
    * * * */

    // Disable Field Unused warning
#pragma warning disable 0414

    // User inputs, print and graph in one!
    [DebugGUIPrint, DebugGUIGraph(group: 1, r: 1, g: 0.3f, b: 0.3f)]
    float diffX;

    // User inputs, print and graph in one!
    //[DebugGUIPrint, DebugGUIGraph(group: 2, r: 1, g: 0.3f, b: 0.3f)]
    //float accelerationY;

    // User inputs, print and graph in one!
    //[DebugGUIPrint, DebugGUIGraph(group: 3, r: 1, g: 0.3f, b: 0.3f)]
    //float accelerationZ;

    [DebugGUIPrint, DebugGUIGraph(group: 5, r: 1, g: 0.3f, b: 0.3f)]
    float diffO;

    [DebugGUIPrint, DebugGUIGraph(group: 0, r: 1, g: 0.3f, b: 0.3f)]
    float step;
  
    Queue<float> deltaTimeBuffer = new();
    float smoothDeltaTime => deltaTimeBuffer.Sum() / deltaTimeBuffer.Count;

    void Awake()
    {
        // Init smooth DT
        for (int i = 0; i < 10; i++)
        {
            deltaTimeBuffer.Enqueue(0);
        }

        // Log (as opposed to LogPersistent) will disappear automatically after some time.
        DebugGUI.Log("measuring acceleration");
    }

    void Update()
    {


    }

    void FixedUpdate()
    {
        // Update smooth delta time queue
        deltaTimeBuffer.Dequeue();
        deltaTimeBuffer.Enqueue(Time.deltaTime);


        data = Connect.data;
        d = readJson(data);

        if (d.type == "android.sensor.gyroscope")
        {
            accelerationX = d.values[0];
            //accelerationY = d.values[1];
            //accelerationZ = d.values[2];

            //diffY = accelerationY - prevY;
            //diffZ = accelerationZ - prevZ;

            delta = (float.Parse(d.timestamp) - prevStampX) / 1000000000;
            if (float.Parse(d.timestamp) != prevStampX)
            {
                diffX = (accelerationX - prevX) / delta;
            }

            //total = (diffY + diffZ) * (diffY + diffZ);

            prevX = accelerationX;
            //prevY = accelerationY;
            //prevZ = accelerationZ;

            prevStampX = float.Parse(d.timestamp);

            if ((diffX > difftrig) && step != 2)
            {
                step = 1;
                tempStampX = float.Parse(d.timestamp);
            }
            if (float.Parse(d.timestamp) > (tempStampX + 1000000000) && step != 2)
            {
                step = 0;
            }

        }
        else
        {
            accelerationX = prevX;
            //accelerationY = prevY;
            //accelerationZ = prevZ;
        }




        if (d.type == "android.sensor.orientation")
        {
            if (d.values[0] > 180)
            {
                oriantation = 360 - d.values[0];
            }

            else
            {
                oriantation = d.values[0];
            }
            delta = (float.Parse(d.timestamp) - prevStampO) / 1000000000;
            if (float.Parse(d.timestamp) != prevStampO)
            {
                diffO = Math.Abs(oriantation - prevO) / delta;
            }
            prevO = oriantation;
            prevStampO = float.Parse(d.timestamp);

            if ((diffO) > diffOtrig && step!=1)
            {
                step = 2;
                tempStampO = float.Parse(d.timestamp);

            }
            if (float.Parse(d.timestamp) > (tempStampO + 1000000000) && step == 2)
            {
                step = 0;
            }

        }

        stepPublic = step;
        diffOPublic = diffO;






        //graph settings:
        if (smoothDeltaTime != 0)
        {
            DebugGUI.Graph("smoothFrameRate", 1 / smoothDeltaTime);
        }
        if (Time.deltaTime != 0)
        {
            DebugGUI.Graph("frameRate", 1 / Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(this);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log(DebugGUI.ExportGraphs());
        }




    }

    void OnDestroy()
    {

    }
}