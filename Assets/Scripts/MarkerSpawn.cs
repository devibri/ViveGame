using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerSpawn : MonoBehaviour {
    int xRange;
    int zRange; 
    // Use this for initialization

    void Start () {
        //sets initial position of first marker
        xRange = 0;
        zRange = 0;

        while (xRange == 0 && zRange == 0) {
            xRange = Random.Range(-3, 4);
            zRange = Random.Range(-3, 4);
        } 


		this.transform.position = new Vector3(xRange, 0, zRange);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
