using System;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    public Transform cameraRigTransform;
    public Transform headTransform; // The camera rig's head
    public Vector3 teleportReticleOffset; // Offset from the floor for the reticle to avoid z-fighting
    public LayerMask teleportMask; // Mask to filter out areas where teleports are allowed

    private SteamVR_TrackedObject trackedObj;

    public GameObject laserPrefab; // The laser prefab
    private GameObject laser; // A reference to the spawned laser
    private Transform laserTransform; // The transform component of the laser for ease of use

    private Vector3 hitPoint; // Point where the raycast hits
    private bool shouldTeleport; // True if there's a valid teleport target

    /* MoveMode refers to the type of movement. They are defined as follows
     *  1 - Teleporting and physically turning
     *  2 - Teleporting and turning with directional pad
     *  Any other number - Physically walking and rotating
     */
    private const int MoveMode = 2;

    // Prefabs for reticles
    public GameObject answerReticlePrefab;
    public GameObject rotateReticlePrefab;
    public GameObject teleportReticlePrefab;

    // Instances of prefabs
    private GameObject answerReticle;
    private GameObject teleportReticle;
    private GameObject rotateReticle;


    // Output files
    const string outputFile = "Teleport.txt";
    const string trackpadFile = "Track.txt";
    const string responseFile = "Response.txt";

    char[] delimiters = { '\\' };

    // Vector when you're not touching it
    Vector2 zeroVector = new Vector2(0.0f, 0.0f);

    // Needed for time
    float currentTime;


    // Transforms
    private Transform answerReticleTransform;
    private Transform teleportReticleTransform;
    private Transform rotateReticleTransform;

    // Buttons used
    public const ulong MoveButton = SteamVR_Controller.ButtonMask.Touchpad;
    public const ulong AnswerButton = SteamVR_Controller.ButtonMask.Trigger;
    public KeyCode MoveFileButton = KeyCode.M;

    // Reticle actually being used and its transform
    private GameObject reticle;
    private Transform reticleTransform;

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
        // Intialize laser
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;

        // Create instances of reticles
        teleportReticle = Instantiate(teleportReticlePrefab);
        answerReticle = Instantiate(answerReticlePrefab);
        rotateReticle = Instantiate(rotateReticlePrefab);

        // Make variables for transforms
        teleportReticleTransform = teleportReticle.transform;
        answerReticleTransform = answerReticle.transform;
        rotateReticleTransform = rotateReticle.transform;

        // Use the reticle corresponding to the movement mode
        if (MoveMode == 1)
        {
            reticle = teleportReticle;
            reticleTransform = teleportReticleTransform;
        }
        else if (MoveMode == 2)
        {
            reticle = rotateReticle;
            reticleTransform = rotateReticleTransform;
        }



        //Get markers
        markers[0] = GameObject.Find("StartMarker");
        markers[1] = GameObject.Find("FirstMarker");
        markers[2] = GameObject.Find("SecondMarker");

        // Get the files and time set up for it
        currentTime = Time.time;
        if (System.IO.File.Exists(outputFile))
        {
            System.IO.File.Delete(outputFile);
        }

        System.IO.File.AppendAllText(trackpadFile, "Start time:" + Time.time + "\r\n");

        // Hide reticles and laser
        laser.SetActive(false);
        teleportReticle.SetActive(false);
        answerReticle.SetActive(false);
        rotateReticle.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(MoveFileButton))
        {
            MoveFiles();
        }
        // Check if the user started moving, taking into account which movement
        // mode they are in
        bool movementInitiated = Controller.GetPress(MoveButton);
        bool answerInitiated = Controller.GetPress(AnswerButton);
        
        if (Time.time - currentTime > 0.1f)
        {
            currentTime = Time.time;
            if (Controller.GetAxis() != zeroVector)
            {
                System.IO.File.AppendAllText(trackpadFile, "Time:" + Time.time + " Axis vector:" + Controller.GetAxis().ToString() + " Thumbrotation:" + GetThumbRotation() + "\r\n");
            }
        }


        // Make sure the player either teleport or answers--not both
        if (answerInitiated && movementInitiated)
        {
            answerInitiated = false;
        }

        if (movementInitiated && (MoveMode == 1 || MoveMode == 2))
        {
            // Send out a raycast from the controller
            RaycastHit hit;
            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask))
            {
                // Point the laser
                hitPoint = hit.point;
                ShowLaser(hit);

                //Show teleport reticle
                reticle.SetActive(true);
                reticleTransform.position = hitPoint + teleportReticleOffset;

                // Rotate the reticle to match new orientation
                if (MoveMode == 2)
                {
                    reticleTransform.rotation = Quaternion.Euler(0, cameraRigTransform.eulerAngles.y + GetThumbRotation(), 0);
                }

                // If you're in this block, you hit something with the teleport mask
                shouldTeleport = true;
            } else {
                // If you didn't hit the somthing with the teleport mask, turn off the laser
                laser.SetActive(false);
                reticle.SetActive(false);
            }
        }
        else if (answerInitiated)
        {
            // Send out a raycast from the controller
            RaycastHit hit;

            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask))
            {
                // Point the laser
                hitPoint = hit.point;
                ShowLaser(hit);


                //Show teleport reticle
                answerReticle.SetActive(true);
                answerReticleTransform.position = hitPoint + teleportReticleOffset;

                // If you're in this block, you hit something with the teleport mask
                shouldTeleport = true;
            }
            else
            {
                // If you didn't hit the somthing with the teleport mask, turn off the laser
                laser.SetActive(false);
                answerReticle.SetActive(false);
            }
        }

        // Check if the user stopped moving, taking into account which movement
        // mode they are in
        bool movementTerminated = Controller.GetPressUp(MoveButton);
        bool answerTerminated = Controller.GetPressUp(AnswerButton);

        // If the player initiated a teleport where somewhere nice (object with teleport mask)
        // then teleport them there
        if (movementTerminated && shouldTeleport && (MoveMode == 1 || MoveMode == 2))
        {

            if (MoveMode == 2)
            {
                Rotate();
            }

            Teleport();

            // Laser stays for some reason without this line
            laser.SetActive(false);
            answerReticle.SetActive(false);
        }
        else if (shouldTeleport && answerTerminated)
        {
            // Writes the response to a file
            System.IO.File.AppendAllText(responseFile, "Response: " + answerReticleTransform.position.ToString() + " Time: " + Time.time + "\r\n");
            answerReticle.SetActive(false);
            laser.SetActive(false);
        }
    }

    private GameObject GetCurrentMarker()
    {
        for (int i = 0; i < markers.Length; i++)
        {
            if (markers[i].active)
            {
                return markers[i];
            }
        }

        return null;
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

        shouldTeleport = false;
        reticle.SetActive(false);
        Vector3 difference = cameraRigTransform.position - headTransform.position;
        difference.y = 0;
        cameraRigTransform.position = hitPoint + difference;
    }
    float GetThumbRotation()
    {
        float thumbAngle = Vector2.Angle(Vector2.up, Controller.GetAxis());
        float turnSign = Mathf.Sign(Controller.GetAxis().x);
        return thumbAngle * turnSign;
    }

    void Rotate()
    {
        cameraRigTransform.Rotate(0, GetThumbRotation(), 0);
    }

    float AngleBetween(Quaternion first, Quaternion second)
    {
        GameObject currentMarker = GetCurrentMarker();
        if (currentMarker == null)
        {
            return Mathf.Infinity;
        }

        Vector4 firstQuaternion = new Vector4(first.w, first.x, first.y, first.z);
        Vector4 secondQuaternion = new Vector4(second.w, second.x, second.y, second.z);
        float dotProduct = Vector4.Dot(firstQuaternion, secondQuaternion);
        float angle = Mathf.Acos(2 * dotProduct * dotProduct - 1);
        angle *= 180 / Mathf.PI;

        return angle;
    }

    void MoveFiles() {
        string[] fileList = System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory(), "*.txt");
        int i = 0;
        char landmark;
        GameObject Landm = GameObject.Find("Landmarks");
        try
        {
            Landm.ToString();
            landmark = 'L';
            
        } catch( Exception e)
        {
            landmark = 'N';
        }
        for (; System.IO.Directory.Exists(i.ToString() + "_" + MoveMode.ToString() + landmark); i++)
        { }
        System.IO.Directory.CreateDirectory(i.ToString() + "_" + MoveMode.ToString() + landmark);
        foreach (string file in fileList)
        {

            string[] splitFile = file.Split(delimiters);
            //Debug.Log(System.IO.Directory.GetCurrentDirectory() + "\\" + i.ToString() + "_" + MoveMode.ToString() + landmark + "\\" + splitFile[splitFile.Length - 1]);
            System.IO.File.Move(file, System.IO.Directory.GetCurrentDirectory() + "\\" + i.ToString() + "_" + MoveMode.ToString() + landmark + "\\" + splitFile[splitFile.Length - 1]);
        }
    }
}