using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

    public Transform shooter;
    public static bool isFired;
    public Rigidbody2D rb;
    public float movespeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        isFired = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("space"))
        {
            isFired = true;
        }
        
        if(isFired)
        {
        //transform.Translate(Vector3.up * Time.deltaTime * movespeed);
        rb.AddRelativeForce(Vector3.up, ForceMode2D.Impulse);
           // transform.position =  Vector3.up * Time.deltaTime;
        } else
        {
            transform.rotation = shooter.rotation;
        }
    }
}
