using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*Restart file used for the training scene -- different start positions and angles*/

public class TrainingRestart : MonoBehaviour {

    //START MARKER
    int randNum;
    int randNum2;
    public int startPosition; //determines which start position was picked


    //FIRST MARKER
    //list of degree values to be added to stack 
    int[] degreeList = new int[] { 56, -56 }; //pick a degree value that is different than the actual experiment (in Restart.cs)

    //current degree value
    public static int degree; //this can be accessed by the rest of the training files

    void Start () {

        randNum = Random.Range(1, 5); //let this be the number of starting positions, min (inclusive) to max (exclusive)
        startPosition = randNum;
        
        randNum2 = Random.Range(0, 2); //determines which degree value you'll use from degreeList
        degree = degreeList[randNum2];
       
    }
	
	// Update is called once per frame
	void Update () {
        //restarts the scene on key press Space
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}
