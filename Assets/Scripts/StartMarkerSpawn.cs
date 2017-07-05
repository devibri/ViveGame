using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script positions the first marker at the starting marker position. 

public class StartMarkerSpawn : MonoBehaviour {
    public int randNum; //randomly pick starting position
    float xCoord;
    float zCoord;
    public static int degree;


    // Use this for initialization
    void Start() {
        xCoord = 0;
        zCoord = 0;

        randNum = Random.Range(1, 9); //let this be the number of starting positions, min (inclusive) to max (exclusive)

        //depending on random roll, set start marker to start position and rotation towards origin
        if (randNum == 1)
        {
            xCoord = -1.83f;
            degree = 270;

        }
        else if (randNum == 2)
        {
            zCoord = 2.44f;
            degree = 0;
        }
        else if (randNum == 3)
        {
            xCoord = 1.83f;
            degree = 90;
        }
        else if (randNum == 4)
        {
            zCoord = -2.44f;
            degree = 180;
        }
        else if (randNum == 5)
        {
            xCoord = -1.83f;
            zCoord = 2.44f;
            degree = 315;
        }
        else if (randNum == 6)
        {
            xCoord = 1.83f;
            zCoord = 2.44f;
            degree = 45;
        }
        else if (randNum == 7)
        {
            xCoord = 1.83f;
            zCoord = -2.44f;
            degree = 135;
        }
        else { //randnum == 8
            xCoord = -1.83f;
            zCoord = -2.44f;
            degree = 225;
        }

        this.transform.Rotate(0, degree, 0);
        this.transform.position = new Vector3(xCoord, 0, zCoord);
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
