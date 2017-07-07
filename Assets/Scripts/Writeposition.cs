using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Writeposition : MonoBehaviour
{

    const string outputFile = "Marker.txt";
    // Use this for initialization
    void Start()
    {
        if (System.IO.File.Exists(outputFile))
        {
            System.IO.File.Delete(outputFile);
        }
        System.IO.File.AppendAllText(outputFile, gameObject.name + GetComponent<Transform>().position + " " + GetComponent<Transform>().eulerAngles.ToString() + "\r\n");

    }
}
