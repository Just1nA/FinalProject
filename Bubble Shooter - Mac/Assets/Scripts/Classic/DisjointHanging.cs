using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisjointHanging : MonoBehaviour
{
    private static int[] links;                    // Stores the links of the bubbles on the grid
    private static int[] ranks;                    // Stores the ranks of the bubbles on the grid
    public static List< List <int> > connections;  // Stores the connected bubbles
   
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

        /* Sets the parent and child according to the set id's */ 
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
        int rows, cols;
        int index, right_index, top_index, top_left, top_right; 
        int current_leader, other_leader;
        
        /* Initialize rows, cols, & the 2D list of connections */
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
                /* Calculates the indices around the current bubble */
                index = i*11 + j;               // Index value of 1D array 
                right_index = index + rows;     // Right bubble's index value in 1D array   
                top_index = index + 1;          // Top bubble's index value in 1D array
                top_left = top_index - rows;    // Top left bubble's index value in 1D array
                top_right = top_index + rows;   // Top right bubble's index value in 1D array

                /* Check if it's color value is not 0*/
                if (colors[i,j] != 0)
                {
                    /* Top Left Cell check */
                    if (j%2 == 1 && i-1 >= 0 && j + 1 < rows)
                    {
                        /* Check if the color value of the Top left cell is not 0 */
                        if(colors[i-1, j+1] != 0)
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
                        /* Check if the color value of Top Right cell is not 0 */
                        if(colors[i+1, j+1] != 0)
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
                        /* Check if the color value of Top cell is not 0*/
                        if(colors[i, j+1] != 0)
                        {
                            /* Find the leaders (set id) of both bubble sets (current vs. top). */
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
                        /* Check if the color value of right cell is not 0*/
                        if(colors[i+1, j] != 0)
                        {
                            /* Find the leaders (set id) of both bubble sets (current vs. right). */
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

        /* Cycles through the grid and add all the connected bubles whose color value is not 0*/
        for(int i = 0; i < rows*cols; i++)
        {
            int y = i%rows;
            int x = i/rows;
            if(colors[x,y] != 0) connections[Find(i)].Add(i);      
        }  
    } 

    /* Pops all bubbles that are isolated (hanging) from the rest of the grid */
    public static void popHanging(bool[,] topWall)
    {
        bool deleteObjects = false;     // Marks if a bubble should be deleted 
        int rows = GridManager.row;        
        int cols = GridManager.col;

        /*  Cycles through connections and pops the isolated bubbles */
        for (int i = 0; i < connections.Count; i++)
        {
            if (connections[i].Count > 0)
            {
                deleteObjects = true; // Marks bubble to be deleted 
               
                /* Cycles through the connected bubbles and check if any of them is connected to the topWall bubbles and mark their delete state to be false */
                for (int j = 0; j < connections[i].Count; j++) 
                {
                    if (topWall[(connections[i][j]/rows), (connections[i][j]%rows)] == true)
                    {   
                        deleteObjects = false;
                        break;
                    }
                }

                /* Pop all bubbles marked to be deleted */
                if (deleteObjects == true)
                {
                    for (int j = 0; j < connections[i].Count; j++) 
                    {
                        Destroy(GridManager.grid[(connections[i][j]/rows), (connections[i][j]%rows)]);  // Destroys the marked bubble GameObject
                        GridManager.colors[(connections[i][j]/rows), (connections[i][j]%rows)] = 0;     // Resets the connections back at those coordinates to 0
                    }
                }   
            }
        }
    }
}