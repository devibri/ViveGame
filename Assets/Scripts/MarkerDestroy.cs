using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerDestroy : MonoBehaviour {

    public FirstMarkerSpawn otherScript;
    public SecondMarkerSpawn otherScript2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Marker"))
        {
            other.gameObject.SetActive(false);
            otherScript.SpawnFirstMarker();

            //otherScript2.SpawnSecondArrow();
        }
        else if (other.gameObject.CompareTag("FirstMarker"))
        {
            other.gameObject.SetActive(false);
            otherScript2.SpawnSecondMarker();
        }
        else if (other.gameObject.CompareTag("SecondMarker")) {
            other.gameObject.SetActive(false);
        }

    }
}
