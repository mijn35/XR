using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class Data
    {
        public List<float> values;
        public string timestamp;
        public string accuracy;
    }

public class Accelero_debugger : MonoBehaviour
{
    public string data;
    //public Connection Connect;
    public Data d;
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
    float acceleration;

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
        // Update smooth delta time queue
        deltaTimeBuffer.Dequeue();
        deltaTimeBuffer.Enqueue(Time.deltaTime);


       // data=Connect.data;
        d = readJson(data);
        acceleration = d.values[0];

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
            Debug.Log(DebugGUI.ExportGraphs());
        }
    }

    void FixedUpdate()
    {

    }

    void OnDestroy()
    {

    }
}