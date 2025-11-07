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
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + size / 2, transform.position.y));
    }

    void Awake()
    {
        size = GetComponent<SpriteRenderer>().sprite.bounds.size.x;
    }

    void Start()
    {
        fishAI = GetComponent<FishAI>();
        // Full length of fish
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
        if (collision.gameObject.CompareTag("Hook"))
        {
            Hook hook = GameManager.instance.hook;
            switch (GameManager.instance.fishingState)
            {
                case FishingState.Sink:
                    hook.LoseHealth(damage);
                    // If this is what caused it to get hooked
                    if (hook.Health <= 0)
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
    }
}
