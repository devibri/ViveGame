using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

    //START MARKER
    int randNum;
    public int startPosition; //determines which start position was picked
    static int prevPosition; //determines what the starting position was for last round
    string[] fileList = { "Marker.txt", "Place.txt", "Response.txt", "Time.txt", "Track.txt" };


    //FIRST MARKER
    //list of degree values to be added to stack 
    static List<float> degreeList = new List<float> { 135, 112.5f, 90, 67.5f, 45, 22.5f, -22.5f, -45, -65.5f, -90, -112.5f, -135 };
    //stack of degree values
    static Stack<float> degreeStack = new Stack<float>();

    //current degree value
    public static float degree;
    public AudioSource end;

    void Start () {
        //makes sure no start positions are ever chosen twice in a row
        do
        {
            randNum = Random.Range(1, 9); //let this be the number of starting positions, min (inclusive) to max (exclusive)
        } while (randNum == prevPosition);

        startPosition = randNum;
        prevPosition = randNum;


        //makes sure each angle is picked twice only

        //puts each degree value into stack 
        for (int j = 0; j < degreeList.Count; j++) {
            int index = Random.Range(0, degreeList.Count - 1); //pick a random item from the list of degrees
            degreeStack.Push(degreeList[index]); //add that degree to the stack of degrees
            degreeList.RemoveAt(index);
        }

        try
        {
            //for every restart, pops a value from the stack 
            degree = degreeStack.Pop();
       }
        catch (System.Exception e)
        {
            GetComponent<AudioSource>().Play();
        }
        //Debug.Log("Degree stack elements left: " + degreeStack.Count);
        //print("Degree: " + degree);

    }

    // Update is called once per frame
    void Update () {
        //restarts the scene on key press Space
        if (Input.GetKeyDown("space"))
        {
            //Debug.Log("Space");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            foreach(string file in fileList)
            {
                System.IO.File.AppendAllText(file, "-------New Trial--------\r\n");
            }
        }

    }
}
