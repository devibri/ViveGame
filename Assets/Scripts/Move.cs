using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    const float MOVE_AMOUNT = 10.0f;
    float currentTime;
    const string outputFile = "Place.txt";
    public GameObject eyes;
    // Use this for initialization
    void Start()
    {
        currentTime = Time.time;
    }

    // Write the position and rotation of the object it is attached to  (camera rig)
    void Update()
    {
	// Write the position and rotation 5 times per second
        if (Time.time - currentTime > 0.2f)
        {
            System.IO.File.AppendAllText(outputFile, "Time:" + Time.time + " Eyes position:" + eyes.GetComponent<Transform>().position + " Camera rig position:" + GetComponent<Transform>().position + " Eyes rotation:" + eyes.GetComponent<Transform>().eulerAngles.ToString() + "\r\n");
            currentTime = Time.time;
        }
    }

    // Fixed Update is different from regular update. It runs before update
    void FixedUpdate()
    {
        move();
    }
    
    // Move according to the keyboard
    void move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Transform>().Translate(GetComponent<Transform>().forward / 10, Space.World);
        }
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Transform>().Translate(GetComponent<Transform>().right / -10, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Transform>().Translate(GetComponent<Transform>().forward / -10, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Transform>().Translate(GetComponent<Transform>().right / 10, Space.World);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            GetComponent<Transform>().RotateAround(GetComponent<Transform>().transform.position, Vector3.up, -MOVE_AMOUNT);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            GetComponent<Transform>().RotateAround(GetComponent<Transform>().transform.position, Vector3.up, MOVE_AMOUNT);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            GetComponent<Transform>().Translate(Vector3.up / 10);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            GetComponent<Transform>().Translate(Vector3.down / 10);
        }
        if (Input.GetKey(KeyCode.R))
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0, GetComponent<Transform>().eulerAngles.y, 0);
        }
    }
}
