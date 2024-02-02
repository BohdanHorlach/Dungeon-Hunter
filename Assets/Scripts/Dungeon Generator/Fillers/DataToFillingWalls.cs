using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(fileName = "DataToFillingWalls", menuName = "Dungeon Hunter/DataToFillingWalls")]
public class DataToFillingWalls : ScriptableObject
{
    [SerializeField] private TileBase[] wallsFromUp;
    [SerializeField] private TileBase[] wallsFromDown;
    [SerializeField] private TileBase[] wallsFromRight;
    [SerializeField] private TileBase[] wallsFromLeft;
    [SerializeField] private TileBase[] wallsFromCornerUpRight;
    [SerializeField] private TileBase[] wallsFromCornerUpLeft;
    [SerializeField] private TileBase[] wallsFromCornerDownRight;
    [SerializeField] private TileBase[] wallsFromCornerDownLeft;


    private Dictionary<Vector2Int, TileBase[]> wallsByDirection;


    private void Awake()
    {
        wallsByDirection = new Dictionary<Vector2Int, TileBase[]>
        {
            { new Vector2Int(0, 1), wallsFromRight },
            { new Vector2Int(0, -1), wallsFromLeft },
            { new Vector2Int(1, 0), wallsFromUp },
            { new Vector2Int(-1, 0), wallsFromDown },
            { new Vector2Int(1, 1), wallsFromCornerUpRight },
            { new Vector2Int(1, -1), wallsFromCornerUpLeft },
            { new Vector2Int(-1, 1), wallsFromCornerDownRight },
            { new Vector2Int(-1, -1), wallsFromCornerDownLeft }
        };
    }


    public TileBase GetRandomTileWallFromDirection(Vector2Int direction)
    {
        TileBase[] tiles = wallsByDirection[direction];
        int indexTile = UnityEngine.Random.Range(0, tiles.Length);

        return tiles[indexTile];
    }
}