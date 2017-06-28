using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackpadRotation : MonoBehaviour
{
    // Defining and instantiating a reference the controller
    // Not sure what trackedObj is 
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    private GameObject camera;


    // Awake runs before the game starts
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Get a reference to the players 
    void Start()
    {
        camera = GameObject.Find("Camera (eye)");
    }

    // Update is called once per frame
    void Update(){}

    // Used to find sign of the angle of rotation
    // Right rotations are positive
    // Left roatations are negative
    int sign(float num){
        if (num >= 0)
        {
            return 1;
        } else {
            return -1;
        }
    }

    // Get the angle the play would rotate
    float getAngle()
    {
        return Vector2.Angle(Vector2.up, Controller.GetAxis()) * sign(Controller.GetAxis().x);
    }
}