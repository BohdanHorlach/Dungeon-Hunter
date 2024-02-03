using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "SpawnInteractSettings", menuName = "Dungeon Hunter/InteractFillerSettings")]
public class SpawnInteractSettings : ScriptableObject
{
    [SerializeField] private GameObject[] interactPrefabs;
    [SerializeField] private int density;


    private List<GameObject> spawnedObjects = new List<GameObject>();

    private void Cleaning()
    {
        foreach(GameObject gameObject in spawnedObjects)
        {
            Destroy(gameObject);
        }
    }


    public List<Vector2Int> RandomSpawn(List<Vector2Int> positions, Tilemap tilemap, int size)
    {
        Cleaning();

        int countIteration = size * density;
        List<Vector2Int> spawnedPosition = new List<Vector2Int>();

        for(int i = 0; i < countIteration; i++)
        {
            int randomSpawnIndex = Random.Range(0, positions.Count);
            int randomPrefabIndex = Random.Range(0, interactPrefabs.Length);
            Vector2Int spawnPos = positions[randomSpawnIndex];

            spawnedPosition.Add(spawnPos);
            GameObject gameObject = Instantiate(interactPrefabs[randomPrefabIndex], tilemap.CellToWorld((Vector3Int)spawnPos), Quaternion.identity);
            spawnedObjects.Add(gameObject);
        }

        return spawnedPosition;
    }
}