using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


//Started this with 1:30 min left so code architechture may suffer
//Generates grid brains
public class GridGenerator : MonoBehaviour
{
    [SerializeField] Vector2 gridSize;
    [SerializeField] [Range(0, 0.5f)] float delayBetweenTiles;
    [SerializeField] [Range(0, 1)] float creationAnimationDuration;
    [SerializeField] [Range(0, 5)] float spacing;
    GameObject Grid;
    Coroutine gridGenerationRoutine;

    private void OnEnable()
    {
        EventManager.AddListener<int, int>(Events.General.GenerateNewGrid, GenerateGrid);
    }
    private void OnDisable()
    {
        EventManager.RemoveListener<int, int>(Events.General.GenerateNewGrid, GenerateGrid);
    }
#if UNITY_EDITOR
    [ContextMenu("Generate")]
    public void GenerateGrid()
    {
        StartCoroutine(GenerateGridRoutine());
    }
#endif
    public void GenerateGrid(int sizeX, int sizeY)
    {
        gridSize = new Vector2(sizeX, sizeY);
        StartCoroutine(GenerateGridRoutine());
    }
    IEnumerator GenerateGridRoutine()
    {
        if (gridGenerationRoutine != null)
            StopCoroutine(gridGenerationRoutine);
        Grid = GameObject.Find("Grid");
        if (Grid != null)
            DestroyImmediate(Grid);
        Grid = new GameObject("Grid");
        var thisGridBrain = Grid.AddComponent<GridBrain>();
        thisGridBrain.InitGrid((int)gridSize.x, (int)gridSize.y);
        gridGenerationRoutine = StartCoroutine(GenerateGridRoutine(thisGridBrain));
        yield return gridGenerationRoutine;

    }

    IEnumerator GenerateGridRoutine(GridBrain newBrain)
    {
        WaitForSeconds delay = new WaitForSeconds(delayBetweenTiles);
        Tile tile = Lookup.TilePrefab;
        Vector3 pos = Vector3.zero;
        Vector3 centerOffset = new Vector3((gridSize.x * 0.5f) - 0.5f, (gridSize.y * 0.5f) - 0.5f, 0);
        Camera.main.transform.position = Vector3.forward * -Mathf.Max(gridSize.x, gridSize.y); // center camera on newly created grid
        //Create Grid
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int k = 0; k < gridSize.y; k++)
            {
                pos = new Vector3(i * tile.transform.localScale.x, k * tile.transform.localScale.x, 0) - centerOffset;
                if (tile == null) break;
                Tile newTile = null;
                newTile = Instantiate(tile, pos, Quaternion.identity, Grid.transform);
                //We Can manipulate the newely created tile here
                newTile.SetType((TileType)Random.Range(0, Enum.GetValues(typeof(TileType)).Length));

                newTile.positionInGrid = new Vector2Int(i, k);
                newTile.myBrains = newBrain;

                newBrain.grid[i, k] = newTile;
                Utilities.LerpFloatValue(this, (a) => newTile.transform.localScale = Vector3.one * a, 0, 1, creationAnimationDuration);
                if (delayBetweenTiles > 0)
                    yield return delay;
            }
        }


        GridUtilities.FindAndSetNeighbours(newBrain);
        gridGenerationRoutine = null;

    }
}