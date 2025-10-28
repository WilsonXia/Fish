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
    float distance;
    [SerializeField]
    float maxDistance;

    public void Move()
    {
        Vector3 distCovered = Vector3.left * speed * Time.deltaTime;
        transform.position += distCovered;
        // Check if travelled distance reached
        if (speed > 0)
        {
            distance += speed * Time.deltaTime;
        }
        else
        {

            distance -= speed * Time.deltaTime;
        }

        if (distance >= maxDistance)
        {
            // Reset
            distance = 0;
            // reflect direction
            speed *= -1;
        }
    }
}
