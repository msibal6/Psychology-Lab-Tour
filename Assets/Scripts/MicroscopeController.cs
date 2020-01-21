using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MicroscopeController : MonoBehaviour
{


    private string placedSlide;
    public Transform placePoint;

    void Awake()
    {
        Debug.Log("Awake");
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneUnloaded += SaveMicroscope;
            
    }

    private void SaveMicroscope(Scene current)
    {
        MySceneManager.instance.placedSlide = placedSlide;
    }


    public void Place(GameObject objectToPlace)
    {
        placedSlide = objectToPlace.name;
        objectToPlace.transform.position = placePoint.position;
        Debug.Log(placedSlide);
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
        return placedSlide;
    }

 
}
