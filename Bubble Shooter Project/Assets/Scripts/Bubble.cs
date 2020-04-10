using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

    public Transform shooter;
    public static bool isFired;
    public static bool wallHit;
    public static bool swallHit;
    public static bool rotating;
    public GameObject SpawnedBall;
    public Rigidbody2D rb;
    public float movespeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        isFired = false;
        wallHit = false;
        swallHit = false;
      
    }

    // Update is called once per frame
    void Update()
    {
        
        rotating = true;
        if(Input.GetKey("space"))
        {
            isFired = true;
            rotating = false;
              
        }
        
        if(isFired)
        {
        //transform.Translate(Vector3.up * Time.deltaTime * movespeed);
        
        rb.AddRelativeForce(Vector2.up, ForceMode2D.Force);
           // transform.position =  Vector3.up * Time.deltaTime;
        } else if(wallHit)
        {
           // rb.velocity = Vector3.zero;
        }else if(rotating)
        {
            transform.rotation = shooter.rotation;
        }else if(swallHit)
        {
            Debug.Log("smallwallhit");
            Debug.Log(swallHit);
            var tempx = transform.position.x * -1;
            var tempy = transform.position.y;
            var tempz = transform.position.z;
            tempx = tempx + tempx;
            rb.velocity = new Vector3(tempx, -8, tempz);
            
            swallHit = false;
        }
    }

    public void StopBubble()
    {
         if(SpawnedBall != null)
         {
            Instantiate(SpawnedBall, rb.position, Quaternion.identity);
         }

         Destroy(gameObject);
    }
}
