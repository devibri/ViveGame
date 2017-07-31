using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*changes the center of the cube that collides with the marker to match where the player's eye is (HMD position in the tracked space)*/

public class Collide : MonoBehaviour
{
    private GameObject cam; //represents the camera (player position in tracked space)
    // Use this for initialization
    void Start()
    {
        cam = GameObject.Find("Camera (eye)");

}

    // Update is called once per frame
    void Update()
    {
        //sets the center of the cube (box collider) to the camera location
        this.transform.position = cam.GetComponent<Transform>().position;
    }
}
