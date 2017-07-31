using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * FOR TRAINING SCENE -- 
 * determines position and spawns the second marker
 */

public class TrainingSecondMarkerSpawn : MonoBehaviour
{
    // Use this for initialization
    private GameObject SecondMarker;
    private GameObject FirstMarker;
    private GameObject arrow2;
   
    //renderer for second marker and its arrow
    Renderer rend;
    Renderer rend2;

    float firstDegree; //degree of first arrow
    float randNum; //determines how much forward the second marker spawns
    private const string markerFile = "Marker.txt"; //to write to file 

    void Start()
    {
        this.transform.position = new Vector3(10, 0, 10); //on initialization, move marker out of the way
        //get components of first and second marker
        FirstMarker = GameObject.Find("FirstMarker");
        arrow2 = GameObject.Find("ringarrow2");

        //get random number to adjust distance added to base value to move second marker
        randNum = Random.Range(0, 3) / 3.28f; //divide to convert to meter amt

        //renderer for secomd marker and its arrow
        rend = this.GetComponent<Renderer>();
        rend2 = arrow2.GetComponent<Renderer>();

        //on initialization, make secomd marker and its arrow invisible
        rend.enabled = false;
        rend2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }


    /* 
     * When player collides with first marker, place second marker at appropriate distance away 
     */

    public void SpawnSecondMarker()
    {
        //make second marker and its arrow visible
        rend.enabled = true;
        rend2.enabled = true;

        //finding the rotation of first arrow
        firstDegree = TrainingFirstMarkerSpawn.degree;

        //and setting secondarrow to the same degree
        this.transform.Rotate(0, firstDegree, 0);

        //Finding the position of the first marker and setting second marker to that positon
        this.transform.position = FirstMarker.transform.position;

        //moving second marker forward by a certain random amount
        this.transform.Translate(Vector3.back * (1.22f), Space.Self); //4ft + random amt

        //appends to data file 
        System.IO.File.AppendAllText(markerFile, "First Marker:" + FirstMarker.transform.position + "\r\n");
        System.IO.File.AppendAllText(markerFile, "Second Marker:" + this.transform.position + "\r\n");



    }
   
}
