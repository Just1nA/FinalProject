using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {   

        if (Input.GetKey ("left"))
        {
            if ((Mathf.Round(transform.eulerAngles.z) >= 0f && Mathf.Round(transform.eulerAngles.z) < 72f) || (Mathf.Round(transform.eulerAngles.z) >= 288f && Mathf.Round(transform.eulerAngles.z) <= 360f))
            {
                transform.Rotate(new Vector3(0,0,1));
            }  
        } else if (Input.GetKey("right"))
        {
            if ((Mathf.Round(transform.eulerAngles.z) >= 0f && Mathf.Round(transform.eulerAngles.z) <= 72f) || (Mathf.Round(transform.eulerAngles.z) > 288f && Mathf.Round(transform.eulerAngles.z) <= 360f))
            {
                transform.Rotate(new Vector3(0,0,-1)); 
            } 
            
        }
    }
}
