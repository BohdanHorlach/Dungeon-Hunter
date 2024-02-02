using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerator
{
    public static HashSet<Vector2Int> GenerateToArea(Vector2Int start, int countStep)
    {
        HashSet<Vector2Int> result = new HashSet<Vector2Int>();

        result.Add(start);
        Vector2Int previousPosition = start;

        for (int i = 0; i < countStep; i++)
        {
            Vector2Int newPosition = previousPosition + Direction.GetRandomMainDirection();
            result.Add(newPosition);
            previousPosition = newPosition;
        }

        return result;
    }


    public static (HashSet<Vector2Int>, Vector2Int) GenerateToDirection(Vector2Int start, Vector2Int direction, int length, int width)
    {
        HashSet<Vector2Int> result = new HashSet<Vector2Int>();
        Vector2Int currentPosition = start;

        for (int i = 0; i < length; i++)
        {
            for (int j = -width; j <= width; j++)
            {
                Vector2Int asideOffset = Direction.GetRandomMainDirection() * j;
                result.Add(currentPosition + asideOffset);
            }
            result.Add(currentPosition);
            currentPosition += direction;
        }

        return (result, currentPosition);
    }
}