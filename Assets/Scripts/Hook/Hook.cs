using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(HookData))]
[RequireComponent(typeof(HookMovement))]
public class Hook : MonoBehaviour
{
    [SerializeField]
    Transform attachFishPoint;
    [SerializeField]
    float padHeight = 0.4f;
    HookData data;
    HookMovement mov;
    public HookData Data { get { return data;}}
    
    // Initializing
    void Awake()
    {
        data = GetComponent<HookData>();
        mov = GetComponent<HookMovement>();
    }

    void Update()
    {
        switch (GameManager.instance.fishingState)
        {
            case FishingState.Sink:
                // Player Info Check
                if (Data.Health <= 0)
                {
                    // Start Reeling
                    GameManager.instance.AdvanceFishState();
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
        mov.MoveCheck(data);
    }

    // Manager Methods
    public void AddObserver(Observer obs)
    {
        data.AddObserver(obs);
    }
    public void LoseHealth(int amount)
    {
        data.Health -= amount;
        if (data.Health < 0)
        {
            data.Health = 0;
        }
    }
    
    void CalculateValue()
    {
        data.Value = 0;
        foreach (Fish fish in data.caughtFish)
        {
            data.Value += fish.value;
        }
    }

    public void AddFish(Fish fish)
    {
        // - Use a less expensive fish (sprite + info) for memory + computation efficieny?
        data.caughtFish.Add(fish);
        fish.gameObject.transform.SetParent(attachFishPoint); // attach to hookingpoint
        // Rotate for aesthetic
        if (attachFishPoint.childCount > 1)
        {
            RepositionFish();
        }
        else
        {
            // Caught just one fish
            // Reset position
            fish.gameObject.transform.localPosition = new Vector3(0, -fish.Size / 2, 0);
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
            float fishLength = child.GetComponent<Fish>().Size / 2;
            // print($"Step: {step} Angle:{angle} Fish:{child.name}");
            // Resets rotation
            child.rotation = Quaternion.Euler(0, 0, -90);
            // Reset position
            child.localPosition = new Vector3(0, 0, 0);
            // Rotates
            child.Rotate(new Vector3(0, 0, 1), angle);
            child.Translate(new Vector3(padHeight + fishLength, 0, 0));
            step++;
        }
    }
}
