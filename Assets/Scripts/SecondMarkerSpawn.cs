using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//determines the position of and places second marker 

public class SecondMarkerSpawn : MonoBehaviour
{
    // Use this for initialization
    private GameObject SecondMarker;
    private GameObject FirstMarker;
    private GameObject arrow2; //second marker's arrow
   
    //renderer for second marker and arrow 
    Renderer rend;
    Renderer rend2;

    float firstDegree; //degree of first arrow
    int randNum; //random value that determines how much forward the second marker spawns
    float length; //the amount past the base distance that the marker is moved forward
    private const string markerFile = "Marker.txt"; //for writing data
    float[] lengthArray = new float[] { 0, .5f, 1 }; //values added to base value -- distance moved forward


    void Start()
    {
        this.transform.position = new Vector3(10, 0, 10); //on initialization move second marker out of way

        //get components of first and second marker
        FirstMarker = GameObject.Find("FirstMarker");
        arrow2 = GameObject.Find("ringarrow2");

        //get random number to adjust distance to second marker
        randNum = Random.Range(0, lengthArray.Length);
        length = lengthArray[randNum] / 3.28f; //divide to convert to meter amt -- the length added to the base distance to move the second marker forward

        //renderer for the secomd marker and its arrow
        rend = this.GetComponent<Renderer>();
        rend2 = arrow2.GetComponent<Renderer>();

        //on initialization, make second marker invisible 
        rend.enabled = false;
        rend2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnSecondMarker()
    {
        //when player collides with first marker, make second marker visible 
        rend.enabled = true;
        rend2.enabled = true;

        //finding the rotation of first arrow
        firstDegree = FirstMarkerSpawn.degree;

        //and setting secondarrow to the same degree
        this.transform.Rotate(0, firstDegree, 0);

        //Finding the position of the first marker and setting second marker to that positon
        this.transform.position = FirstMarker.transform.position;

        //moving second marker forward by a certain random amount
        this.transform.Translate(Vector3.back * (1.22f + length), Space.Self); //4ft + random amt 

        //writing to text file
        System.IO.File.AppendAllText(markerFile, "First Marker:" + FirstMarker.transform.position + "\r\n");
        System.IO.File.AppendAllText(markerFile, "Second Marker:" + this.transform.position + "\r\n");
    }
   
}
