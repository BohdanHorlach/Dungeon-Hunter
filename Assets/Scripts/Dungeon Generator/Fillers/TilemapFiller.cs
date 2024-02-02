using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TilemapFiller : MonoBehaviour
{
    [SerializeField] private Tilemap tilemapGround;
    [SerializeField] private Tilemap tilemapWalls;
    [SerializeField] private TileBase[] tilesToGround;
    [SerializeField] private DataToFillingWalls dataToFillingWalls;



    private void Cleaning()
    {
        tilemapGround.ClearAllTiles();
        tilemapWalls.ClearAllTiles();
    }



    //Key is Position wall, Value is a direction wall.
    private List<KeyValuePair<Vector2Int, Vector2Int>> GetWallsFromCoordinates(Vector2Int coordinates)
    {
        List<Vector2Int> directions = Direction.EdgeDirection;
        List<KeyValuePair<Vector2Int, Vector2Int>> walls = new List<KeyValuePair<Vector2Int, Vector2Int>>();

        foreach (Vector2Int direction in directions)
        {
            Vector3Int cell = (Vector3Int)coordinates;
            cell += (Vector3Int)direction;

            TileBase fillerCell = tilemapGround.GetTile(cell);

            if (Array.IndexOf(tilesToGround, fillerCell) != -1)
                walls.Add(new KeyValuePair<Vector2Int, Vector2Int>((Vector2Int)cell, direction));
        }

        return walls;
    }


    private HashSet<KeyValuePair<Vector2Int, Vector2Int>> FindWalls(IEnumerable<Vector2Int> positions)
    {
        HashSet<KeyValuePair<Vector2Int, Vector2Int>> walls = new HashSet<KeyValuePair<Vector2Int, Vector2Int>>();

        foreach (Vector2Int position in positions)
        {
            List<KeyValuePair<Vector2Int, Vector2Int>> newWalls = GetWallsFromCoordinates(position);
            walls.UnionWith(newWalls);
        }

        return walls;
    }


    private void FillToGround(IEnumerable<Vector2Int> positions)
    {
        foreach (Vector3Int position in positions)
        {
            int indexTile = UnityEngine.Random.Range(0, tilesToGround.Length);
            tilemapGround.SetTile(position, tilesToGround[indexTile]);
        }
    }


    private void FillToWalls(IEnumerable<KeyValuePair<Vector2Int, Vector2Int>> walls)
    {
        foreach(var wall in walls)
        {
            Vector3Int position = (Vector3Int)wall.Key;
            Vector2Int direction = wall.Value;
            TileBase tile = dataToFillingWalls.GetRandomTileWallFromDirection(direction);

            tilemapWalls.SetTile(position, tile);
        }
    }


    public void Fill(IEnumerable<Vector2Int> positions)
    {
        Cleaning();

        HashSet<Vector2Int> resultGround = new HashSet<Vector2Int>();
        resultGround.UnionWith(positions);

        IEnumerable<Vector2Int> positionOfHolesAndIrregularities = FindWalls(positions).Select(x => x.Key);
        resultGround.UnionWith(positionOfHolesAndIrregularities);

        FillToGround(resultGround);

        IEnumerable<KeyValuePair<Vector2Int, Vector2Int>> walls = FindWalls(positionOfHolesAndIrregularities);

        FillToWalls(walls);
    }
}