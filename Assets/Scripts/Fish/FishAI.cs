using UnityEngine;

public class FishAI : MonoBehaviour
{
    // Movement Variables
    [SerializeField]
    protected float speed;
    protected Vector2 acceleration = Vector2.zero;
    // --------------------
    protected Vector2 direction = Vector2.left;
    protected Vector2 velocity = Vector2.zero;
    protected Vector2 position = Vector2.zero;


    // Sprite Flip
    protected bool isFacingRight = false;

    public virtual void Start()
    {
        position = transform.position;
    }
    
    public virtual void Randomize()
    {
        speed += Random.Range(-1f, 1f);
        if(Random.Range(0f, 0.5f) > 0.3f)
        {
            Flip();
        }
    }

    public virtual void Move()
    {
        // Physics Procedure
        // Add acceleration to velocity
        velocity += acceleration * Time.deltaTime;
        // Add velocity to position
        position += velocity * Time.deltaTime;
        // Normalize Direction
        // direction = velocity.normalized;
        transform.position = position;
        // Reset Acceleration
        acceleration = Vector2.zero;
    }

    public void ApplyForce(Vector2 force)
    {
        // f = ma, a = f/m later
        // acceleration acts as the sum of all forces
        acceleration += force;
    }
    public void ApplyDrag(float dragCoeff)
    {
        Vector2 drag = velocity * -1;
        drag.Normalize();
        drag *= dragCoeff;
        ApplyForce(drag);
    }

    // For now, direction is decided by Flip, but it should be the other way around
    // Check direction to see if it needs to Flip

    // TODO: Flip fish when they reach a side boundary
    //  Make a boundary trigger for that
    protected void Flip()
    {
        isFacingRight = !isFacingRight;
        GetComponent<SpriteRenderer>().flipX = isFacingRight;
        direction *= -1;
    }
}
