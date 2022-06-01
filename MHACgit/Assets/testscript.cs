using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class testscript : MonoBehaviour
{
    // Start is called before the first frame update
    void awake()
    {
        // Get a list of serial port names.
        string[] ports = SerialPort.GetPortNames();

        Debug.Log("The following serial ports were found:");

        // Display each port name to the console.
        foreach (string port in ports)
        {
            Debug.Log(port);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
