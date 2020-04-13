using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWalls : MonoBehaviour
{
   void OnTriggerEnter2D (Collider2D col) 
    {
    
        
    
        Bubble.swallHit = true;
        Bubble.rotating = false;
        Debug.Log("SMALL");
        if(col.tag == "Bubble")
        {
            Debug.Log("Function");
           var tempx = transform.position.x;
            var tempy = transform.position.y;
            var x = col.GetComponent<Bubble>().rb.velocity.x;
            var y = col.GetComponent<Bubble>().rb.velocity.y;

           // tempz = tempz + tempz;
          //   col.GetComponent<Bubble>().rb.velocity = new Vector2(-tempx, -tempy);

           // col.GetComponent<Bubble>().rb.velocity = rb.velocity.normalized * 10f;
           // col.GetComponent<Bubble>().rb.AddRelativeForce(new Vector2(-x, y), ForceMode2D.Force);
            
            //swallHit = false;
        }
        
    }
}
