using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleTWall : MonoBehaviour
{
   void OnTriggerEnter2D (Collider2D col) 
    {
        Bubble.wallHit = true;
        Bubble.isFired = false;
        Bubble.rotating = false;
        Debug.Log("IT HIT SOMETHING");
        if(col.tag == "Bubble")
        {
            Debug.Log("Function");
            col.GetComponent<Bubble>().rb.velocity = Vector3.zero;
            //col.GetComponent<Bubble>().StopBubble();
        }
        
    }
}
