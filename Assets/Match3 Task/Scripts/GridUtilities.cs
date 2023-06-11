using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class GridUtilities
{

    public static void FindAndSetNeighbours(GridBrain gridBrain)
    {
        int length = gridBrain.GridLength;
        int width = gridBrain.GridWidth;
        var grid = gridBrain.grid;
        //Teach Each Tile Who are his/her neighbours
        for (int i = 0; i < length; i++)
        {
            for (int k = 0; k < width; k++)
            {
                var tile = grid[i, k];
                List<Tile> neighbours = new List<Tile>();
                if (i < length - 1)
                    neighbours.Add(grid[i + 1, k]);
                if (i > 0)
                    neighbours.Add(grid[i - 1, k]);
                if (k < width - 1)
                    neighbours.Add(grid[i, k + 1]);
                if (k > 0)
                    neighbours.Add(grid[i, k - 1]);

                tile.SetNeighbours(neighbours.ToArray());

            }
        }
    }

    internal static void AssignTypes(GridBrain newBrain)
    {
        foreach (var item in newBrain.tiles)
        {
            item.SetType((TileType)Random.Range(0, Enum.GetValues(typeof(TileType)).Length));
        }
    }   

}
