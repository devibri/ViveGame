using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Deals with player collsion with markers
 * Will take appropriate action on collision (usually disappearing collided marker and spawning next one)
 */ 

public class MarkerDestroy : MonoBehaviour {

    public FirstMarkerSpawn otherScript;
    public SecondMarkerSpawn otherScript2;
    const string timeFile = "Time.txt";

    // Use this for initialization
    void Start () {
        //if (System.IO.File.Exists(timeFile))
        //{
        //    System.IO.File.Delete(timeFile);
        //}
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void OnTriggerEnter(Collider other)
    {
        //if the cube (moving with the player) collides with the start marker
        if (other.gameObject.CompareTag("Marker"))
        {
            System.IO.File.AppendAllText(timeFile, "Start marker time: " + Time.time + "\r\n"); //make a note of that in the data output
            other.gameObject.SetActive(false); //make start marker disappear
            otherScript.SpawnFirstMarker(); //call on first marker script to spawn the first marker

            //otherScript2.SpawnSecondArrow();
        }

        //if the player collides with the first marker, make it disappear and spawn the second marker
        else if (other.gameObject.CompareTag("FirstMarker"))
        {
            other.gameObject.SetActive(false);
            otherScript2.SpawnSecondMarker();
        }
        //if the player collides with the second marker, make it disappear and write that to file
        else if (other.gameObject.CompareTag("SecondMarker"))
        {
            System.IO.File.AppendAllText(timeFile, "Second marker time: " + Time.time + "\r\n");
            other.gameObject.SetActive(false);
        }

    }
}
