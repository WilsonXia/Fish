using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public GameObject entity;
    [SerializeField]
    bool isSchool;
    [SerializeField]
    float spawnRadius;
    [SerializeField]
    int spawnCount;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

    // Find a random point within it's center and radius
    Vector2 GetRandomPoint()
    {
        Vector2 center = new Vector2(transform.position.x, transform.position.y);
        Vector2 displacement = new Vector2(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius));
        return center + displacement;
    }

    // Spawn the entity
    void SpawnEntity()
    {
        if (!isSchool)
        {
            GameObject spawned = Instantiate(entity, GetRandomPoint(), Quaternion.identity);
            if (CheckIfFish(spawned))
            {
                // send to fish bin
                spawned.transform.SetParent(LevelSpawner.fishes.transform);
                // Randomize
                spawned.GetComponent<Fish>().Randomize();
            }
        }
        else
        {
            Instantiate(entity, transform.position, Quaternion.identity);
        }
    }

    bool CheckIfFish(GameObject gameObject)
    {
        return gameObject.CompareTag("Fish");
    }

    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnEntity();
        }
        // Remove once job is done
        if (transform.parent == null)
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
