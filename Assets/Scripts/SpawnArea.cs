using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    [SerializeField]
    GameObject entity;
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
        Instantiate(entity, GetRandomPoint(), Quaternion.identity);
    }

    void Start()
    {
        for(int i = 0; i < spawnCount; i++)
        {
            SpawnEntity();
        }
    }
}
