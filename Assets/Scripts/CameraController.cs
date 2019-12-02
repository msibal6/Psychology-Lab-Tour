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

    private float maxInteractionDistance = 2.5f;

    private float maxVertical = 30f;
    private float maxHorizontal = 30f;
    private Camera cam;

    private bool grabbing;
    private Collider heldOject;






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
        Ray ray = new Ray(transform.position, cam.transform.forward);

        // Looking to determine if there is something to interact with
        // 
        Physics.Raycast(ray, out hit, maxInteractionDistance);

        // You try to grab here
        if (Input.GetMouseButtonDown(0))
        {
            // Nothing in your had
            if (!grabbing)
            {
                if (hit.collider != null && hit.collider.gameObject.CompareTag("canGrab"))
                {
                    heldOject = hit.collider;
                    grabbing = true;
                }
            // You already have in something in your hand
            //  TO DO
            // Need to account for when you want to do something with  held object i.e put in the microscope
            } else {
                grabbing = false;
            }

        }

        if (grabbing)
        {
            heldOject.transform.position = holdPoint.transform.position; 
        }





    }

    void OnDrawGizmos()
    {
        //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Ray ray = new Ray(transform.position, cam.transform.forward);

        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.green;
        Gizmos.DrawRay(ray);
        Gizmos.DrawRay(transform.position, cam.transform.forward * 100);
    }
}
