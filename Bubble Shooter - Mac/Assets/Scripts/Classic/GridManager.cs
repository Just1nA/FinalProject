using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GridManager : MonoBehaviour
{
    public Transform Shooterstuff;                                  // Used to update position of shooter after row shifting the grid system
    public GameObject [] bubbles;                                   // Keeps track of bubble GameObjects in the grid
    public static GameObject [,] grid;                              // Grid system of bubbles that are loaded 
    public static int [,] colors;                                   // Keeps track of all the bubble colors in the grid
    public static bool[,] shotBubbles, topWall;                     // Keeps track of shot bubbles and bubbles that are in the grid's top row
    private int index = 0;                                          
    public int badrow;                                              // Keeps track of the bad area top row
    public static int row = 11, col = 9;                            // Specific grid dimensions
    public static int CONNECTIONSCORE, maxScore = 0, TOTALSCORE, updateYValue;    // Scoring variables and row shifting stuff
    int Vertical, Horizontal, Columns, Rows, UpdateRow;             // Camera, grid, and row shifting variables
    public Text scoreText, highscore;                               // Score Board
    public Transform WallTop;                                       // Used to update shape of the top wall when row shifting
    public int hscore, ROWSHIFTER, ROWSPAWN;                        // Used to print on the game screen                                             
    
    // Start is called before the first frame update
    void Start()
    {
        /* Keeps track of high score */
        hscore = PlayerPrefs.GetInt("HighScore");
        highscore.text = hscore.ToString();
        
        /* This generates the grid within the Main Camera persepective */
        Vertical = (int)Camera.main.orthographicSize;
        Horizontal = (int)(Vertical * Camera.main.aspect);
        
        /* Calculates the value of Rows and Columns based on axis */
        Columns = Horizontal * 2;
        Rows = Vertical * 2;
        badrow = 2;

        ROWSHIFTER = PlayerPrefs.GetInt("ROWSHIFTER");
        ROWSPAWN = PlayerPrefs.GetInt("ROWSPAWN");

        /* Creates the actual grid system and fills it with randomly generated bubbles */
        createGrid();
        spawnBubbles();

        /* Initialize disjoint sets on the grid upon start*/
        Disjoint.resize(row*col);
        Disjoint.analyze_colors(colors);

        /* Initializes scoring and row shifting variables upon start */
        CONNECTIONSCORE = 0;
        TOTALSCORE = 0;
        UpdateRow = 0;
        updateYValue = 0;
    }

    void Update()
    { 
        bool checkPopped = false;
        bool checkBadBubble = false;
        bool checkWinCondition = false;

        /* Row Shifting */
        if (UpdateRow == ROWSHIFTER)
        { 
            /* Moves down grid system after 5 shots */
            transform.position = new Vector2(transform.localPosition.x, transform.localPosition.y - 1); 
            Shooterstuff.position = new Vector2((float)-4.5, (float) -5.5);
            
            updateYValue++; // Update the column values of each bubbles after shifting the grid
            badrow++;       // Shifts the badrow area after shifting down the grid by a row
            UpdateRow = 0;  // Resets so it will shift the row again after another 5 shots 

            /* Checking for bubbles in the bad area and setting the boolean to send to "Game Over" screen */
            for (int i = 0; i < col; ++i)
            {
                if (colors[i, badrow] != 0) 
                {
                    checkBadBubble = true; 
                    break;
                }
            }   

            /* Transform the shape of the top wall to drop down a row */ 
            WallTop.localPosition =  new Vector2(WallTop.localPosition.x, WallTop.localPosition.y - 0.5f);
            WallTop.localScale = new Vector2(WallTop.localScale.x, WallTop.localScale.y + 6); 

            /* Loads "Game Over" screen after shifting rows */
            if (checkBadBubble == true)   
            {
                SceneManager.LoadScene("GameOver");
            }     
        }

        /* Add shooter bubble to grid and analyze the grid after every shoot*/ 
        if (Bubble.stopBubbleCalled == true)
        {
            UpdateRow++; // Increments the counter for bubbles shot so it can drop down a row after 5 shots

            addShooterTile(BubbleOnBoard.boardI, BubbleOnBoard.boardJ, SpawnShooterBub.sIndex); // Add shooter bubble to grid bubbles
            
            /* Update the grid by resizing and analyzing the Disjoint Sets to pop the connections */
            Disjoint.resize(row*col);
            Disjoint.analyze_colors(colors);
            
            checkPopped = Disjoint.Pop_Connections(BubbleOnBoard.boardI, BubbleOnBoard.boardJ); // Pops and scores sets that have 3 or more bubbles
            
            /* Update the grid by calling DisjointHanging to pop the hanging bubbles */
            DisjointHanging.resize(row*col);
            DisjointHanging.analyze_colors(colors);
            DisjointHanging.popHanging(topWall);
            
            /* Add the connection score to the total score and display it on the game board after every shot */ 
            TOTALSCORE += CONNECTIONSCORE;
            scoreText.text = TOTALSCORE.ToString();
            CONNECTIONSCORE = 0; 

            /* Checks if there are any bubbles left in the colors array to see if the player won the game */ 
            checkWinCondition = true;
            for (int i = 0; i < col; ++i)
            {
                for (int j = 0; j < row; ++j)
                {
                    if (colors[i, j] != 0) 
                    {
                        checkWinCondition = false;
                        break;
                    }    
                }
            }

            /* Loads "Game Over" screen after checking if bubbles have been popped in the bad area (every round, except when row shifting occurs */
            if(BubbleOnBoard.boardJ == badrow && checkPopped == false)
            {
                SceneManager.LoadScene("GameOver");
            }
        
            /* Loads "Winner" screen */
            if (checkWinCondition == true)
            {
                PlayerPrefs.SetInt("YourScore", TOTALSCORE);
                SceneManager.LoadScene("Winner");
            }
        }
    }
    
    /* Dynamically create a 2D GameObject grid & 2D color grid */
    private void createGrid()
    {
        grid = new GameObject[col, row];    // Stores the grid index of each bubble
        colors = new int[col, row];         // Stores the color value of each bubble
        shotBubbles = new bool[col, row];   // Stores the state of bubbles to see if they have been shot or not
        topWall = new bool[col, row];       // Stores the state of bubbles to check if they are connected to the top wall

        /* Cycles through the grid & initializes the state of bubbles in the top row to true to use it to drop the hanging bubbles later */
        for (int i = 0; i < col; ++i)
        {
            for (int j = 0; j < row; ++j)
            {
                if (j == row-1) topWall[i, j] = true; 
            }
        }
    }

    /* Generate a bubble grid on the main camera */
    private void spawnBubbles()
    {
        for (int i = 0; i < col; ++i)
        {
            for (int j = ROWSPAWN; j < row; ++j)
            {
                index = getRandom();
                addTile(i, j, index);
            }
        }
    }

    /* Add the tile bubble into main camera and add GameObject to 2d grid and add color to 2d colors */
    private void addTile(int i, int j, int index)
    {   
        if (index == 0) return; // Ignores the bubble with the color value of 0

        GameObject bubble = (GameObject)Instantiate(bubbles[index], transform);

        /* Offset the even columns */
        if (j % 2 == 0) 
        {
            bubble.transform.localPosition = new Vector2((i + 0.5f),  j);
        }
        /* No offset */
        else 
        {
            bubble.transform.localPosition = new Vector2(i,  j);
        }
        
        grid[i,j] = bubble; // Sets the current grid index to the bubble GameObject
        colors[i,j] = index; // Sets the current color value to the current bubble
    }

    /* Function that adds the shooter bubble */
    private void addShooterTile(int i, int j, int index)
    {   
        GameObject bubble = (GameObject)Instantiate(bubbles[index], transform);
        
        /* Offset the even columns */
        if (j % 2 == 0) 
        {
            bubble.transform.localPosition = new Vector2((i + 0.5f),  j);
        }
        /* No offset */
        else 
        {
            bubble.transform.localPosition = new Vector2(i,  j);
        }
        
        grid[i,j] = bubble; // Sets the current grid index to the bubble GameObject
        colors[i,j] = index; // Sets the current color value to the current bubble
        
        shotBubbles[i,j] = true; // Mark current bubble as shot
    }    

    /* Generates random bubbles to be used in the shooter and the grid */
    private int getRandom()
    {
        int randomNum = Random.Range(1, bubbles.Length);
        return randomNum;
    }
}