using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondMarkerSpawn : MonoBehaviour
{
    // Use this for initialization
    private GameObject SecondMarker;
    private GameObject FirstMarker;
    int xRange;
    int zRange;
    
    void Start()
    {


        SecondMarker = GetComponent<GameObject>();

        FirstMarker = GameObject.Find("FirstMarker");

        xRange = Random.Range(-3, 4);
        zRange = Random.Range(-3, 4);


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Space)) {

            this.transform.position = new Vector3(FirstMarker.transform.position.x + xRange, 0, FirstMarker.transform.position.z + zRange);
                //Random.Range(-3, 4), 0, Random.Range(-3, 4));
           
       }

    }
}
