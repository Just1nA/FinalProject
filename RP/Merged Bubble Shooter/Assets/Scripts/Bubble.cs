using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

    public Transform shooter;
    public static bool isFired;
    public static bool once;
    public static bool swallHit;
    public static bool rotating;
    public static float xcoord;
    public static float ycoord;
    public GameObject StaticBall;
    public GameObject SpawnedBubble;
    public Rigidbody2D rb;
    public Transform newbubblepos;
    public float movespeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        isFired = false;
        once = false;
      
    }

    // Update is called once per frame
    void Update()
    {
        
        rotating = true;
        if(Input.GetKeyDown("space"))
        {
            if(once == false){
            isFired = true;
            rotating = false;
            once = true;
            }
              
        }
        
        if(isFired)
        {

        //transform.Translate(Vector3.up * Time.deltaTime * movespeed);
       // rb.velocity = new Vector3(10, 10, 0);
        //rb.AddRelativeForce(Vector2.up, ForceMode2D.Impulse);
        FIRE();
        isFired = false;
      // rb.AddForceAtPosition()
        xcoord = rb.velocity.x;
        ycoord = rb.velocity.y;
      //  rb.velocity = new Vector2(xcoord,ycoord);
      

        transform.parent = null;
       // Vector3 vel = rb.velocity;
        //transform.position += vel * Time.deltaTime;
        
           // transform.position =  Vector3.up * Time.deltaTime;
        } else if(rotating)
        {
            transform.rotation = shooter.rotation;
        }
    }

    public void StopBubble()
    {
         if(SpawnedBubble != null)
         {
            Instantiate(StaticBall, rb.position, Quaternion.identity);
            var newbub = Instantiate(SpawnedBubble, newbubblepos.position, Quaternion.identity);
            newbub.transform.parent = newbubblepos.transform;

         }

         Destroy(gameObject);
    }

    public void FIRE()
{
    rb.AddRelativeForce(Vector2.up * movespeed, ForceMode2D.Impulse);

}
}

