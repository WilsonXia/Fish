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
            Hook hook = GameManager.instance.hook;
            switch (GameManager.instance.fishingState)
            {
                case FishingState.Sink:
                    hook.LoseHealth(damage);
                    // If this is what caused it to get hooked
                    if (hook.Health <= 0)
                    {
                        hook.AddFish(this);
                    }
                    break;
                case FishingState.Hooked:
                    // attach to player
                    hook.AddFish(this);
                    break;
                case FishingState.Caught:
                    break;
                default:
                    break;
            }
        }
    }
}
