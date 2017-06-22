using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerDestroy : MonoBehaviour {

    public SecondMarkerSpawn otherScript;

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
            otherScript.SpawnSecondMarker();
        }
    }
}
