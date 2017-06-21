using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerSpawn : MonoBehaviour {
    public float distance1 = 1;
    public float distance2 = 2;
    public float distance3 = 3;

	// Use this for initialization
	void Start () {

        //sets initial position of marker
        Random rand = new Random();
        int distance = Random.Range(0, 3);
		this.transform.position = new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
