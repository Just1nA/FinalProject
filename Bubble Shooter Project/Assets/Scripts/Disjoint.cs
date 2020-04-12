using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disjoint : MonoBehaviour
{
    private static int[] links;
    private static int[] sizes;

    public static void Print()
    {
        Debug.Log(GridManager.colors[8,10]);
        for (int i = 0; i < links.Length; ++i)
        {
            Debug.Log(links[i] + ", " + sizes[i]);
        }
    }

    public static void resize(int elements)
    {
        links = new int[elements];
        sizes = new int[elements];
        for (int i = 0; i < elements; ++i)
        {
            links[i] = -1;
            sizes[i] = 1;
        }
    }

    public static int Union(int s1, int s2)
    {
        int p; int c;

        if (sizes[s1] > sizes[s2])
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
        sizes[p] += sizes[c];
        return p;
    }

    public static int Find(int element)
    {
        while (links[element] != -1) element = links[element];
        return element;
    }


}
