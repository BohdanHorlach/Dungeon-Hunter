using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;


public class InteractFiller : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private int minDistanceFromCenter;


    private bool IsEdgeOfArea(Vector2Int position)
    {
        foreach(Vector2Int edge in Direction.EdgeDirections)
        {
            Vector3Int edgePosition = (Vector3Int)(position + edge);

            if(tilemap.GetTile(edgePosition) == null)
            {
                return true;
            }
        }

        return false;
    }


    private List<Vector2Int> GetValidPositionFomDistance(IEnumerable<Vector2Int> positions)
    {
        return positions
            .Where(position => Vector2Int.Distance(Vector2Int.zero, position) >= minDistanceFromCenter)
            .Where(position => IsEdgeOfArea(position) == false)
            .ToList();
    }


    public void Fill(SpawnInteractSettings[] spawnsSetting, IEnumerable<Vector2Int> positions, int sizeMap)
    {

        List<Vector2Int> validPos = GetValidPositionFomDistance(positions);

        foreach (SpawnInteractSettings spawner in spawnsSetting)
        {
            List<Vector2Int> spawnedPositions = spawner.RandomSpawn(validPos, tilemap, sizeMap);
            List<Vector2Int> newValidPos = validPos.Except(spawnedPositions).ToList();

            validPos = newValidPos;
        }
    }
}