using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainingRestart : MonoBehaviour {

    //START MARKER
    int randNum;
    int randNum2;
    public int startPosition; //determines which start position was picked
    //static int prevPosition; //determines what the starting position was for last round 


    //FIRST MARKER
    //list of degree values to be added to stack 
    //static List<int> degreeList = new List<int> { -90, 90 };
    int[] degreeList = new int[] { -90, 90 };
    //stack of degree values
    //static Stack<int> degreeStack = new Stack<int>();

    //current degree value
    public static int degree;

    void Start () {
        //makes sure no start positions are ever chosen twice in a row
//        do
//        {
//            
//        } while (randNum == prevPosition);

        randNum = Random.Range(1, 5); //let this be the number of starting positions, min (inclusive) to max (exclusive)
        startPosition = randNum;
        //prevPosition = randNum;
        
        randNum2 = Random.Range(0, 2);
        degree = degreeList[randNum2];
        
        //print("Degree: " + degree);
       
    }
	
	// Update is called once per frame
	void Update () {
        //restarts the scene on key press Space
        if (Input.GetKeyDown("space"))
        {
            //Debug.Log("Space");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}
