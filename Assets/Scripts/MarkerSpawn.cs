using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerSpawn : MonoBehaviour {
    // Use this for initialization

    void Start () {
        //sets initial position of first marker
		this.transform.position = new Vector3(Random.Range(-3, 4), 0, Random.Range(-3, 4));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
