using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject[] bubbles;
    public GameObject[,] grid;
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
        Columns = Horizontal * 2;
        Rows = Vertical * 2;

        createGrid();
        spawnBubbles();
           
    }

    /* Dynamically create a 2d GameObject grid */
    private void createGrid()
    {
        int row = 11; int col = 9;
        grid = new GameObject[col, row];
    }


    /* Generate a bubble grid on the main camera */
    private void spawnBubbles()
    {
        COL = Columns - 8;
        ROW = Rows - 1;
        
        for (int i = 7; i < COL; ++i)
        {
            for (int j = 5; j < ROW; ++j)
            {
                index = getRandom();
                addTile(i, j);
            }
        }
    }



    /* Add the tile bubble into main camera and add GameObject to 2d grid */
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
        int x = 0; int y = 0;
        x = i - 7;
        y = j;
        //x = ;
        //y = ;
                //Debug.Log(i + ", " + j);
        Debug.Log(x + ", " + y);
        grid[x,y] = bubble;
        //if (x == 0 && y == 5) Destroy(grid[x,y]);

    }

    /* Get a random element of the bubbles GameObject array */
    private int getRandom()
    {
        int randomNum = Random.Range(0, bubbles.Length);
        return randomNum;
    }
}
