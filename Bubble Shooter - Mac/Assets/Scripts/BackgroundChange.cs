using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChange : MonoBehaviour
{
    public Sprite background1, background2; 
    public int rowshift; 
    public GameObject background;

    /* This changes the background according to the mode the player chose */
    void Start()
    {
        rowshift = PlayerPrefs.GetInt("ROWSHIFTER");
        if (rowshift == 999999999)
            background.GetComponent<SpriteRenderer>().sprite = background1; 
        else
            background.GetComponent<SpriteRenderer>().sprite = background2; 
    }
}
