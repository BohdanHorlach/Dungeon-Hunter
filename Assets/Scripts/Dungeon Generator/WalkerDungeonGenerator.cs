using NavMeshPlus.Components;
using System.Collections.Generic;
using UnityEngine;

public class WalkerDungeonGenerator : MonoBehaviour
{
    [Header("Fillers")]
    [SerializeField] private TilemapFiller tilemapFiller;
    [SerializeField] private InteractFiller interactFiller;


    [Header("NavMesh")]
    [SerializeField] private NavMeshSurface navMeshSurface;


    [Header("Rooms Settings")]
    [SerializeField, Min(1)] private int sizeDungeon;
    [SerializeField, Min(1)] private int countAreaIteration;
    [SerializeField, Min(1)] private int countStepFromArea;

    [Header("Corridors Settings")]
    [SerializeField, Range(1, 4)] private int maxCountOfCorridors;
    [SerializeField, Min(1)] private int lengthCorridors;
    [SerializeField, Min(1)] private int widthCorridors;
    [SerializeField, Min(1)] private int density;
    [SerializeField] private bool isGenerationAlongEdges = false;


    private readonly Vector2Int START_POSITION = new Vector2Int(0, 0);

    private HashSet<Vector2Int> dungeonMap = new HashSet<Vector2Int>();
    private List<Vector2Int> roomPositions = new List<Vector2Int>();


    private void Cleaning()
    {
        dungeonMap.Clear();
        roomPositions.Clear();
    }


    private List<Vector2Int> GetRandomDirectionsOfCorridors()
    {
        List<Vector2Int> resultDirections = new List<Vector2Int>();

        for(int i = 0; i < maxCountOfCorridors; i++)
        {
            Vector2Int direction;

            if (isGenerationAlongEdges == true)
                direction = Direction.GetRandomEdgeDirection();
            else
                direction = Direction.GetRandomMainDirection();

            if (resultDirections.Contains(direction) == false)
                resultDirections.Add(direction);
        }

        return resultDirections;
    }


    public void CreateLevel()
    {
        Cleaning();

        roomPositions.Add(START_POSITION);

        for (int i = 0; i < sizeDungeon; i++)
        {
            List<Vector2Int> directions = GetRandomDirectionsOfCorridors();

            for(int j = 0; j < directions.Count; j++)
            {
                Vector2Int newRoomPos = CorridorGeneration(roomPositions[i], directions[j]);
                roomPositions.Add(newRoomPos);
            }
        }

        foreach(Vector2Int roomPos in roomPositions)
        {
            RoomGeneretion(roomPos);
        }

        tilemapFiller.Fill(dungeonMap);
        interactFiller.Fill(dungeonMap, sizeDungeon);
        navMeshSurface.BuildNavMesh();
    }



    private void RoomGeneretion(Vector2Int roomPosition)
    {
        HashSet<Vector2Int> room = new HashSet<Vector2Int>();

        for (int i = 0; i < countAreaIteration; i++)
        {
            HashSet<Vector2Int> newArea = ProceduralGenerator.GenerateToArea(roomPosition, countStepFromArea);
            room.UnionWith(newArea);
        }

        dungeonMap.UnionWith(room);
    }


    private Vector2Int CorridorGeneration(Vector2Int startPosition, Vector2Int direction)
    {
        var resultCorridor = ProceduralGenerator.GenerateToDirection(startPosition, direction, lengthCorridors, widthCorridors, density);

        Vector2Int endPoint = resultCorridor.Item2;
        HashSet<Vector2Int> corridor = resultCorridor.Item1;

        dungeonMap.UnionWith(corridor);

        return endPoint;
    }
}
