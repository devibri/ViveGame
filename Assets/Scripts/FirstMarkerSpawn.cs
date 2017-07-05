using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMarkerSpawn : MonoBehaviour {
    
    private GameObject FirstMarker;
    private GameObject StartMarker;
    private GameObject arrow;

    Renderer rend; //renderer for FirstMarker
    Renderer rend2; //renderer for FirstArrow
    int startDegree; //degree of start arrow
    public static float degree; //degree of first arrow
    float currentRotation;
    float randNum; //determines which random length is picked
    float randNum2; //determines which random degree is picked
    
    int startPosition; //determines which start position was picked
    int[] degreeChange = new int[] {-70, -42, -14, 14, 42, 70}; //degrees plus or minus the current heading -- change this to change angles of triangles 

    void Start () {
        this.transform.position = new Vector3(10, 0, 10);

        //how much distance the first marker will change by, randomly
        randNum = Random.Range(0, 6) / 3.28f; //divide to convert to meter amt
        randNum2 = Random.Range(0, 6); //number of options for degree picked 


        //start marker
        StartMarker = GameObject.Find("StartMarker");
        StartMarkerSpawn StartMarkerScript = StartMarker.GetComponent<StartMarkerSpawn>();
        startPosition = StartMarkerScript.randNum; //number of options for degrees picked 

        //firstmarker's arrow
        arrow = GameObject.Find("ringarrow");


        rend = this.GetComponent<Renderer>();
        rend2 = arrow.GetComponent<Renderer>();

        //make firstmarker invisible on start, until startmarker collision
        rend.enabled = false;
        rend2.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnFirstMarker()
    {
        

        rend.enabled = true;
        rend2.enabled = true;

        //changing first marker position depending on starting position  --  5ft + random amount
        if (startPosition == 1)
        {
            this.transform.position = new Vector3(1.524f + randNum, 0, 0);
        }
        else if (startPosition == 2)
        {
            this.transform.position = new Vector3(0, 0, -1.524f - randNum);
        }
        else if (startPosition == 3)
        {
            this.transform.position = new Vector3(-1.524f - randNum, 0, 0);
        }
        else {
            this.transform.position = new Vector3(0, 0, 1.524f + randNum);
        }



        //changing rotation of firstmarker

        //finding the rotation of start arrow
        startDegree = StartMarkerSpawn.degree;

        degree = startDegree + degreeChange[(int)randNum2];

        //and setting the second arrow to the same rotation plus or minus a certain amount
        this.transform.Rotate(0, degree, 0);

    }
}
