using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.IO.Ports;

public class ArduinoReadThread : MonoBehaviour
{
    //https://forum.unity.com/threads/unity3d-arduino-read-serial-in-unity3d.45477/
    //https://stackoverflow.com/questions/60027732/using-arduino-as-controller-in-unity-is-giving-me-bad-framerate

    public static string comPort = " ";
    SerialPort stream = new SerialPort(comPort, 115200);

    public string receivedstring;

    public int recButton;
    public int zoominButton;
    public int zoomoutButton;
    public int joyX;
    public int joyY;
    public float x;
    public float y;
    public float z;
    public int timer;


    void Start()
    {
        //stream.ReadTimeout = 100;

        stream.Open();  // Uw serialpoort openen
        Thread sampleThread = new Thread(new ThreadStart(sampleFunction));
        sampleThread.IsBackground = true;
        sampleThread.Start();
    }

    public void SetComport(string _comport)
    {
        comPort = _comport;
    }


    void Update()
    {

    }

    public void sampleFunction()
    {
        while (true)
        {

            if (stream.IsOpen)
            {
                try
                {

                    receivedstring = stream.ReadLine();

                    Debug.Log(receivedstring);
                    string[] datas = receivedstring.Split(',');
                    if (datas[0] != "" && datas[1] != "" && datas[2] != "" && datas[3] != "" && datas[4] != "" && datas[5] != "" && datas[6] != "" && datas[7] != "" && datas[8] != "")
                    {
                        zoominButton = int.Parse(datas[0]);  //zoominButton 
                        zoomoutButton = int.Parse(datas[1]); //zoomoutButton
                        recButton = int.Parse(datas[2]);     //recordButton

                        joyX = int.Parse(datas[3]); //joystickX
                        joyY = int.Parse(datas[4]); //joystickY

                        x = float.Parse(datas[5]) / 100;
                        y = float.Parse(datas[6]) / 100;
                        z = float.Parse(datas[7]) / 100;

                        timer = int.Parse(datas[8]);

                        Debug.Log(recButton + " " + zoominButton + " " + zoomoutButton + " " + joyX + " " + joyY + " " + x + " " + y + " " + z + " " + timer);
                    }
                }
                catch (System.Exception)
                {

                }
            }
        }
    }
}
