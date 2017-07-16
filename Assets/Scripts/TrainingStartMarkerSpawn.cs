using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script positions the first marker at the starting marker position. 

public class TrainingStartMarkerSpawn : MonoBehaviour {
    
    float xCoord;
    float zCoord;
    public static int degree;
    private GameObject GameController;
    private const string markerFile = "Marker.txt";
    private const string timeFile = "Time.txt";


    // Use this for initialization
    void Start() {

        //main game controller 
        GameController = GameObject.Find("GameController");
        TrainingRestart RestartScript = GameController.GetComponent<TrainingRestart>();


        xCoord = 0;
        zCoord = 0;
        

        //depending on random roll, set start marker to start position and rotation towards origin
        if (RestartScript.startPosition == 1)
        {
            xCoord = -1.52f;
            degree = 270;

        }
        else if (RestartScript.startPosition == 2)
        {
            xCoord = -1.52f;
            degree = 270;
        }
        else if (RestartScript.startPosition == 3)
        {
            xCoord = 1.52f;
            degree = 90;
        }
        else if (RestartScript.startPosition == 4)
        {
            xCoord = 1.52f;
            degree = 90;
        }
        

        this.transform.position = new Vector3(xCoord, 0, zCoord);
        this.transform.Rotate(0, degree, 0);
        

        if (System.IO.File.Exists(markerFile))
        {
            System.IO.File.Delete(markerFile);
        }
        if (System.IO.File.Exists(timeFile))
        {
            System.IO.File.Delete(timeFile);
        }
        System.IO.File.AppendAllText(markerFile, "Starting Marker:" + this.transform.position + "\r\n");
        System.IO.File.AppendAllText(timeFile, "Start time:" + Time.time + "\r\n");

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
