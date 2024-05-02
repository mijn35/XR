using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class CubeBehav : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public List<float> values;
        public string timestamp;
        public string accuracy;
        public string type;
    }
    // Start is called before the first frame update
    public string data;
    public Connection Connect;
    public Data d;
    public float a;

    void Start()
    {
        data = Connect.data;
    }

    public static Data readJson(string data)
    {
        return JsonUtility.FromJson<Data>(data);
    }

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        data = Connect.data;
        d = readJson(data);
        if (d.type == "android.sensor.orientation")
        {
            transform.rotation = Quaternion.Euler(0f, d.values[0], 0f);
        }

    }
}
