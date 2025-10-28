using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(HookData))]
[RequireComponent(typeof(HookMovement))]
public class Hook : Subject
{
    [SerializeField]
    Transform attachFishPoint;
    [SerializeField]
     float padHeight = 0.4f;
    HookData data;
    HookMovement mov;
    public int Health { get { return data.Health; } }
    public int Value { get { return data.Value; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
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
        // - Use a less expensive fish (sprite + info) for memory + computation efficieny?
        data.caughtFish.Add(fish);
        fish.gameObject.transform.SetParent(attachFishPoint); // attach to hookingpoint
        // Reset position
        fish.gameObject.transform.localPosition = new Vector3(0, padHeight, 0);
        // Rotate for aesthetic
        if (attachFishPoint.childCount > 1)
        {
            RepositionFish();
        }
        else
        {
            fish.gameObject.transform.Rotate(new Vector3(0, 0, 1), -90f);
        }
        CalculateValue();
    }

    void RepositionFish()
    {
        // make sure fish models all place their sprite anchor to the head
        int step = 0;
        foreach (Transform child in attachFishPoint)
        {
            float angleStep = (float)step / (attachFishPoint.childCount - 1);
            float angle = Mathf.LerpAngle(-45, 45, angleStep);
            // print($"Step: {step} Angle:{angle} Fish:{child.name}");
            // Resets rotation
            child.rotation = Quaternion.Euler(0, 0, -90);
            // Rotates
            child.Rotate(new Vector3(0, 0, 1), angle);
            step++;
        }
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
