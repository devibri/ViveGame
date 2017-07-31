using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingFirstMarkerSpawn : MonoBehaviour {
    
    //FOR TRAINING SCENE ONLY -- moves the first marker to appropriate spot and rotation on contact 


    private GameObject FirstMarker;
    private GameObject StartMarker;
    private GameObject GameController;
    private GameObject arrow;

    Renderer rend; //renderer for FirstMarker
    Renderer rend2; //renderer for FirstArrow
    int startDegree; //degree of start arrow
    public static float degree; //degree of first arrow


    void Start ()
    {
        //initializes marker in spot that can be ignored
        this.transform.position = new Vector3(10, 0, 10);

        /*initializing stuff*/

        //start marker
        StartMarker = GameObject.Find("StartMarker");
        TrainingStartMarkerSpawn StartMarkerScript = StartMarker.GetComponent<TrainingStartMarkerSpawn>();
        
        //main game controller 
        GameController = GameObject.Find("GameController");
        TrainingRestart RestartScript = GameController.GetComponent<TrainingRestart>();
        
        //firstmarker's arrow
        arrow = GameObject.Find("ringarrow");

        //renderer for firstmarker and its arrow
        rend = this.GetComponent<Renderer>();
        rend2 = arrow.GetComponent<Renderer>();

        //make firstmarker invisible on start, until startmarker collision
        rend.enabled = false;
        rend2.enabled = false; 
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    //on collision with startmarker, make firstmarker visible and set it to appropriate position / rotation 
    public void SpawnFirstMarker()
    {
        //make firstmarker and its arrow appear
        rend.enabled = true;
        rend2.enabled = true;

        //temporarily set firstmarker position to position of startmarker 
        this.transform.position = StartMarker.transform.position;

        //finding the rotation of start marker
        startDegree = TrainingStartMarkerSpawn.degree;

        //temporarily setting first marker to same degree
        this.transform.Rotate(0, startDegree, 0);

        //moves the marker towards origin by 5ft
        this.transform.Translate(Vector3.back * (1.52f), Space.Self); 

        //changing rotation of firstmarker to the same rotation plus or minus a certain amount
        degree = startDegree + TrainingRestart.degree;
        this.transform.Rotate(0, this.transform.rotation.y + TrainingRestart.degree, 0);
    }
}
