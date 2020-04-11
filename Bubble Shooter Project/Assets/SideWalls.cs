using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWalls : MonoBehaviour
{
   void OnTriggerEnter2D (Collider2D col) 
    {
    
        
    
        Bubble.swallHit = true;
        Bubble.rotating = false;
        Debug.Log(Bubble.swallHit);
        Debug.Log("SMALL");
        if(col.tag == "Bubble")
        {
            //Debug.Log("Function");
           // col.GetComponent<Bubble>().rb.velocity = Vector3.zero;
           col.GetComponent<Bubble>().rb.gravityScale = 0;
            var tempx = transform.position.x;
            var tempy = transform.position.y;
           // tempz = tempz + tempz;
            tempx = tempx + tempx;
            col.GetComponent<Bubble>().rb.velocity = new Vector2(-tempx, -tempy);
            
            //swallHit = false;
        }
        
    }
}
