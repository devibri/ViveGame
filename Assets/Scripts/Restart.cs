using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * 
 * RESTART -- Deals with resetting the scene when the user presses Spacebar
 * and handling the array of angles for the triangles. 
 * 
 * 
 */


public class Restart : MonoBehaviour {

    //START MARKER
    int randNum; //determines which initial / new start position is picked
    public int startPosition; //keeps track of which start position was picked 
    static int prevPosition; //determines what the starting position was for last round


    string[] fileList = { "Marker.txt", "Place.txt", "Response.txt", "Time.txt", "Track.txt" }; //used for tracking data output files


    //FIRST MARKER
    //list of degree values that the triangle can take -- add to this list to add more angles. 
    static List<float> degreeList = new List<float> { 135, 112.5f, 90, 67.5f, 45, 22.5f, -22.5f, -45, -65.5f, -90, -112.5f, -135 };
    //stack of degree values
    static Stack<float> degreeStack = new Stack<float>();

    //current degree value
    public static float degree;
    public AudioSource end;

    void Start () {
        
        /*
         * PICKING START POSITION
         * picks a start position based on a random number, never pick same start twice
         * 
         */

        do
        {
            randNum = Random.Range(1, 9); //let this be the number of starting positions, min (inclusive) to max (exclusive)
        } while (randNum == prevPosition); //makes sure no start positions are ever chosen twice in a row

        //sets the 
        startPosition = randNum; //allows other scripts to see what the current start position is
        prevPosition = randNum; //makes sure this position isn't picked next restart


        /*
         * PICKING ANGLE
         * Determines the angle of the triangle -- that is, how the first marker will be oriented 
         * relative to the start marker
         * makes sure each angle is picked twice only
         */

        //puts each degree value into stack in a random order 
        for (int j = 0; j < degreeList.Count; j++) {
            int index = Random.Range(0, degreeList.Count - 1); //pick a random item from the list of degrees
            degreeStack.Push(degreeList[index]); //add that degree to the stack of degrees
            degreeList.RemoveAt(index); //take it out fo the arraylist
        }

        //takes a new degree for the current degree value and takes it out of the stack, once you run out play a sound 
        try
        {
            //for every restart, pops a value from the stack 
            degree = degreeStack.Pop();
       }
        catch (System.Exception e) //once all the angles have been removed, play a sound
        {
            GetComponent<AudioSource>().Play();
        }
    }

    // Update is called once per frame
    void Update () {

        //restarts the scene on key press Space
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //appends "new trial" to the output file each time
            foreach(string file in fileList)
            {
                System.IO.File.AppendAllText(file, "-------New Trial--------\r\n");
            }
        }

    }
}
