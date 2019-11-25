﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /*
     *
     */

    public float horizontalPan = 2.5f;
    public float verticaPan = 2.5f;
    public float moveSpeed = 2.0f; 

    private float yaw;
    private float pitch;

    private float maxIntDist = 2.5f;

    private float maxVertical = 30f;
    private float maxHorizontal = 30f;
    private Camera cam;





    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
        yaw += horizontalPan * Input.GetAxis("Mouse X");
        pitch -= verticaPan * Input.GetAxis("Mouse Y");


        transform.position += moveSpeed * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.eulerAngles = new Vector3(Mathf.Clamp(pitch,-maxVertical,maxVertical), Mathf.Clamp(yaw,-maxHorizontal,maxHorizontal));

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);

    }

    void FixedUpdate()
    {
        RaycastHit hit;
        //Ray ray = cam.ScreenPointToRay(transform.TransformDirection(Vector3.forward));

        Ray ray = new Ray(transform.position, Vector3.forward);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.distance < maxIntDist)
            {

                Debug.Log("Hit the sphere");
                Debug.Log(hit.transform);
            }
        }
        





    }

    void OnDrawGizmos()
    {
        //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Ray ray = new Ray(transform.position, Vector3.forward);

        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray);
    }
}
