using UnityEngine;

[RequireComponent(typeof(FishAI))]
public class Fish : MonoBehaviour
{
    // Fields
    [SerializeField]
    public int value;
    [SerializeField]
    int damage;
    bool isCaught = false;
    FishAI fishAI;
    float size;
    public float Size { get { return size; } }

    void OnDrawGizmos()
    {
        // Full length of fish
        size = GetComponent<SpriteRenderer>().sprite.bounds.size.x * transform.localScale.x;
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + size / 2, transform.position.y));
    }

    void Awake()
    {
        fishAI = GetComponent<FishAI>();
        // Full length of fish
        size = GetComponent<SpriteRenderer>().sprite.bounds.size.x * transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCaught && GameManager.instance.InGame)
            // Basic ahh movement
            fishAI.Move();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if hook collided
        if (collision.gameObject.CompareTag("Hook"))
        {
            Hook hook = GameManager.instance.hook;
            switch (GameManager.instance.fishingState)
            {
                case FishingState.Sink:
                    hook.LoseHealth(damage);
                    if (hook.Data.Health > 0)
                    {
                        // Flee
                        // Disable Interaction
                        GetComponent<BoxCollider2D>().isTrigger = false;
                        // swiftly move away from the hook
                        // Despawn
                        Destroy(gameObject);
                    }
                    else // Got Hooked
                    {
                        isCaught = true;
                        hook.AddFish(this);
                    }
                    break;
                case FishingState.Hooked:
                    // attach to player
                    if (!isCaught)
                    {
                        isCaught = true;
                        hook.AddFish(this);
                    }
                    break;
                case FishingState.Caught:
                    break;
                default:
                    break;
            }
        }
        else if (collision.gameObject.CompareTag("SideBound"))
        {
            if (fishAI.IsBounded)
            {
                fishAI.Bounce();
            }
        }
    }

    // Reference
    public void Randomize()
    {
        fishAI.Randomize();
    }
}
