using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMarkerSpawn : MonoBehaviour {
    
    private GameObject FirstMarker;
    private GameObject StartMarker;
    private GameObject GameController;
    private GameObject arrow;

    Renderer rend; //renderer for FirstMarker
    Renderer rend2; //renderer for FirstArrow
    int startDegree; //degree of start arrow
    public static float degree; //degree of first arrow
    float currentRotation;
    float randNum; //determines which random length is picked
    //float randNum2; //determines which random degree is picked

    //int[] degreeChange = new int[] { -70, -42, -14, 14, 42, 70 }; //degrees plus or minus the current heading -- change this to change angles of triangles 



    void Start () {
        this.transform.position = new Vector3(10, 0, 10);

        

        //how much distance the first marker will change by, randomly
        randNum = Random.Range(0, 3) / 3.28f; //divide to convert to meter amt
        //randNum2 = Random.Range(0, 6); //number of options for degree picked 


        //start marker
        StartMarker = GameObject.Find("StartMarker");
        StartMarkerSpawn StartMarkerScript = StartMarker.GetComponent<StartMarkerSpawn>();

        //main game controller 
        GameController = GameObject.Find("GameController");
        Restart RestartScript = GameController.GetComponent<Restart>();
        //RestartScript.startPosition = StartMarkerScript.randNum; //number of options for degrees picked 

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

        //finding the rotation of start marker
        startDegree = StartMarkerSpawn.degree; 

        //temporarily setting first marker to same degree
        this.transform.Rotate(0, startDegree, 0);
        //Debug.Log("StartDegree: " + startDegree);

        

        //now moving firstmarker forward given amount -- setting position to other marker, then moving it forward
        this.transform.position = StartMarker.transform.position;

        this.transform.Translate(Vector3.back * (1.22f + randNum), Space.Self); //5ft + random amt


        //changing rotation of firstmarker to the same rotation plus or minus a certain amount
        degree = startDegree + Restart.degree;
        //Debug.Log("Degree: " + degree);
        this.transform.Rotate(0, this.transform.rotation.y + Restart.degree, 0);


    }
}
