using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(HookData))]
[RequireComponent(typeof(HookMovement))]
public class Hook : Subject
{
    HookData data;
    HookMovement mov;
    public int Health { get { return data.Health; } }
    public int Value { get { return data.Value; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        data = GetComponent<HookData>();
        mov = GetComponent<HookMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        switch (GameManager.instance.fishingState)
        {
            case FishingState.Sink:
                // Player Info Check
                if (Health <= 0)
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
        mov.MoveCheck();
    }

    public void LoseHealth(int amount)
    {
        data.Health -= amount;
        if (data.Health < 0)
        {
            data.Health = 0;
        }
        Notify(gameObject, UIUpdateEvent.Health);
    }

    public void AddFish(Fish fish)
    {
        // Think about how to attach the fish
        // Attach sprite within a 120 degree range from the lowest point of the hook
        // Increment the rotation based on the number of fish
        // - Use a less expensive fish (sprite + info) for memory + computation efficieny?
        data.caughtFish.Add(fish);
        fish.gameObject.SetActive(false);
        CalculateValue();
    }

    void CalculateValue()
    {
        data.Value = 0;
        foreach (Fish fish in data.caughtFish)
        {
            data.Value += fish.value;
        }
        Notify(gameObject, UIUpdateEvent.Value);
    }
}
