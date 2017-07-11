﻿using UnityEngine;

public class Recordteleport : MonoBehaviour
{
    public Transform cameraRigTransform;
    public Transform headTransform; // The camera rig's head
    public Vector3 teleportReticleOffset; // Offset from the floor for the reticle to avoid z-fighting
    public LayerMask teleportMask; // Mask to filter out areas where teleports are allowed

    private SteamVR_TrackedObject trackedObj;

    public GameObject laserPrefab; // The laser prefab
    private GameObject laser; // A reference to the spawned laser
    private Transform laserTransform; // The transform component of the laser for ease of use

    public GameObject teleportReticlePrefab; // Stores a reference to the teleport reticle prefab.
    private GameObject reticle; // A reference to an instance of the reticle
    private Transform teleportReticleTransform; // Stores a reference to the teleport reticle transform for ease of use

    private Vector3 hitPoint; // Point where the raycast hits
    private bool shouldTeleport; // True if there's a valid teleport target

    /* MoveMode refers to the type of movement. They are defined as follows
     *  1 - Teleporting and physically turning
     *  2 - Teleporting and turning with directional pad
     *  Any other number - Physically walking and rotating
     */
    private int MoveMode = 2;

    const string outputFile = "Teleport.txt";
    const string trackpadFile = "Track.txt";
    Vector2 zeroVector = new Vector2(0.0f, 0.0f);
    float currentTime;

    private GameObject[] markers = new GameObject[3];


    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Start()
    {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
        reticle = Instantiate(teleportReticlePrefab);
        teleportReticleTransform = reticle.transform;
        laser.SetActive(false);
        reticle.SetActive(false);

        markers[0] = GameObject.Find("StartMarker");
        markers[1] = GameObject.Find("FirstMarker");
        markers[2] = GameObject.Find("SecondMarker");
        currentTime = Time.time;
        if (System.IO.File.Exists(outputFile))
        {
            System.IO.File.Delete(outputFile);
        }

        if (System.IO.File.Exists(trackpadFile))
        {
            System.IO.File.Delete(trackpadFile);
        }
        if (System.IO.File.Exists(trackpadFile))
        {
            System.IO.File.Delete(trackpadFile);
        }
        System.IO.File.AppendAllText(trackpadFile, "Start time:" + Time.time +"\r\n");
    }

    void Update()
    {
        // Check if the user started moving, taking into account which movement
        // mode they are in
        bool movementInitiated = false;
        if (MoveMode == 1)
        {
            movementInitiated = Controller.GetPress(SteamVR_Controller.ButtonMask.Trigger);
        }
        else if (MoveMode == 2)
        {
            movementInitiated = Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad);

            if (Time.time - currentTime > 0.1f)
            {
                currentTime = Time.time;
                if (Controller.GetAxis() != zeroVector)
                {
                    System.IO.File.AppendAllText(trackpadFile, Controller.GetAxis().ToString() + " " + GetPlayerRotation() + "\r\n");
                }
            }
        }

        if (movementInitiated)
        {
            // Send out a raycast from the controller
            RaycastHit hit;
            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask))
            {
                // Point the laser
                hitPoint = hit.point;

                //This should work
                foreach(GameObject obj in markers)
                {
                    if (obj.GetComponent<Renderer>().enabled)
                    {
                        Vector3 oldOrientation = trackedObj.transform.eulerAngles;
                        trackedObj.transform.LookAt(obj.transform.position);
                        Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask);
                        trackedObj.transform.eulerAngles = oldOrientation;
                        break;
                    }
                }

                ShowLaser(hit);

                //Show teleport reticle
                reticle.SetActive(true);
                teleportReticleTransform.position = hitPoint + teleportReticleOffset;

                // If you're in this block, you hit something with the teleport mask
                shouldTeleport = true;
            }
            else
            {
                // If you didn't hit the somthing with the teleport mask, turn off the laser
                laser.SetActive(false);
                reticle.SetActive(false);
            }
        }

        // Check if the user stopped moving, taking into account which movement
        // mode they are in
        bool movementTerminated = false;
        if (MoveMode == 1)
        {
            movementTerminated = Controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger);
        }
        else if (MoveMode == 2)
        {
            movementTerminated = Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad);
        }

        // If the player initiated a teleport where somewhere nice (object with teleport mask)
        // then teleport them there
        if (movementTerminated && shouldTeleport)
        {
            Teleport();

            // I'm not sure where this should go
            // when the moving trial is being done
            System.IO.File.AppendAllText(outputFile, cameraRigTransform.position.ToString() + " " + cameraRigTransform.eulerAngles.ToString() + "\r\n");

            // If the user is in the trackpad rotation mode
            // then rotate them
            if (MoveMode == 2)
            {
                Rotate();
            }

            // Laser stays for some reason witout this line
            laser.SetActive(false);
        }
    }

    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true); //Show the laser
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f); // Move laser to the middle between the controller and the position the raycast hit
        laserTransform.LookAt(hitPoint); // Rotate laser facing the hit point
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
            hit.distance); // Scale laser so it fits exactly between the controller & the hit point
    }

    private void Teleport()
    {
        shouldTeleport = false; // Teleport in progress, no need to do it again until the next touchpad release
        reticle.SetActive(false); // Hide reticle
        Vector3 difference = cameraRigTransform.position - headTransform.position; // Calculate the difference between the center of the virtual room & the player's head
        difference.y = 0; // Don't change the final position's y position, it should always be equal to that of the hit point

        cameraRigTransform.position = hitPoint + difference; // Change the camera rig position to where the the teleport reticle was. Also add the difference so the new virtual room position is relative to the player position, allowing the player's new position to be exactly where they pointed. (see illustration)

    }

    float GetPlayerRotation()
    {
        float thumbAngle = Vector2.Angle(Vector2.up, Controller.GetAxis());
        float turnSign = Mathf.Sign(Controller.GetAxis().x);
        return thumbAngle * turnSign;
    }

    void Rotate()
    {
        cameraRigTransform.Rotate(0, GetPlayerRotation(), 0);
    }
}