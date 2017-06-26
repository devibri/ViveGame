using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondMarkerSpawn : MonoBehaviour
{
    // Use this for initialization
    private GameObject SecondMarker;
    private GameObject FirstMarker;
    private GameObject arrow;
    int xRange;
    int zRange;
    Renderer rend;
    Renderer rend2;
    
    void Start()
    {


        SecondMarker = GetComponent<GameObject>();

        FirstMarker = GameObject.Find("FirstMarker");

        xRange = Random.Range(-3, 4);
        zRange = Random.Range(-3, 4);

        rend = GetComponent<Renderer>();
        


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnSecondMarker()
    {
        
        this.transform.position = new Vector3(FirstMarker.transform.position.x + xRange, 0, FirstMarker.transform.position.z + zRange);

        arrow = GameObject.Find("ringarrow2");
        rend2 = arrow.GetComponent<Renderer>();
        rend2.enabled = true;

        //SecondMarker.GetComponent<Renderer>().enabled = true;
        rend.enabled = true;

    }
}
