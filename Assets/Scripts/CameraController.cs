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

    private bool grabbing;
    private Collider heldOject;

    private GameObject microscope = GameObject.Find("Microscope");




    



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
        yaw += horizontalPan * Input.GetAxis("Mouse X");
        pitch -= verticaPan * Input.GetAxis("Mouse Y");
        transform.position += moveSpeed * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.eulerAngles = new Vector3(Mathf.Clamp(pitch,-maxVertical,maxVertical), Mathf.Clamp(yaw,-maxHorizontal,maxHorizontal));
    }

    void FixedUpdate()
    {

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        // Looking to determine if there is something to interact with  
        Physics.Raycast(ray, out hit, maxInteractionDistance);


        // You try to grab here
        if (Input.GetMouseButtonDown(0))
        {
            // Nothing in your hand and you are looking at something you can grab
            // TODO Add all grabbin cases i.e grabbing and you want to switch with something and etc

            if (!grabbing && hit.collider != null && hit.collider.gameObject.layer == 8)
            {

                heldOject = hit.collider;
                Debug.Log(heldOject.gameObject.name);
                grabbing = true;

                // You already have in something in your hand
                //  TO DO
                // Need to account for when you want to do something with  held object i.e put in the microscope
            }

            // Inserting Slide Bed if we are grabbing it
            else if (heldOject.gameObject.tag == "Slide" && hit.collider != null  && hit.collider.gameObject.name == "Slide Bed")
            {
                Debug.Log("touching slide Bed");

            } 
            else
            {
                grabbing = false;
                heldOject = null;
            }

        }

        // Object Tracking for when you are holding it
        // Goes faster wehn you are moving to prevent jitter
        if (grabbing)
        {
            bool isMoving = System.Math.Abs(Input.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Vertical")) > 0;
            heldOject.transform.position = Vector3.MoveTowards(heldOject.transform.position, holdPoint.transform.position, isMoving ? .08f : 0.05f);
        }





    }

    void OnDrawGizmos()
    {

        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * 100);
    }
}
