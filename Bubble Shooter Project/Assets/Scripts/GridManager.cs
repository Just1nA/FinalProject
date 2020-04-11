using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject[] bubbles;
    public GameObject[,] grid;
    public int[,] colors;
    public Sprite sprite;
    private int index = 0;
    public int COL, ROW;
    int Vertical, Horizontal, Columns, Rows;
    

    // Start is called before the first frame update
    void Start()
    {
        Vertical = (int)Camera.main.orthographicSize;
        Horizontal = (int)(Vertical * Camera.main.aspect);
        Columns = Horizontal * 2;
        Rows = Vertical * 2;

        createGrid();
        spawnBubbles();
        Debug.Log(colors[0,5]);
        Debug.Log(colors[8,10]);
    }

    /* Dynamically create a 2d GameObject grid & 2d color grid */
    private void createGrid()
    {
        int row = 11; int col = 9;
        grid = new GameObject[col, row];
        colors = new int[col, row];
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
                addTile(i, j, index);
            }
        }
    }



    /* Add the tile bubble into main camera and add GameObject to 2d grid and add color to 2d colors */
    private void addTile(int i, int j, int index)
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
        Debug.Log(x + ", " + y);
        //Set GameObject bubble on 2d grid
        grid[x,y] = bubble;
        //Set color on 2d grid
        colors[x,y] = index;

    }

    /* Get a random element of the bubbles GameObject array */
    private int getRandom()
    {
        int randomNum = Random.Range(1, bubbles.Length);
        return randomNum;
    }
}
