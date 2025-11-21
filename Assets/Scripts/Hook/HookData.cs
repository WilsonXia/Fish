using System.Collections.Generic;
using UnityEngine;

public class HookData : Subject
{
    // Player Info --------------
    [Header("Player Info")]
    [SerializeField]
    int maxHealth = 2;
    int health;
    public int Health { get { return health; } set { health = value; Notify(gameObject, Observables.Health); } }

    [SerializeField]
    float maxRGauge = 5f;
    public float MaxReelGauge { get { return maxRGauge; } set { maxRGauge = value; }}//Notify(gameObject, Observables.ReelGauge); } }
    float reelGauge; // Decides how long u can go down (seconds)
    public float ReelGauge { get { return reelGauge; } set { reelGauge = value; Notify(gameObject, Observables.ReelGauge); } }

    // Hooking ------------------
    [Header("Fish Info")]
    public List<Fish> caughtFish = new List<Fish>();
    [SerializeField]
    int value = 0;
    public int Value { get { return value; } set { this.value = value; Notify(gameObject, Observables.Value); } }

    // Initialize
    void Awake()
    {
        health = maxHealth;
        reelGauge = maxRGauge;
    }
}
