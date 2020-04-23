using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShooterBub : MonoBehaviour
{
    public GameObject[] shooterBubs;         // Stores the shooter bubbles       
    public static GameObject shooterBub;     // Used to set the shooterBubs array   
    public static int sIndex;                // Used to get a random shooter bubble index 
    public Transform Axis;                   // Used to reset the position of the shooter bubbles (explained later)
    public Transform shooter;                // Used to match the shooter bubble's rotation to the shooter itself

    /* Start shooter instantiate */
    void Start()
    {
        sIndex = getRandom(); 

        /* Initiates the shooter bubble */
        shooterBub = (GameObject)Instantiate(shooterBubs[sIndex], transform.position, Quaternion.identity);
        
        /* Set the position of the shooter bubbles */
        shooterBub.transform.position = new Vector2((float)-0.158, (float) -4.561);
        shooterBub.transform.SetParent(Axis.transform);
    }

    /* Late update is used to prevent receiving the wrong color index */
    void LateUpdate()
    {   
        /* Create a new shooter bubble when it is empty */
        if (shooterBub == null)
        {
            sIndex = getRandom();
            shooterBub = (GameObject)Instantiate(shooterBubs[sIndex], transform.position, Quaternion.identity); // Generates new random shooter bubble
            shooterBub.transform.position = new Vector2((float)-0.158, (float) -4.561);
            shooterBub.transform.SetParent(Axis.transform);   // Moves the shooter bubbles back to its original coordinates 
        }      
        if (shooterBub != null) shooterBub.transform.rotation = shooter.transform.rotation; // Matches shooter bubble's rotation to shooter's rotation 
    }

    /* Generates random shooter bubble */
    private int getRandom()
    {
        int randomNum = Random.Range(1, shooterBubs.Length);
        return randomNum;
    }
}
