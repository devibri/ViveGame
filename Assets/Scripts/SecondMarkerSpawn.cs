using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondMarkerSpawn : MonoBehaviour
{
    // Use this for initialization
    private GameObject SecondMarker;
    private GameObject FirstMarker;
    private GameObject arrow;
    //private GameObject firstarrow;
    int xRange;
    int zRange;
    Renderer rend;
    Renderer rend2;
    int degree;
    
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

        //adjusting the second arrow's ring
        arrow = GameObject.Find("ringarrow2");

        //finding the rotation of first arrow
        degree = ArrowRotate.randDegree; //GameObject.Find("ringarrow").GetComponent<ArrowRotate>().randNum;

        //and setting the second arrow to the same rotation
        arrow.transform.Rotate(0, degree, 0);

        rend2 = arrow.GetComponent<Renderer>();
        rend2.enabled = true;

       
        rend.enabled = true;

    }
}
