using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MySceneManager : MonoBehaviour
{

    public static MySceneManager instance;
    public Transform[] slides;
    public Vector3 playerPos;



    // Start is called before the first frame update
    void Start()
    {

        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        // Maintain the same instance across the game
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchScene(string name)
    {
        SceneManager.LoadScene(name);



    }

   
}
