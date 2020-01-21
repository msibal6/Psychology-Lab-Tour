using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlideController : MonoBehaviour
{


    private GameObject holder;

    
    // Start is called before the first frame update
    void Start()
    {
        // Second inilization 
        if (MySceneManager.instance != null)
        {
            for (int i = 0; i < MySceneManager.instance.slideNames.Length; i++)
            {
                if (MySceneManager.instance.slideNames[i] == gameObject.name)
                    gameObject.transform.position = MySceneManager.instance.slidePos[i]; ;
            }

           

                                 
        }

        
    }
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneUnloaded += SaveSlidepos;

    }

    private void SaveSlidepos(Scene current)
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsHeld()
    {
        return (holder != null);
    }

    public GameObject GetHolder()
    { 
        return holder;
    }

    public void SetHolder(GameObject newHolder)
    {
        holder = newHolder;
    }


}
