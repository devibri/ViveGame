using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotate : MonoBehaviour {
    public static int randNum;
    public static int randDegree;
    GameObject marker;
    float markerX;
    float markerZ;

    //Random rand = new Random();
    //ArrayList angleArray = new ArrayList();

    int[] angleArray = new int[] { 0, 45, 90, 135, 180, 225, 270, 315 };
    //int randNum;


    // Use this for initialization
    void Start () {
        
        //if marker is in a certain position, prevent degree from being such that it points back to the origin
        marker = GameObject.Find("FirstMarker");
        markerX = marker.transform.position.x;
        markerZ = marker.transform.position.z;

        if (((marker.transform.position.z < 0) && (marker.transform.position.x == 0)) || ((markerZ > 0) && (markerX == 0)))
        {
            GetRandDegree(180, 0);
        }
        else if (((markerZ < 0) && (markerX < 0)) || ((markerZ > 0) && (markerX > 0)))
        {
            GetRandDegree(225, 45);
        }
        else if (((markerZ == 0) && (markerX < 0)) || ((markerZ == 0) && (markerX > 0)))
        {
            GetRandDegree(270, 90);
        }
        else if (((markerZ > 0) && (markerX < 0)) || ((markerZ < 0) && (markerX > 0)))
        {
            GetRandDegree(315, 135);
        }

        this.transform.Rotate(0, randDegree, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetRandDegree(int wrongDegree, int wrongDegree2) {
        do
        {
            randNum = Random.Range(0, 8);
            randDegree = angleArray[randNum];
        } while (randDegree == wrongDegree || randDegree == wrongDegree2);




    }
}
