using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disjoint : MonoBehaviour
{
    private static int[] links;
    private static int[] ranks;
    //public static int[] connected_indices;
    public static List< List <int> > connections; 
    
    //declare 2D list

    public static void Print()
    {
        //Debug.Log(GridManager.colors[8,10]);
        for (int i = 0; i < links.Length; ++i)
        {
            //Debug.Log(links[i] + ", " + ranks[i]);
        }


        //Debug.Log("Node:  ");
        for (int i = 0; i < links.Length; i++) Debug.Log(i);  


        //Debug.Log("Links: ");
        for (int i = 0; i < links.Length; i++) Debug.Log(links[i]);  


        //Debug.Log("Ranks: ");
        for (int i = 0; i < links.Length; i++) Debug.Log(ranks[i]);  
 
        
    }

    public static void resize(int elements)
    {
        links = new int[elements];
        ranks = new int[elements];
        for (int i = 0; i < elements; ++i)
        {
            links[i] = -1;
            ranks[i] = 1;
        }

        //Debug.Log("Links size = " + links.Length);
        //Debug.Log("\nRanks size = " + ranks.Length);
    }

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

    public static int Find(int e)
    {
        int p,c; 

        // Find the root of the tree, but along the way, set
        // the parents' links to the children.
        c = -1; 
        while (links[e] != -1)
        {
            p = links[e];
            links[e] = c;
            c = e;
            e = p;
        }
        // Now, travel back to the original element, setting
        // every link to the root of the tree.
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

    public static void analyze_colors(int[,] colors)
    {
        int rows, cols;
        int index, right_index, top_index, top_left, top_right; 
        int current_leader, other_leader;
        
        //initialize rows and cols
        rows = GridManager.row;
        cols = GridManager.col;

        int[] v = new int[rows * cols];

        connections = new List < List <int> >(); 
        for (int i = 0; i < rows * cols; i++)
        {
            connections.Add(new List<int> ()); 
        }

        //connections[0].Add(3);  
        //Debug.Log("Connections = " + connections[0][0]);
        

        for (int i = 0; i < rows * cols; i++) v[i] = 1; //helper to store sizes of the set at indices

        for(int i = 0; i < cols; i++)
        {
            for(int j = 0; j < rows; j++)
            {
                index = i*11 + j; 
                //Debug.Log(i + ", " + j + " :" + index);
               
                right_index = index + rows;
                top_index = index + 1;
                top_left = top_index - rows;
                top_right = top_index + rows;

                //Debug.Log("right index = " + right_index + "\t top index = " + top_index + "\t top left index  = " + top_left);

                /* Check if the indices are out of bounds */
                if (colors[i,j] != 0)
                {
                    // Top Left Cell
                    if (j%2 == 1 && i-1 > 0 && j + 1 < rows)
                    {
                        if(colors[i,j] == colors[i-1, j+1])
                        {
                            //find the leaders(set id) of both the sets.
                            current_leader = Find(index);
                            other_leader = Find(top_left); 

                            //if they are disjoint, union them and sum-up the size of new leader
                            if(current_leader != other_leader)
                            {
                                Union(current_leader, other_leader);
                                v[Find(current_leader)] = v[current_leader] + v[other_leader];
                                //Debug.Log("Top Left cell check: size of v at " + Find(current_leader) + " (" + i + "," + j + ") = " + v[Find(current_leader)]);
                                
                            }
                        }
                    }

                    //Top right Cell
                    if(j%2 == 0 && i+1 < cols && j+1 < rows)
                    {
                        if(colors[i,j] == colors[i+1, j+1])
                        {
                            //find the leaders(set id) of both the sets.
                            current_leader = Find(index);
                            other_leader = Find(top_right); 

                            //if they are disjoint, union them and sum-up the size of new leader
                            if(current_leader != other_leader)
                            {
                                Union(current_leader, other_leader);
                                v[Find(current_leader)] = v[current_leader] + v[other_leader];
                                //Debug.Log("Top Left cell check: size of v at " + Find(current_leader) + " (" + i + "," + j + ") = " + v[Find(current_leader)]);
                                
                            }
                        }
                    }
                    //Top cell
                    if (j+1 < rows)
                    {
                        if(colors[i,j] == colors[i, j+1])
                        {
                            //find the leaders(set id) of both the sets.
                            current_leader = Find(index);
                            other_leader = Find(top_index); 

                            //if they are disjoint, union them and sum-up the size of new leader
                            if(current_leader != other_leader)
                            {
                                Union(current_leader, other_leader);
                                v[Find(current_leader)] = v[current_leader] + v[other_leader];
                                //Debug.Log("Top cell check: size of v at " + Find(current_leader) + " (" + i + "," + j + ") = " + v[Find(current_leader)]);
                            }
                        }
                    }

                    // Right Cell
                    if (right_index < rows * cols && i+1 < cols)
                    {
                        if(colors[i,j] == colors[i+1, j])
                        {
                            //find the leaders(set id) of both the sets.
                            current_leader = Find(index);
                            other_leader = Find(right_index); 

                            //if they are disjoint, union them and sum-up the size of new leader
                            if(current_leader != other_leader)
                            {
                                Union(current_leader, other_leader);
                                v[Find(current_leader)] = v[current_leader] + v[other_leader];
                                //Debug.Log("Right cell check: size of v at " + Find(current_leader) + " (" + i + "," + j + ") = " + v[Find(current_leader)]);
                            }
                        }
                    }
                }
            }
        }
        

        for(int i = 0; i < rows*cols; i++)
        {
            int y = i%rows;
            int x = i/rows;

            Debug.Log("x = " + x + "\t y = " + y);
            if(colors[x,y] != 0)
                connections[Find(i)].Add(i); // storing the leader indices
            
        }

        
        Debug.Log("Running through connections!");
        for (int i = 0; i < connections.Count; i++)
        {
            Debug.Log("Connections[" + i/rows + "," + i%rows + "] size: " + connections[i].Count);
            for (int j = 0; j < connections[i].Count; j++)
                Debug.Log("(" + i/rows + ", " + j%rows + ") = (" + connections[i][j]/rows + ", " + connections[i][j]%rows );
            
        }   
          
    } 

    public static void Pop_Connections()
    {
        /* Pseudo Code 

        This is done somewhere else so we can call the pop_connections function 
        if (connections[i].Count >= 3 && (bool) shot == True)
            Pop_Connections();


        */
        
        

    }
}