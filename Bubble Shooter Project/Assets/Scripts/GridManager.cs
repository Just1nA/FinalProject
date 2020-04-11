using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject[] bubbles;
    public Sprite sprite;
    private int index = 0;
    public int COL, ROW;
    int Vertical, Horizontal, Columns, Rows;
    

    // Start is called before the first frame update
    void Start()
    {
        Vertical = (int)Camera.main.orthographicSize;
        Horizontal = (int)(Vertical * Camera.main.aspect);
        //Columns = Horizontal * 2;
        //Rows = Vertical * 2;
        Rows = Horizontal * 2;
        Columns = Vertical * 2;

        generateGrid();
           
    }

    /* Generate a bubble grid on the main camera */
    private void generateGrid()
    {
        ROW = Rows - 8;
        COL = Columns - 1;

        for (int i = 7; i < ROW; ++i)
        {
            for (int j = 5; j < COL; ++j)
            {
                index = getRandom();
                addTile(i, j);
            }
        }
    }

    /* Add the tile bubble into the camera grid */
    private void addTile(int i, int j)
    {   
        GameObject bubble = (GameObject)Instantiate(bubbles[index], transform);
        
        //Offset the odd columns
        if (j % 2 == 0) 
        {
            bubble.transform.position = new Vector2(i - (Horizontal - 0.5f) + 0.5f ,  j - (Vertical - 0.5f));
        }
        //Regular offset
        else 
        {
            bubble.transform.position = new Vector2(i - (Horizontal - 0.5f) ,  j - (Vertical - 0.5f));
        }

        //if (j == 8) Destroy(bubble);
    }

    /* Get a random element of the bubbles GameObject array */
    private int getRandom()
    {
        int randomNum = Random.Range(0, bubbles.Length);
        return randomNum;
    }
}
