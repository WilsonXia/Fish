using UnityEngine;

// enum FishMovement
// {
//     Basic,
//     Fast
// }
public class FishAI : MonoBehaviour
{
    // Experimental
    // [SerializeField]
    // FishMovement pattern;

    // Movement Variables
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float acceleration;
    // --------------------
    protected Vector2 direction = Vector2.left;
    protected Vector2 velocity = Vector2.zero;
    protected Vector2 position = Vector2.zero;

    // Distance related
    protected float distTravelled;
    [SerializeField]
    protected float maxDistance;

    // Sprite Flip
    protected bool isFlipped = false;

    public void Start()
    {
        position = transform.position;
    }
    
    public void Randomize()
    {
        speed += Random.Range(-1f, 1f);
        distTravelled += Random.Range(0f, maxDistance - 0.3f);
        if(Random.Range(0f, 0.5f) > 0.3f)
        {
            Flip();
        }
    }

    public virtual void Move()
    {
        // Physics Procedure
        velocity = direction * speed * Time.deltaTime;
        position += velocity;
        transform.position = position;

        // BackAndForth();
    }

    // For now, direction is decided by Flip, but it should be the other way around
    // Check direction to see if it needs to Flip
    protected void Flip()
    {
        isFlipped = !isFlipped;
        GetComponent<SpriteRenderer>().flipX = isFlipped;
        direction *= -1;
    }
}
