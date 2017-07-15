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
    public float RotationThreshold = .1f;
    public float TranslationThreshold = .3f;

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

    // Reticle actually being used and its transform
    private GameObject reticle;
    private Transform reticleTransform;

    private GameObject[] markers = new GameObject[3];
    private GameObject currentMarker;

    //player position / rotation
    private GameObject camera;


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

        if (MoveMode == 1) {
            reticle = teleportReticle;
            reticleTransform = teleportReticleTransform;
        } else if (MoveMode == 2) {
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

        //if (System.IO.File.Exists(trackpadFile))
        //{
        //    System.IO.File.Delete(trackpadFile);
        //}
        //if (System.IO.File.Exists(trackpadFile))
        //{
        //    System.IO.File.Delete(trackpadFile);
        //}
        //if (System.IO.File.Exists(responseFile))
        //{
        //    System.IO.File.Delete(responseFile);
        //}
        System.IO.File.AppendAllText(trackpadFile, "Start time:" + Time.time + "\r\n");

        // Hide everything
        laser.SetActive(false);
        teleportReticle.SetActive(false);
        answerReticle.SetActive(false);
        rotateReticle.SetActive(false);
    }

    void Update()
    {
        camera = GameObject.Find("Camera (eye)");
        //moves the camera rig rotation to always be the same as the player rotation
        //cameraRigTransform.forward = camera.transform.forward;



        // Check if the user started moving, taking into account which movement
        // mode they are in
        bool movementInitiated = false;
        bool answerInitiated = false;
        movementInitiated = Controller.GetPress(MoveButton);
        answerInitiated = Controller.GetPress(AnswerButton);
        if (Time.time - currentTime > 0.1f)
        {
            currentTime = Time.time;
            if (Controller.GetAxis() != zeroVector)
            {
                System.IO.File.AppendAllText(trackpadFile, Controller.GetAxis().ToString() + " " + GetThumbRotation() + "\r\n");
            }
        }


        // Make sure the player either teleport or answers--not both
        if (answerInitiated && movementInitiated) {
            answerInitiated = false;
        }

        if (movementInitiated){
            // Send out a raycast from the controller
            RaycastHit hit;
            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask)){
                // Point the laser
                hitPoint = hit.point;

                if (Vector3.Magnitude(hitPoint - GetCurrentMarker().transform.position) < TranslationThreshold) {
                    SnapReticlePosition(hit);
                    //SnapReticleRotation();
                }


                

                ShowLaser(hit);

                //Show teleport reticle
                reticle.SetActive(true);
                reticleTransform.position = hitPoint + teleportReticleOffset;

                // Rotate the marker to match new orientation
                if (MoveMode == 2){
                    reticleTransform.rotation = Quaternion.Euler(0, cameraRigTransform.eulerAngles.y + GetThumbRotation(), 0);
                }

                // If you're in this block, you hit something with the teleport mask
                shouldTeleport = true;
            } else {
                // If you didn't hit the somthing with the teleport mask, turn off the laser
                laser.SetActive(false);
                reticle.SetActive(false);
            }
        } else if (answerInitiated){
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
            } else {
                // If you didn't hit the somthing with the teleport mask, turn off the laser
                laser.SetActive(false);
                answerReticle.SetActive(false);
            }
        }

        // Check if the user stopped moving, taking into account which movement
        // mode they are in
        bool movementTerminated = false;
        bool answerTerminated = false;
        movementTerminated = Controller.GetPressUp(MoveButton);
        answerTerminated = Controller.GetPressUp(AnswerButton);

        // If the player initiated a teleport where somewhere nice (object with teleport mask)
        // then teleport them there
        if (movementTerminated && shouldTeleport) {
            Teleport();

            if (MoveMode == 2 && ReticleSnapped())
            {
                //Rotate();
                SnapPlayerRotation();
            }
            
           // Laser stays for some reason without this line
           laser.SetActive(false);
            answerReticle.SetActive(false);
        } else if (shouldTeleport && answerTerminated) {
            Debug.Log("TEXT");
            // Writes the response to a file
            System.IO.File.AppendAllText(responseFile, "Response: " + answerReticleTransform.position.ToString() + " Time: " + Time.time + "\r\n");
            answerReticle.SetActive(false);
            laser.SetActive(false);
        }

        if (Vector3.Magnitude(hitPoint - GetCurrentMarker().transform.position) < TranslationThreshold)
        {
            //SnapReticlePosition(hit);
            SnapReticleRotation();
        }
        //SnapReticleRotation();
    }

    private GameObject GetCurrentMarker() {
        for (int i = 0; i < markers.Length; i++) {
            if (markers[i].active) {
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
        shouldTeleport = false; // Teleport in progress, no need to do it again until the next touchpad release
        reticle.SetActive(false); // Hide reticle
        cameraRigTransform.position = hitPoint; // Change the camera rig position to where the the teleport reticle was. Also add the difference so the new virtual room position is relative to the player position, allowing the player's new position to be exactly where they pointed. (see illustration)
    }

    float GetThumbRotation() {
        float thumbAngle = Vector2.Angle(Vector2.up, Controller.GetAxis());
        float turnSign = Mathf.Sign(Controller.GetAxis().x);
        return thumbAngle * turnSign;
    }

    void Rotate()
    {
        cameraRigTransform.Rotate(0, GetThumbRotation(), 0);
    }

    bool ReticleSnapped() {
           return reticleTransform.rotation == currentMarker.transform.rotation;
    }

    void SnapReticlePosition(RaycastHit hit) {
        foreach (GameObject obj in markers)
        {
            if (obj.activeSelf)
            {
                Vector3 oldOrientation = trackedObj.transform.eulerAngles;
                trackedObj.transform.LookAt(obj.transform.position);
                Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask);
                trackedObj.transform.eulerAngles = oldOrientation;
                hitPoint = hit.point;
                break;
            }
        }

        Debug.Log("Marker position: " + GetCurrentMarker().transform.position);
        Debug.Log("Reticle position: " + reticleTransform.position);
    }

    void SnapReticleRotation() {
        currentMarker = GetCurrentMarker();
        if (currentMarker != null && Mathf.Abs(reticleTransform.rotation.y) - Mathf.Abs(currentMarker.transform.localRotation.y) < RotationThreshold)
        {
            reticleTransform.rotation = GetCurrentMarker().transform.localRotation;
        }
    }

    void SnapPlayerRotation() {
        cameraRigTransform.rotation = GetCurrentMarker().transform.rotation;
    }
}
