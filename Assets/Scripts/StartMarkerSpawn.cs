using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script positions the starting marker at the starting position of the triangle, using the start position given to it by Restart.cs

public class StartMarkerSpawn : MonoBehaviour {
    
    float xCoord; //x coordinate of startmarker
    float zCoord; //z coordinate of startmarker
    public static int degree; //degree of the triangle
    private GameObject GameController; //used to handle restarting the game
    //used to write data to files
    private const string markerFile = "Marker.txt";
    private const string timeFile = "Time.txt";


    // Use this for initialization
    void Start() {

        //main game controller 
        GameController = GameObject.Find("GameController");
        Restart RestartScript = GameController.GetComponent<Restart>(); 


        xCoord = 0;
        zCoord = 0;

        /* OLD COORD SYSTEM -- places the start marker around the sides and corners of the walls with a 2ft clearance. Seemed a bit close, but feel free to use it / test it 

        //depending on random roll, set start marker to start position and rotation towards origin
        if (RestartScript.startPosition == 1)
        {
            xCoord = -1.828f;
            degree = 270;

        }
        else if (RestartScript.startPosition == 2)
        {
            zCoord = 2.44f;
            degree = 0;
        }
        else if (RestartScript.startPosition == 3)
        {
            xCoord = 1.828f;
            degree = 90;
        }
        else if (RestartScript.startPosition == 4)
        {
            zCoord = -2.44f;
            degree = 180;
        }
        else if (RestartScript.startPosition == 5)
        {
            xCoord = -1.828f;
            zCoord = 2.44f;
            degree = 315;
        }
        else if (RestartScript.startPosition == 6)
        {
            xCoord = 1.828f;
            zCoord = 2.44f;
            degree = 45;
        }
        else if (RestartScript.startPosition == 7)
        {
            xCoord = 1.828f;
            zCoord = -2.44f;
            degree = 135;
        }
        else { //randnum == 8
            xCoord = -1.828f;
            zCoord = -2.44f;
            degree = 225;
        }*/

        

        //depending on random num picked in Restart.cs, set start marker to start position and rotation towards origin -- places markers 
        // at sides and corners of room with 3ft clearance -- corners are pushed in a bit more, 4ft clearance

        /*
         * Marker start positions in room based on starting position #
         *  _______________________
         * |           1           | -
         * |    8             5    |
         * | 4      <-(0,0)      2 |      //<-(0, 0) represents origin and starting rotation          
         * |     7           6     |
         * |___________3___________| +  
         *  -                      +
         */ 

        if (RestartScript.startPosition == 1)
        {
            xCoord = -1.52f;
            degree = 270;

        }
        else if (RestartScript.startPosition == 2)
        {
            zCoord = 2.13f;
            degree = 0;
        }
        else if (RestartScript.startPosition == 3)
        {
            xCoord = 1.52f;
            degree = 90;
        }
        else if (RestartScript.startPosition == 4)
        {
            zCoord = -2.13f;
            degree = 180;
        }
        else if (RestartScript.startPosition == 5)
        {
            xCoord = -1.22f;
            zCoord = 1.83f;
            degree = 315;
        }
        else if (RestartScript.startPosition == 6)
        {
            xCoord = 1.22f;
            zCoord = 1.83f;
            degree = 45;
        }
        else if (RestartScript.startPosition == 7)
        {
            xCoord = 1.22f;
            zCoord = -1.83f;
            degree = 135;
        }
        else { //randnum == 8
            xCoord = -1.22f;
            zCoord = -1.83f;
            degree = 225;
        }

        //sets the starting marker to the appropriate rotation and coordinates according the the above "if statement" so it faces the origin
        this.transform.Rotate(0, degree, 0);
        this.transform.position = new Vector3(xCoord, 0, zCoord);

        System.IO.File.AppendAllText(markerFile, "Starting Marker:" + this.transform.position + "\r\n");
        System.IO.File.AppendAllText(timeFile, "Start time:" + Time.time + "\r\n");

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
