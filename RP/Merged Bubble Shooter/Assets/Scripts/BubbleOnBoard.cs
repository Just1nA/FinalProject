using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleOnBoard : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D col) 
    {
    
        Debug.Log("IT HIT SOMETHING");
        if(col.tag == "Bubble")
        {
            col.GetComponent<Bubble>().rb.velocity = Vector3.zero;
            col.GetComponent<Bubble>().StopBubble();
        }
        
    }
}
