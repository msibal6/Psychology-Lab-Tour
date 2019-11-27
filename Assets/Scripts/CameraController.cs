using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /*
     *
     */

    public Transform holdPoint;
    public float horizontalPan = 2.5f;
    public float verticaPan = 2.5f;
    public float moveSpeed = 2.0f; 
    
    private float yaw;
    private float pitch;

    private float maxIntDist = 2.5f;

    private float maxVertical = 30f;
    private float maxHorizontal = 30f;
    private Camera cam;

    private bool grabbing = false;






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

        Debug.DrawRay(transform.position, cam.transform.forward, Color.green);

    }

    void FixedUpdate()
    {
        RaycastHit hit;
        //Ray ray = cam.ScreenPointToRay(transform.TransformDirection(Vector3.forward));

        Ray ray = new Ray(transform.position, cam.transform.forward);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.distance < maxIntDist)
            {
                Debug.Log("can Grab");
                if (Input.GetMouseButtonDown(0))
                {
                    if (!grabbing)
                    {
                        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "canGrab") grabbing = true;
                    }
                    else
                    {
                        grabbing = false;

                    }
                }

            }
        }





        if (grabbing)
        {
            hit.collider.transform.position = holdPoint.transform.position;
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
