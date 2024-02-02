using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TilemapFiller : MonoBehaviour
{
    [SerializeField] private Tilemap tilemapGround;
    [SerializeField] private Tilemap tilemapWalls;
    [SerializeField] private TileBase[] tilesToGround;
    [SerializeField] private TileBase[] tilesToWall;
    [SerializeField, Min(0)] private int smoothing = 3;



    private void Cleaning()
    {
        tilemapGround.ClearAllTiles();
        tilemapWalls.ClearAllTiles();
    }



    //Key is Position wall, Value is a direction wall.
    private List<Vector2Int> GetWallsFromCoordinates(Vector2Int coordinates)
    {
        List<Vector2Int> directions = Direction.EdgeDirections;
        List<Vector2Int> walls = new List<Vector2Int>();

        foreach (Vector2Int direction in directions)
        {
            Vector3Int cell = (Vector3Int)(coordinates + direction);

            TileBase fillerCell = tilemapGround.GetTile(cell);

            if (fillerCell == null)
                walls.Add((Vector2Int)cell);
        }

        return walls;
    }


    private HashSet<Vector2Int> FindWalls(IEnumerable<Vector2Int> positions)
    {
        HashSet<Vector2Int> walls = new HashSet<Vector2Int>();

        foreach (Vector2Int position in positions)
        {
            List<Vector2Int> newWalls = GetWallsFromCoordinates(position);
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


    private void FillToWalls(IEnumerable<Vector2Int> walls)
    {
        List<Vector2Int> directions = Direction.MainDirections;
        foreach(Vector3Int wall in walls)
        {
            foreach (Vector2Int direction in directions)
            {
                Vector3Int position = wall + (Vector3Int)direction;
                if (tilemapWalls.GetTile(position) == null)
                {
                    int indexTile = UnityEngine.Random.Range(0, tilesToWall.Length);
                    tilemapWalls.SetTile(wall, tilesToWall[indexTile]);
                }
            }
        }
    }


    public void Fill(IEnumerable<Vector2Int> positions)
    {
        Cleaning();

        FillToGround(positions);

        IEnumerable<Vector2Int> positionOfHolesAndIrregularities = FindWalls(positions);

        for (int i = 0; i < smoothing; i++)
        {
            FillToGround(positionOfHolesAndIrregularities);
            positionOfHolesAndIrregularities = FindWalls(positionOfHolesAndIrregularities);
        }

        FillToWalls(positionOfHolesAndIrregularities);
    }
}