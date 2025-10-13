using UnityEngine;

public class Fish : MonoBehaviour
{
    // Fields
    [SerializeField]
    public int value;
    [SerializeField]
    int damage;

    // Movement AI
    [SerializeField]
    float maxDistance;
    float distance;
    [SerializeField]
    float speed;

    // Update is called once per frame
    void Update()
    {
        // Basic ahh movement
        Vector2 newPosition = new Vector2(0, 0);
        newPosition = Vector2.left * speed * Time.deltaTime;
        transform.position += new Vector3(newPosition.x, newPosition.y, 0);

        // Check if travelled distance reached
        distance += newPosition.x;
        if (distance >= maxDistance)
        {
            // Reset
            distance = 0;
            // reflect direction
            speed *= -1;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hook"))
        {
            print("Hook Detected");
            switch (GameManager.instance.fishingState)
            {
                case FishingState.Sink:
                    GameManager.instance.hook.LoseHealth(damage);
                    break;
                case FishingState.Hooked:
                    GameManager.instance.hook.LoseHealth(damage);
                    break;
                case FishingState.Caught:
                    break;
                default:
                    break;
            }

        }
    }
}
