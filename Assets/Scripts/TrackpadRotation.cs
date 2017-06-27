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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(getAngle());
        }

        // Actually rotating
        // camera.transform.Rotate(0, , 0)
    }

    int sign(float num){
        if (num >= 0)
        {
            return 1;
        }
        else {
            return -1;
        }
    }

    // Get the angle the play would rotate
    float getAngle()
    {
        return Vector2.Angle(Vector2.up, Controller.GetAxis()) * sign(Controller.GetAxis().x);
    }
}