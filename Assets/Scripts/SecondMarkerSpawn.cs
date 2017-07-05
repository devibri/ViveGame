using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondMarkerSpawn : MonoBehaviour
{
    // Use this for initialization
    private GameObject SecondMarker;
    private GameObject FirstMarker;
    private GameObject arrow2;
   
    Renderer rend;
    Renderer rend2;
    float firstDegree; //degree of first arrow
    float randNum; //determines how much forward the second marker spawns



    float distance;


    void Start()
    {
        this.transform.position = new Vector3(10, 0, 10);
        //get components of first and second marker
        FirstMarker = GameObject.Find("FirstMarker");
        arrow2 = GameObject.Find("ringarrow2");

        //get random number to adjust distance to second marker
        randNum = Random.Range(0, 6) / 3.28f; //divide to convert to meter amt

        //distance away second marker will be
        distance = randNum;

        rend = this.GetComponent<Renderer>();
        rend2 = arrow2.GetComponent<Renderer>();

        rend.enabled = false;
        rend2.enabled = false;



    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnSecondMarker()
    {
        rend.enabled = true;
        rend2.enabled = true;
        //changing rotation of secondmarker

        //finding the rotation of first arrow
        firstDegree = FirstMarkerSpawn.degree;

        //and setting secondarrow to the same degree
        this.transform.Rotate(0, firstDegree, 0);

        //Finding the position of the first marker and setting second marker to that positon

        this.transform.position = FirstMarker.transform.position;

        //moving second marker forward by a certain random amount
        this.transform.Translate(Vector3.back * (1.524f + distance), Space.Self); //5ft + random amt

        

    }
   
}
