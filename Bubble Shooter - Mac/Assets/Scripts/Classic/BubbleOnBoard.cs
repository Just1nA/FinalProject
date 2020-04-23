using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleOnBoard : MonoBehaviour
{
    public static int boardI, boardJ;  // Indices used in the grid (different from World coordinates) 

    void OnTriggerEnter2D (Collider2D col) 
    {
        /* Checks for collisions between the shooter bubble and the grid bubbles */
        if(col.tag == "Bubble")
        {
            calculateCoordinates(col.GetComponent<Bubble>().gameObject.transform.localPosition.x, 
                                 col.GetComponent<Bubble>().gameObject.transform.localPosition.y + GridManager.updateYValue);
            col.GetComponent<Bubble>().rb.velocity = Vector3.zero;   // Sets the bubble's velocity to 0 to stop it
            col.GetComponent<Bubble>().StopBubble();                 // Destroys the moving bubble (it is replaced with a static one, but it is done somewhere else)
        }
    }

    /* Converts world coordinates to implemented grid system coordinates */
    void calculateCoordinates (double shooterX, double shooterY)
    {
        boardJ = Mathf.RoundToInt((float)shooterY);      // Calculates grid j coordinate after rounding the world coordinates to an integer
        if (boardJ % 2 == 0) shooterX -= 0.5;            // Even rows are offsetted in world x coordinates
        
        boardI = Mathf.RoundToInt((float)shooterX);      // Need to round converted x world coordinate to grid i coordinate
        
        if (boardI >= GridManager.col) boardI -= 1;      // Calculates the grid i coordinate value
        if (boardI < 0) boardI = 0;                      // Resets i coordinate if shooter bubble is out of bounds on the left side of the board 
    }
}
