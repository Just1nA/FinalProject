using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

    public Transform shooter;                               // Used to manipulates shooter's position 
    public static bool isFired, once, stopBubbleCalled;     // Used to keep track of states of the shooter and the moving shooter bubble
    public Rigidbody2D rb;                                  // Gives the shooter bubble its starting "shoot" force
    public float movespeed;                                 // Multiplier used when calculating new force to put on the shooter bubble

    void Start()
    {
        /* When new bubble is spawned, initialize all states to false */
        isFired = false;
        once = false;
        stopBubbleCalled = false;
    }

    void Update()
    {
        /* Changes the state of the bubble's isFired boolean when spacebar is pressed */
        if(Input.GetKeyDown("space"))
        {
            /* Ensures the movement command is only initiated once */
            if(once == false)
            {
                isFired = true;
                once = true;
            }     
        }
        /* Calls movement command and resets isFired */
        if(isFired)
        {
            rb.AddRelativeForce(Vector2.up * movespeed, ForceMode2D.Impulse); // Applies start force to shooter bubble 
            isFired = false; // Sets it back to state of non-firing
        } 
    }

    /* Changes stopBubbleCalled bool and deletes moving object - Used in BubbleOnBoard */
    public void StopBubble()
    {
        stopBubbleCalled = true;  
        Destroy(gameObject);    
    }   
}