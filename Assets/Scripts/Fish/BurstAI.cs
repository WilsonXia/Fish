using UnityEngine;

public class BurstAI : FishAI
{
    // Burst
    [SerializeField]
    float pushForce;
    [SerializeField]
    float dragCoeff = 2.5f;
    // Timer
    float timer = 0;
    float moveCooldown = 2;

    public override void Start()
    {
        base.Start();
        isBoundaryLocked = true;
    }

    public override void Randomize()
    {
        base.Randomize();
        timer = Random.Range(0, moveCooldown);
    }

    public override void Move()
    {
        timer += Time.deltaTime;
        if(timer >= moveCooldown)
        {
            // Cut Velocity
            velocity = Vector2.zero;
            ApplyForce(direction * speed * pushForce);
            timer = 0;   
        }
        ApplyDrag(dragCoeff);
        base.Move();
    }
}
