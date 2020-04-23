using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public float maxLength = 9;     // Laser length
    private LineRenderer lrend;     // Laser itself
    private Ray ray;                // Position of laser using rays

    void Start()
    {
        lrend = GetComponent<LineRenderer>();         /* Initialize the Line Renderer object upon start */
    }
    
    void Update()
    {
        /* Updates the ray to the position of the shooter's rotation & shoots directly up & out of the shooter */
        ray = new Ray(transform.position, transform.up);
        
        lrend.SetPosition(0, transform.position);                        //  Anchors original line renderer point to the tip of the shooter & 
        lrend.SetPosition(1, ray.origin + ray.direction * maxLength);    //  transforms the line to match with the direction of the shooter   
    }
}