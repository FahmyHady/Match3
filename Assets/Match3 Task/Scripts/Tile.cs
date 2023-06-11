using UnityEngine;
public enum TileType { Apple, PineApple, Banana, Orange, Strawberry, Kiwi }

public class Tile : MonoBehaviour
{
    public bool full;
    public TileType type;
    internal Vector2Int positionInGrid;
    internal GridBrain myBrains;
    MaterialPropertyBlock materialProperties;
    [SerializeField] Renderer rendererComponent; // Reference to the Renderer component
    private void Awake()
    {
        materialProperties = new MaterialPropertyBlock();
    }
    public void SetNeighbours(Tile[] _neighbours)
    {
        Neighbours = _neighbours;
    }
    public Tile[] Neighbours { get; private set; }

    public void SetType(TileType type)
    {
        Color color = default;
        this.type = type;
        switch (type)
        {
            case TileType.Apple:
                color = Color.red;
                break;
            case TileType.PineApple:
                color = Color.magenta;
                break;
            case TileType.Banana:
                color = Color.yellow;
                break;
            case TileType.Orange:
                color = new Color(1, 0.5f, 1);

                break;
            case TileType.Strawberry:
                color = new Color(0.6415094f, 0, 0.08120058f);
                break;

            case TileType.Kiwi:
                color = Color.green;
                break;
        }
        materialProperties.SetColor("_Color", color);
        rendererComponent.SetPropertyBlock(materialProperties);

    }
}