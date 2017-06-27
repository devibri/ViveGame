using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackpadRotation : MonoBehaviour
{
    // Defining and instantiating a reference the controller 
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    // Keep track of the location of the thumb on the trackpad and the
    // direction of the player
    Vector2 touchPosition, playerDirection;
    private GameObject camera;
    bool write;


    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Use this for initialization
    void Start()
    {
        camera = GameObject.Find("Camera (eye)");
        write = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("You're facing: " + camera.transform.forward.ToString());
        //Debug.Log("You're pointing: " + Controller.GetAxis().ToString()); // Working as expected
        Debug.Log(getAngle());
    }

    // Get the angle the play would rotate
    float getAngle()
    {
        playerDirection = camera.transform.forward;
        Vector2 trackpadDirection = Controller.GetAxis();
        float offsetAngle = Vector.Angle(playerDirection, Vector2.up);
        Vector2 turnVector = Quaternion.AngleAxis(offsetAngle, Vector3.forwward) * trackpadDirection;

        // Find the angle between them
        return Vector2.Angle(playerDirection, turnVector);
    }
}