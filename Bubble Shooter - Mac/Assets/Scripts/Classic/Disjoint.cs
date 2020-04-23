using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disjoint : MonoBehaviour
{
    private static int[] links;                         // Stores the links of the bubbles on the grid
    private static int[] ranks;                         // Stores the ranks of the bubbles on the grid
    public static List< List <int> > connections;       // Stores the connected bubbles 

    /* Resizes the links and ranks of the elements*/
    public static void resize(int elements)
    {
        links = new int[elements];
        ranks = new int[elements];
        
        for (int i = 0; i < elements; ++i)
        {
            links[i] = -1;
            ranks[i] = 1;
        }
    }

    /* Unionize two sets with leader s1 and s2 and returns the leader value of new set */
    public static int Union(int s1, int s2)
    {
        int p, c;
        if (ranks[s1] > ranks[s2])
        {
            p = s1;
            c = s2;
        }
        else
        {
            p = s2;
            c = s1;
        }
        links[c] = p;
        
        if (ranks[p] == ranks[c]) ranks[p]++;
        return p;
    }
    
    /* Cycles through the links of the specified bubble (e = index of bubble) and returns the leader of the set */
    public static int Find(int e)
    {
        int p,c; 
           
        /* Find the leader (root) of the set while setting the parents' links to the children */
        c = -1; 
        while (links[e] != -1)
        {
            p = links[e];
            links[e] = c;
            c = e;
            e = p;
        }
        /* Traverses back to the original element while setting every link to the root of the tree (Path Compression). */
        p = e;
        e = c;
        while (e != -1) 
        {
            c = links[e];
            links[e] = p;
            e =c;
        }
        return p;
    }

    /* Analyzes the grid */ 
    public static void analyze_colors(int[,] colors)
    {
        int rows, cols;                                              // Rows & columns of the grid
        int index, right_index, top_index, top_left, top_right;      // Surrounding bubbles' indices 
        int current_leader, other_leader;                            // Indices of the set leaders
        
        /* Initialize rows and cols */
        rows = GridManager.row;
        cols = GridManager.col;

        /* Resizing the connection (2D list) */
        connections = new List < List <int> >(); 
        for (int i = 0; i < rows * cols; i++)
            connections.Add(new List<int> ()); 
        
        /* Cycles through the grid of bubbles & performs union on all of the sets when necessary */
        for(int i = 0; i < cols; i++)
        {
            for(int j = 0; j < rows; j++)
            {
                index = i*11 + j;               // Index value of 1D array         
                right_index = index + rows;     // Right bubble's index value in 1D array
                top_index = index + 1;          // Top bubble's index value in 1D array
                top_left = top_index - rows;    // Top left bubble's index value in 1D array
                top_right = top_index + rows;   // Top right bubble's index value in 1D array

                /* Check if the indices are out of bounds */
                if (colors[i,j] != 0)
                {
                    /* Top Left Cell check */
                    if (j%2 == 1 && i-1 >= 0 && j + 1 < rows)
                    {
                        if(colors[i,j] == colors[i-1, j+1])
                        {
                            /* Find the leaders (set id) of both bubble sets (current vs. top left). */
                            current_leader = Find(index);
                            other_leader = Find(top_left); 

                            /* Unions them when disjoint */
                            if(current_leader != other_leader)
                                Union(current_leader, other_leader);
                        }
                    }

                    /* Top Right Cell check */
                    if(j%2 == 0 && i+1 < cols && j+1 < rows)
                    {
                        if(colors[i,j] == colors[i+1, j+1])
                        {
                            /* Find the leaders (set id) of both bubble sets (current vs. top right). */
                            current_leader = Find(index);
                            other_leader = Find(top_right); 

                            /* Unions them when disjoint */
                            if(current_leader != other_leader)
                                Union(current_leader, other_leader);
                        }
                    }
                    
                    /* Top Cell check */
                    if (j+1 < rows)
                    {
                        if(colors[i,j] == colors[i, j+1])
                        {
                            /* Find the leaders(set id) of both bubble sets (current vs. top). */
                            current_leader = Find(index);
                            other_leader = Find(top_index); 

                            /* Unions them when disjoint */
                            if(current_leader != other_leader)
                                Union(current_leader, other_leader);
                        }
                    }

                    /* Right Cell check */
                    if (right_index < rows * cols && i+1 < cols)
                    {
                        if(colors[i,j] == colors[i+1, j])
                        {
                            /* Find the leaders(set id) of both bubble sets (current vs. right) */
                            current_leader = Find(index);
                            other_leader = Find(right_index); 

                            /* Unions them when disjoint */
                            if(current_leader != other_leader)
                                Union(current_leader, other_leader);
                        }
                    }
                }
            }
        }
        
        /* Initializes the connections of the 2D list that keeps track of all connected bubbles in the set */
        for(int i = 0; i < rows*cols; i++)
        {
            int y = i%rows;
            int x = i/rows;
            
            if(colors[x,y] != 0) connections[Find(i)].Add(i); // Storing the leader indices            
        }
    } 

    /* Used to pop the bubbles that are in sets greater than 3 */
    public static bool Pop_Connections(int iSB, int jSB)
    {
        bool checkIfPopped = false;                 // Used in Grid Manager to do scoring and to end the game (win/lose)
        int rows = GridManager.row;             
        int cols = GridManager.col;
        int getColor = 0;
        int shooter_index = iSB*11 + jSB;           // Index of the shooter bubble based on the grid i,j values
        int shooter_leader = Find(shooter_index);   // Finds the shooter bubble's set leader 

        /* Finds a connection that has a size that is greater than 3 & make sure it is a shot bubble */
        if(connections[shooter_leader].Count >= 3)
        {
            /* Pops the bubbles that are connected to the shooter bubble */
            for(int i = 0; i < connections[shooter_leader].Count; i++)
            {
                int x = (connections[shooter_leader][i]/rows);
                int y = (connections[shooter_leader][i]%rows);

                Destroy(GridManager.grid[x, y]);
                getColor = GridManager.colors[x,y];
                GridManager.colors[x,y] = 0;  
            }
            getClusterScore(connections[shooter_leader].Count, getColor); // Scores the set 
            checkIfPopped = true;
        }
        return checkIfPopped; // Returns the state of the set that is popped
    }
        
    /* Scoring the sets based on size & color value */
    public static void getClusterScore(int clusterSize, int colorOfBubble)
    {
        GridManager.CONNECTIONSCORE = clusterSize * colorOfBubble;
    }  
}