using UnityEngine;

[CreateAssetMenu(fileName = "DungeonSettingsForGenereting", menuName = "Dungeon Hunter/DungeonSettingsForGenereting")]
public class DungeonSettingsForGenereting : ScriptableObject
{
    [Header("Rooms Settings")]
    [SerializeField, Min(1)] private int sizeDungeon;
    [SerializeField, Min(1)] private int countAreaIteration;
    [SerializeField, Min(1)] private int countStepFromArea;

    [Header("Corridors Settings")]
    [SerializeField, Range(1, 4)] private int maxCountOfCorridors;
    [SerializeField, Min(1)] private int lengthCorridors;
    [SerializeField, Min(1)] private int widthCorridors;
    [SerializeField] private bool isGenerationAlongEdges = false;

    [Header("Corridors Settings")]
    [SerializeField] private SpawnInteractSettings[] interactSpawnSettings;


    public int SizeDungeon { get => sizeDungeon; }
    public int CountAreaIteration { get => countAreaIteration; }
    public int CountStepFromArea { get => countStepFromArea; }

    public int MaxCountOfCorridors { get => maxCountOfCorridors; }
    public int LengthCorridors { get => lengthCorridors; }
    public int WidthCorridors { get => widthCorridors; }
    public bool IsGenerationAlongEdges { get => isGenerationAlongEdges; }
    public SpawnInteractSettings[] InteractSpawnSetting { get => (SpawnInteractSettings[])interactSpawnSettings.Clone(); }
}