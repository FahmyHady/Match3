using System;
using System.Collections.Generic;
using UnityEngine;

//There isn't enough time to add everything to clean up archeticture but I wrote everything myself
public class GridBrain : MonoBehaviour //Owns The Grid and runs commands on owned grid
{
    [SerializeField] internal Tile[,] grid;//all tiles represented by a 2D array
    internal List<Tile> tiles=new List<Tile>();//all tiles
    [field: SerializeField] public int GridLength { get; private set; }
    [field: SerializeField] public int GridWidth { get; private set; }

    public void InitGrid(int sizeX, int sizeY)
    {
        grid = new Tile[sizeX, sizeY];
        GridLength = sizeX;
        GridWidth = sizeY;
    }




}
