using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public float maxInteractionDistance;

    private readonly float maxVertical = 30f;
    private readonly float maxHorizontal = 30f;

    private bool grabbing;
    private Collider heldObject;


    


    



    // Start is called before the first frame update
    void Start()
    {

        if (MySceneManager.instance != null)
        { 
            gameObject.transform.position = MySceneManager.instance.playerPos;
        }


        //TODO Cursor visibility needs fixing

        //Cursor.visible = true;
        //Cursor.SetCursor(Texture2D.blackTexture, new Vector2(Screen.width / 2, Screen.height / 2), CursorMode.Auto);

        Cursor.lockState = CursorLockMode.Locked;

        Cursor.SetCursor(Texture2D.blackTexture, Vector2.zero, CursorMode.Auto);


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
        Ray ray = new Ray(transform.position, transform.forward);


        // Looking to determine if there is something to interact with  
        Physics.Raycast(ray, out RaycastHit hit, maxInteractionDistance);


        // You try to grab here
        if (Input.GetMouseButtonDown(0))
        {
            // TODO Add all grabbin cases i.e grabbing and you want to switch with something and etc

            // Nothing in your hand and you're trying to grab something
            if (!grabbing && hit.collider != null && hit.collider.gameObject.layer == 8)
            {

                heldObject = hit.collider;
                grabbing = true;

                // Check if we are grabbing a slide

                if (hit.collider.gameObject.tag == "Slide")
                {
                    SlideController tempSlide = hit.collider.gameObject.GetComponent<SlideController>();
                    // Is the slide in the microscope
                    if (tempSlide.IsHeld())
                    {
                        // Release the slide from the microscope
                        MicroscopeController tempMicro = tempSlide.GetHolder().GetComponent<MicroscopeController>();
                        tempMicro.Release();
                        tempSlide.SetHolder(gameObject);
                    }
                }
            }

            // Inserting  and releasing Slide Bed if we are grabbing it
            else if (heldObject != null && heldObject.gameObject.tag == "Slide" && hit.collider != null && hit.collider.gameObject.name == "Slide Bed")
            {
                MicroscopeController tempMicroscopeController = hit.collider.transform.parent.gameObject.GetComponent<MicroscopeController>();

                if (!tempMicroscopeController.ContainSlide())
                {
                    tempMicroscopeController.Place(heldObject.gameObject);
                    heldObject.gameObject.GetComponent<SlideController>().SetHolder(tempMicroscopeController.gameObject);
                    Release();
                }
                else
                {
                    // Placeholder for tooltip that tells there is a slide in the microscope
                    Debug.Log("Theres already A slide in the microscope");

                }
            }
            // You are releasing it into nothing i.e dropping it
            else
            {
                Release();
            }

            if (hit.collider != null && hit.collider.gameObject.name == "Looking part")
            {
                // Saves position
                // TODO Save slide posiitons and microscope heling 
                MySceneManager.instance.playerPos = transform.position;

                MySceneManager.instance.SwitchScene("MicroscopeView");

            }
        }

        // Object Tracking for when you are holding it
        // Goes faster wehn you are moving to prevent jitter
        if (grabbing)
        {
            bool isMoving = System.Math.Abs(Input.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Vertical")) > 0;
            heldObject.transform.position = Vector3.MoveTowards(heldObject.transform.position, holdPoint.transform.position, isMoving ? .08f : 0.05f);

        }
    }


    private void Release()
    {
        grabbing = false;
        heldObject = null;
    }

    void OnDrawGizmos()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 100);
    }


    void SavePlayer()
    {
        MySceneManager.instance.playerPos = transform.position;




    }

}
