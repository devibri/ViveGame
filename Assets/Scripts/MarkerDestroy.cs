using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (other.gameObject.CompareTag("Marker"))
        {
            System.IO.File.AppendAllText(timeFile, "Start marker time: " + Time.time + "\r\n");
            other.gameObject.SetActive(false);
            otherScript.SpawnFirstMarker();

            //otherScript2.SpawnSecondArrow();
        }
        else if (other.gameObject.CompareTag("FirstMarker"))
        {
            other.gameObject.SetActive(false);
            otherScript2.SpawnSecondMarker();
        }
        else if (other.gameObject.CompareTag("SecondMarker"))
        {
            System.IO.File.AppendAllText(timeFile, "Second marker time: " + Time.time + "\r\n");
            other.gameObject.SetActive(false);
        }

    }
}
