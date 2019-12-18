using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroscopeController : MonoBehaviour
{


    private Collider placedObject;
    public Transform placePoint;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Place(Collider objectToPlace)
    {
        placedObject = objectToPlace;
        placedObject.transform.position = placePoint.position;

    }

    void Release()
    {
        placedObject = null;


    }
}
