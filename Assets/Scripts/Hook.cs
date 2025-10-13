using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Hook : MonoBehaviour
{

    // Player Info --------------
    int health = 2;

    // Hooking ------------------
    public List<Fish> caughtFish = new List<Fish>();
    public int value = 0;

    // Movement ---------------------------------- 
    [SerializeField]
    float reelingSpeed = 0f;
    float descendSpeed = 2.0f;
    float speedBoost = 3.0f;
    InputAction reelAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        reelAction = InputSystem.actions.FindAction("Reel");
    }
    // Update is called once per frame
    void Update()
    {
        switch (GameManager.instance.fishingState)
        {
            case FishingState.Sink:
                // Player Info Check
                if (health <= 0)
                {
                    // Start Reeling
                    GameManager.instance.fishingState = FishingState.Hooked;
                }
                break;
            case FishingState.Hooked:
                break;
            case FishingState.Caught:
                break;
            default:
                break;
        }

        // Movement Check
        if (reelAction.IsPressed())
        {
            print("Boosting");
            reelingSpeed += speedBoost;
        }
        // Constant Speed
        reelingSpeed += descendSpeed;
        Movement(reelingSpeed);
        // Reset the speed for future calc
        reelingSpeed = 0;
    }

    // movement
    // Based on controller input
    // Held down, descend
    // TODO: Check if acceleration feels nicer
    void Movement(float speed)
    {
        Vector2 newPosition = new Vector2(0, 0);
        // Depends on FishingState
        switch (GameManager.instance.fishingState)
        {
            case FishingState.Sink: // Go Down
                newPosition = Vector2.down * speed * Time.deltaTime;
                break;
            case FishingState.Hooked: // Go Up
                newPosition = Vector2.up * speed * Time.deltaTime;
                break;
            case FishingState.Caught: // No movement
                break;
            default:
                break;
        }
        transform.position += new Vector3(newPosition.x, newPosition.y, 0);
    }

    public void LoseHealth(int amount)
    {
        health -= amount;
    }

    public void AddFish(Fish fish)
    {
        caughtFish.Add(fish);
    }

    void TallyValue()
    {
        foreach (Fish fish in caughtFish)
        {
            value += fish.value;
        }
    }
}
