using System.Collections.Generic;
using UnityEngine;

public class HookData : MonoBehaviour
{
    // Player Info --------------
    [Header("Player Info")]
    [SerializeField]
    int health = 2;
    public int Health { get { return health; } set { health = value; } }

    // Hooking ------------------
    [Header("Fish Info")]
    public List<Fish> caughtFish = new List<Fish>();
    [SerializeField]
    int value = 0;
    public int Value { get { return value; } set { this.value = value; } }
}
