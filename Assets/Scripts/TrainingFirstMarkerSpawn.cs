using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingFirstMarkerSpawn : MonoBehaviour {
    
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



    void Start ()
    {
        
        this.transform.position = new Vector3(10, 0, 10);

        

        //how much distance the first marker will change by, randomly
        randNum = Random.Range(0, 3) / 3.28f; //divide to convert to meter amt

        //start marker
        StartMarker = GameObject.Find("StartMarker");
        TrainingStartMarkerSpawn StartMarkerScript = StartMarker.GetComponent<TrainingStartMarkerSpawn>();
        
        //main game controller 
        GameController = GameObject.Find("GameController");
        TrainingRestart RestartScript = GameController.GetComponent<TrainingRestart>();
        

        //firstmarker's arrow
        arrow = GameObject.Find("ringarrow");


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

    public void SpawnFirstMarker()
    {
        

        rend.enabled = true;
        rend2.enabled = true;

        //now moving firstmarker forward given amount -- setting position to other marker, then moving it forward
        this.transform.position = StartMarker.transform.position;

        //finding the rotation of start marker
        startDegree = TrainingStartMarkerSpawn.degree;

        //temporarily setting first marker to same degree
        this.transform.Rotate(0, startDegree, 0);



        this.transform.Translate(Vector3.back * (.914f + randNum), Space.Self); //3ft + random amt


        
        

        //changing rotation of firstmarker to the same rotation plus or minus a certain amount
        degree = startDegree + TrainingRestart.degree;
        //
        this.transform.Rotate(0, this.transform.rotation.y + TrainingRestart.degree, 0);


    }
}
