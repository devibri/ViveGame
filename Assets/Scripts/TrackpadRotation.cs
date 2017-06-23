using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackpadRotation : MonoBehaviour {
    // Defining and instantiating a reference the controller 
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    // private GameObeject controller; 


    // Keep track of the location of the thumb on the trackpad and the
    // direction of the player
    Vector2 touchPosition, playerDirection;
    private GameObject camera;


    void Awake(){
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Use this for initialization
    void Start () {
        camera = GameObject.Find("Camera (head)");
        // controller = GameObject.Find("Controller (right)");
    }

    // Update is called once per frame
    void Update () {
        Debug.Log(getAngle());
    }

    // Get 
    float getAngle(){
        // Update the two vectors
        touchPosition = Controller.GetAxis();
        playerDirection = camera.transform.forward;

        // Find the angle between them
        return Vector2.Angle(playerDirection, touchPosition);
    }
}