using System.Collections.Generic;
using UnityEngine;

public static class Direction
{
    private static List<Vector2Int> mainDirectionals = new List<Vector2Int> {
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0)    
    };


    private static List<Vector2Int> edgeDirection = new List<Vector2Int>
    {
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(1, 1),
        new Vector2Int(1, -1),
        new Vector2Int(-1, 1),
        new Vector2Int(-1, -1)
    };


    private static Vector3 tilemapOffset = new Vector3(0.5f, 0.5f);


    public static List<Vector2Int> MainDirectionls { get => mainDirectionals; }

    public static List<Vector2Int> EdgeDirection { get => edgeDirection; }

    public static Vector3 TilemapOffset { get => tilemapOffset; }


    private static Vector2Int GetRandomFromList(List<Vector2Int> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }


    public static Vector2Int GetRandomMainDirection()
    {
        return GetRandomFromList(mainDirectionals);
    }


    public static Vector2Int GetRandomEdgeDirection()
    {
        return GetRandomFromList(edgeDirection);
    }
}
