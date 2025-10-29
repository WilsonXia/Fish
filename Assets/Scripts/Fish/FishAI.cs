using UnityEngine;

enum FishMovement
{
    Basic,
    Fast
}
public class FishAI : MonoBehaviour
{
    // Movement AI
    [SerializeField]
    FishMovement pattern;
    [SerializeField]
    float speed;
    float distTravelled;
    [SerializeField]
    float maxDistance;
    bool isFlipped = false;

    public void Start()
    {
        speed += Random.Range(-1f, 1f);
        distTravelled += Random.Range(0f, maxDistance - 0.3f);
        if(Random.Range(0f, 0.5f) > 0.3f)
        {
            Flip();
        }
    }

    public void Move()
    {
        Vector3 distCovered = Vector3.left * speed * Time.deltaTime;
        transform.position += distCovered;
        // Measure Distance Travelled
        MeasureDistanceTravelled();
        // Check if travelled distance reached
        if (distTravelled >= maxDistance)
        {
            // Reset
            distTravelled = 0;
            // reflect direction
            Flip();
        }
    }
    
    void MeasureDistanceTravelled()
    {
        if (speed > 0) // Going left
        {
            distTravelled += speed * Time.deltaTime;
        }
        else // Going Right
        {
            distTravelled += -speed * Time.deltaTime;
        }
    }

    void Flip()
    {
        isFlipped = !isFlipped;
        GetComponent<SpriteRenderer>().flipX = isFlipped;
        speed *= -1;
    }
}
