using UnityEngine;
public class Lookup
{

    public static Tile TilePrefab => Resources.Load<Tile>("Tile");
}