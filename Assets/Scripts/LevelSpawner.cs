using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelData))]

public class LevelSpawner : MonoBehaviour
{
    LevelData levelData;

    public static GameObject fishes;

    [SerializeField]
    List<GameObject> spawners;
    [SerializeField]
    int spawnerAmount;

    [SerializeField]
    float spawnHeader = 20f;
    [SerializeField]
    float spawnIncrement = 5f;
    [SerializeField]
    float marginX = 3f;

    void Awake()
    {
        levelData = GetComponent<LevelData>();
        fishes = new GameObject("Fishes");
        fishes.transform.SetParent(transform);
    }

    void Start()
    {
        CreateSpawners();
    }

    // Tag For Utility
    public GameObject GetRandomObject(List<GameObject> gameObjects)
    {
        return gameObjects[Random.Range(0, gameObjects.Count)];
    }

    public GameObject CreateSpawner(Vector3 location)
    {
        GameObject spawner = Instantiate(GetRandomObject(spawners), location, Quaternion.identity);
        // Check if the spawner is a parent
        if (spawner.GetComponent<SpawnArea>() == null)
        {
            foreach (Transform child in spawner.transform)
            {
                child.GetComponent<SpawnArea>().entity = GetRandomObject(levelData.Fishes);
            }
        }
        else
        {
            spawner.GetComponent<SpawnArea>().entity = GetRandomObject(levelData.Fishes);
        }
        return spawner;
    }

    public void CreateSpawners()
    {
        for (int i = 0; i < spawnerAmount; i++)
        {
            // Adjust spawn parameters
            Vector3 spawnPos = Vector3.zero;
            // Randomize Position
            spawnPos.x = Random.Range(-marginX, marginX);
            spawnPos.y = spawnHeader + spawnIncrement * i;
            spawnPos.y *= -1; // to account for going down
            // Make stuff
            CreateSpawner(spawnPos);
        }
    }
}
