using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    void Update()
    {   
        /* Rotates shooter to the left when user presses left arrow key to a certain degree */
        if (Input.GetKey ("left"))
        {
            if ((Mathf.Round(transform.eulerAngles.z) >= 0f && Mathf.Round(transform.eulerAngles.z) < 72f) || 
                (Mathf.Round(transform.eulerAngles.z) >= 288f && Mathf.Round(transform.eulerAngles.z) <= 360f))
            {
                transform.Rotate(new Vector3(0,0,1)); // Rotates shooter to new position
            }  
        }
        /* Rotates shooter to the right when user presses right arrow key to a certain degree */
        else if (Input.GetKey("right"))
        {
            if ((Mathf.Round(transform.eulerAngles.z) >= 0f && Mathf.Round(transform.eulerAngles.z) <= 72f) || 
                (Mathf.Round(transform.eulerAngles.z) > 288f && Mathf.Round(transform.eulerAngles.z) <= 360f))
            {
                transform.Rotate(new Vector3(0,0,-1)); // Rotates shooter to new position
            }    
        }
    }
}
