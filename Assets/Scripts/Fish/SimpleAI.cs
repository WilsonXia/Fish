using UnityEngine;

// enum FishMovement
// {
//     Basic,
//     Fast
// }
public class SimpleAI : FishAI
{
    public override void Move()
    {
        base.Move();
        BackAndForth();
    }
    
    void BackAndForth()
    {
        // AI SPECIFIC---------------------------------
        // Measure Distance Travelled
        MeasureDistanceTravelled();
        // Check if travelled distance reached
        if (distTravelled >= maxDistance)
        {
            // Reset
            distTravelled = 0;
            // reflect direction
            Flip();
        }
    }

    void MeasureDistanceTravelled()
    {
        if (speed > 0) // Going left
        {
            distTravelled += speed * Time.deltaTime;
        }
        else // Going Right
        {
            distTravelled += -speed * Time.deltaTime;
        }
    }
}
