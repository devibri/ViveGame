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
        if (System.IO.File.Exists(outputFile))
        {
            System.IO.File.Delete(outputFile);
        }
    }

    void Update()
    {
        if (Time.time - currentTime > 0.2f)
        {
            System.IO.File.AppendAllText(outputFile, "Eyes position: " +  eyes.GetComponent<Transform>().position + " Eyes angle:" + eyes.GetComponent<Transform>().eulerAngles.ToString() + " Camera rig position:" + GetComponent<Transform>().position + "Camera rig angle:" + GetComponent<Transform>().eulerAngles.ToString() + "\r\n");
            currentTime = Time.time;
        }
    }

    void FixedUpdate()
    {
        move();
    }
    void move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Transform>().Translate(Vector3.forward / 10, GetComponent<Transform>());
        }
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Transform>().Translate(Vector3.left / 10);
        }
        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Transform>().Translate(Vector3.back / 10);
        }
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Transform>().Translate(Vector3.right / 10);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            GetComponent<Transform>().RotateAround(GetComponent<Transform>().transform.position, Vector3.up, -MOVE_AMOUNT);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            GetComponent<Transform>().RotateAround(GetComponent<Transform>().transform.position, Vector3.up, MOVE_AMOUNT);
        }
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    GetComponent<Transform>().RotateAround(GetComponent<Transform>().transform.position, Vector3.right, MOVE_AMOUNT);
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    GetComponent<Transform>().RotateAround(GetComponent<Transform>().transform.position, Vector3.right, -MOVE_AMOUNT);
        //}
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
