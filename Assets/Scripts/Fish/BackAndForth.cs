using UnityEngine;

public class BackAndForth : FishAI
{
    Vector2 previousPos;

    [SerializeField]
    float maxDistance;

    public override void Start()
    {
        base.Start();
        previousPos = position;
    }
    public override void Move()
    {
        BackNForth();
        base.Move();
    }
    void BackNForth()
    {
        if (!isFacingRight)
        {
            if (previousPos.x - position.x >= maxDistance)
            {
                // Record new position
                previousPos = position;
                // cut velocity
                velocity = Vector2.zero;
                // reflect direction
                Flip();
            }
        }
        else
        {
            if (position.x - previousPos.x >= maxDistance)
            {
                // Record new position
                previousPos = position;
                // cut velocity
                velocity = Vector2.zero;
                // reflect direction
                Flip();
            }
        }
        ApplyForce(direction * speed);
    }
}