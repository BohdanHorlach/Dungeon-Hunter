using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TilemapFiller : MonoBehaviour
{
    [SerializeField] private Tilemap tilemapGround;
    [SerializeField] private Tilemap tilemapWalls;
    [SerializeField] private TileBase tileToGround;
    [SerializeField] private TileBase tileToWall;


    private void Cleaning()
    {
        tilemapGround.ClearAllTiles();
        tilemapWalls.ClearAllTiles();
    }


    private HashSet<Vector3Int> GetWallsFromCoordinates(Vector3Int coordinates)
    {
        List<Vector2Int> directions = Direction.EdgeDirection;
        HashSet<Vector3Int> walls = new HashSet<Vector3Int>();

        foreach (Vector3Int direction in directions)
        {
            Vector3Int cell = new Vector3Int(coordinates.x, coordinates.y);
            cell += direction;

            TileBase fillerCell = tilemapGround.GetTile(cell);

            if (fillerCell != tileToGround)
                walls.Add(cell);
        }

        return walls;
    }


    private HashSet<Vector3Int> FindWalls(IEnumerable<Vector3Int> positions)
    {
        HashSet<Vector3Int> walls = new HashSet<Vector3Int>();

        foreach (Vector3Int position in positions)
        {
            HashSet<Vector3Int> newWalls = GetWallsFromCoordinates(position);
            walls.UnionWith(newWalls);
        }

        return walls;
    }


    public void Fill(IEnumerable<Vector3Int> positions)
    {
        Cleaning();

        HashSet<Vector3Int> resultGround = new HashSet<Vector3Int>();
        resultGround.UnionWith(positions);

        IEnumerable<Vector3Int> positionOfHolesAndIrregularities = FindWalls(positions);
        resultGround.UnionWith(positionOfHolesAndIrregularities);

        foreach (Vector3Int position in resultGround)
            tilemapGround.SetTile(position, tileToGround);

        IEnumerable<Vector3Int> walls = FindWalls(positionOfHolesAndIrregularities);

        foreach (Vector3Int wall in walls)
            tilemapWalls.SetTile(wall, tileToWall);
    }
}