using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondArrowSpawn : MonoBehaviour
{
    // Use this for initialization
    GameObject arrow;// rend;

    void Start()
    {

        arrow = GetComponent<GameObject>();
        arrow.SetActive(false);// = false;



    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnSecondArrow()
    {

        //this.transform.position = new Vector3(FirstMarker.transform.position.x + xRange, 0, FirstMarker.transform.position.z + zRange);


        //SecondMarker.GetComponent<Renderer>().enabled = true;
        arrow.SetActive(true);

    }
}
