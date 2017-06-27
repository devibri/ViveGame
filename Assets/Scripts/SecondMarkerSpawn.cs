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
    int degree;
    int randNum;
    Random rand = new Random();


    void Start()
    {

        //get components of first and second marker
        SecondMarker = GetComponent<GameObject>();
        FirstMarker = GameObject.Find("FirstMarker");

        //get random number to adjust distance to second marker
       
        randNum = Random.Range(1, 4);

        //distance away second marker will be
        xRange = 0;
        zRange = 0;

        rend = GetComponent<Renderer>();
        


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnSecondMarker()
    {
        
        

        //adjusting the second arrow's ring
        arrow = GameObject.Find("ringarrow2");

        //finding the rotation of first arrow
        degree = ArrowRotate.randDegree; //GameObject.Find("ringarrow").GetComponent<ArrowRotate>().randNum;

        //and setting the second arrow to the same rotation
        arrow.transform.Rotate(0, degree, 0);

        rend2 = arrow.GetComponent<Renderer>();
        rend2.enabled = true;

        //then adjusting the position of the second marker

        //put a random number of coords away 

        while (randNum > 0)
        {
            if (degree == 0 | degree == 315 | degree == 45)
            {
                SubtractZ();
            }
            if (degree == 135 | degree == 180 | degree == 225)
            {
                AddZ();
            }
            if (degree == 45 | degree == 90 | degree == 135)
            {
                SubtractX();
            }
            if (degree == 225 | degree == 270 | degree == 315)
            {
                AddX();
            }
            randNum = randNum - 1;
        }
        


        this.transform.position = new Vector3(FirstMarker.transform.position.x + xRange, 0, FirstMarker.transform.position.z + zRange);


        rend.enabled = true;

    }

    public void SubtractZ() {
        zRange = zRange - 1;
    }

    public void AddZ() {
        zRange = zRange + 1;
    }

    public void SubtractX() {
        xRange = xRange - 1;
    }

    public void AddX() {
        xRange = xRange + 1;
    }
}
