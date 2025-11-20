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

    void SwitchDirection()
    {
        // Record new position
        previousPos = position;
        // cut velocity
        velocity = Vector2.zero;
        // reflect direction
        Flip();
    }
    void BackNForth()
    {
        if (!isFacingRight)
        {
            if (previousPos.x - position.x >= maxDistance)
            {
                SwitchDirection();
            }
            else if (previousPos.x - position.x >= maxDistance / 2)
            {
                // Start slowing if at midpoint
                print("slowing down!!");
                ApplyDrag(speed * 1.8f);
            }
        }
        else
        {
            if (position.x - previousPos.x >= maxDistance)
            {
                SwitchDirection();
            }
            else if(position.x - previousPos.x >= maxDistance / 2)
            {
                ApplyDrag(speed * 1.8f);
            }
        }
        // MOVE HERE
        ApplyForce(direction * speed);
    }
}