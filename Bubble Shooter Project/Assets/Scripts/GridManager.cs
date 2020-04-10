using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject[] bubbles;
    public Sprite sprite;
    private int index = 0;
    //public float[,] grid;
    //public static int ROW = 10;
    //public static int COL = 9;
    int Vertical, Horizontal, Columns, Rows;
    

    // Start is called before the first frame update
    void Start()
    {
        Vertical = (int)Camera.main.orthographicSize;
        Horizontal = (int)(Vertical * Camera.main.aspect);
        Columns = Horizontal * 2;
        Rows = Vertical * 2;
        //grid = new float[Columns, Rows];

        

        for (int i = 10; i < Columns; ++i)
        {
            for (int j = 10; j < Rows; ++j)
            {
                index = getRandom();
                //GameObject referenceTile = (GameObject)bubbles[index];
                GameObject tile = (GameObject)Instantiate(bubbles[index], transform);
                tile.transform.position = new Vector2(i - (Horizontal - 0.5f) ,  j - (Vertical - 0.5f));
                //grid[i, j] = Random.Range(0.0f, 1.0f); 
                //GenerateGrid(i , j, grid[i, j]);
            }
        }
           
    }

/*
    private void GenerateGrid(int x, int y, float value)
    {
       //GameObject g = (GameObject)Instantiate(Resources.Load("Bubble"));
       GameObject g = new GameObject("X: "+x+"Y: "+y);
       g.transform.position = new Vector3(x - (Horizontal - 0.5f), y - (Vertical - 0.5f));
       var s = g.AddComponent<SpriteRenderer>();
       s.sprite = sprite;
       s.color = new Color(value, value, value); 
    }
*/

    private int getRandom()
    {
        int randomNum = Random.Range(0, 5);
        return randomNum;
    }
}
