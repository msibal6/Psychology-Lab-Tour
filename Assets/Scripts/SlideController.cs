using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideController : MonoBehaviour
{


    private GameObject holder;


    // Start is called before the first frame update
    void Start()
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
