using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class stepCounterScript : MonoBehaviour
{
    public class Data
    {
        public List<float> values;
        public string timestamp;
        public string accuracy;
        public string type;
    }
    public string data;
    public Connection Connect;
    public Data d;
    public float accelerationX;
    public float accelerationY;
    public float accelerationZ;

    public float b1;
    public float b2;
    public float b3;
    public float b4;
    public float b5;

    public bool step;

    public float temp1;
    public float temp2;

    public bool i = false;
    public static Data readJson(string data)
    {
        return JsonUtility.FromJson<Data>(data);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        data = Connect.data;
        d = readJson(data);
        if (d.type == "android.sensor.accelerometer")
        {
            b5 = b4;
            b4 = b3;
            b3 = b2;
            b2 = b1;

            
            accelerationX = d.values[0];
            accelerationY = d.values[1];
            accelerationZ = d.values[2];

            float x = Mathf.Sqrt(accelerationX * accelerationX + accelerationY * accelerationY + accelerationZ * accelerationZ);
            b1 = x;

            float b = (b1 + b2 + b3 + b4 + b5) / 5;

            if (b > 11)
            {
                step = true;
                temp1 =float.Parse(d.timestamp);
            }
            if (b < 10 && step==true && i == false)
            {
                i= true;
                temp2 =float.Parse(d.timestamp);
            }

            if (i==true && float.Parse(d.timestamp) > temp1+1000000000 && float.Parse(d.timestamp) > temp2 + 500000000 && b<10)
            {
                step = false;
                i = false;
            }
        }
    }
}
