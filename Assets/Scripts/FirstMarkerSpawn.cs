using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Deals with the position of the first marker
 */

public class FirstMarkerSpawn : MonoBehaviour {
    
    private GameObject FirstMarker;
    private GameObject StartMarker;
    private GameObject GameController;
    private GameObject arrow; //the directional arrow of the first marker

    Renderer rend; //renderer for FirstMarker
    Renderer rend2; //renderer for FirstArrow
    int startDegree; //degree of start arrow
    public static float degree; //degree of first arrow
    //float currentRotation;
    int randNum; //determines which random length is picked
    float length; //the length of the path from the start to the first marker
    float[] lengthArray = new float[] { 0, .5f, 1 };  //determines the amount (meter) that will be added to the base distance 


    void Start ()
    {
        //when the marker is initialized, move it somewhere where it won't be seen or be in the way
        this.transform.position = new Vector3(10, 0, 10);

        //determines the random amount that will be added to the base distance from start marker to first marker
        randNum = Random.Range(0, lengthArray.Length);
        length = lengthArray[randNum] / 3.28f; //divide to convert to meter amt   

        
        /*initialize important things*/

        //start marker
        StartMarker = GameObject.Find("StartMarker");
        StartMarkerSpawn StartMarkerScript = StartMarker.GetComponent<StartMarkerSpawn>();
        
        //main game controller 
        GameController = GameObject.Find("GameController");
        Restart RestartScript = GameController.GetComponent<Restart>();

        //firstmarker's arrow
        arrow = GameObject.Find("ringarrow");

        //renderer for first marker and its arrow
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


    /*
     * called when player collides with start marker in Collide.cs
     * makes first marker appear and puts it at the appropriate length away from start marker
     * and orients it according to the degree given to it by Restart.cs
     */
    public void SpawnFirstMarker()
    {
        
        //make marker and its arrow appear
        rend.enabled = true;
        rend2.enabled = true;

        //finding the rotation of start marker
        startDegree = StartMarkerSpawn.degree; 

        //temporarily setting first marker to same degree of start marker
        this.transform.Rotate(0, startDegree, 0);

        // temporarily setting first marker to same position of start marker
        this.transform.position = StartMarker.transform.position;

        // now moving firstmarker forward given amount
        this.transform.Translate(Vector3.back * (1.524f + length ), Space.Self); //4ft + random amt


        //changing rotation of firstmarker to the same rotation plus or minus a certain amount
        degree = startDegree + Restart.degree;
        this.transform.Rotate(0, this.transform.rotation.y + Restart.degree, 0);


    }
}
