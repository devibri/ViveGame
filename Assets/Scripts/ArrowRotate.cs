using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotate : MonoBehaviour {
    public static int randNum = 0;
    public static int randDegree = 0; 

    Random rand = new Random();
    //ArrayList angleArray = new ArrayList();

    int[] angleArray = new int[] { 0, 45, 90, 135, 180, 225, 270, 315 };
    //int randNum;


    // Use this for initialization
    void Start () {
        randNum = Random.Range(0, 9);
        randDegree = angleArray[randNum];
        this.transform.Rotate(0, randDegree, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
