using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackpadRotation : MonoBehaviour {

    private GameObject camera;

    // I don't know what this is
    private SteamVR_TrackedObject trackedObj;

    // Defining and instantiating a reference the controller 
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    // Defining and instantiating a reference to the hmd
    //private Stea

    // Keep track of the position of a thumb on the trackpad, the direction
    // vector of the player, and the angle between the two
    Vector2 touchPadVector, playerDirectionVector;

    void Awake(){
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Use this for initialization
    void Start () {
        camera = GameObject.Find("Camera (head)");
    }
	
	// Update is called once per frame
	void Update () {}

    // Get 
    float getAngle(){
        // Update the two vectors
        touchPadVector = Controller.GetAxis();
        playerDirectionVector = camera.transform.forward;

        // Find the angle between them
        return Vector2.Angle(playerDirectionVector, touchPadVector);
    }
}