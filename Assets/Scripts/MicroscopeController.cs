using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroscopeController : MonoBehaviour
{


    private GameObject placedSlide;
    public Transform placePoint;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Place(GameObject objectToPlace)
    {
        placedSlide = objectToPlace;
        placedSlide.transform.position = placePoint.position;
        Debug.Log(placedSlide.name);
    }

    public void Release()
    {
        placedSlide = null;
        Debug.Log(placedSlide);
    }

    public bool ContainSlide()
    {
        return (placedSlide != null);
    }

    public string GetSlideName()
    {
        return placedSlide.gameObject.name;
    }

    
}
