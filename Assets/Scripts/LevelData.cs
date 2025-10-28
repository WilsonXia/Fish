using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    [SerializeField]
    float depth;
    public float Depth { get { return depth; } }
    [SerializeField]
    List<GameObject> encounterableFish;
    public List<GameObject> Fishes {get { return encounterableFish; }}

    // Customization?
    // Color Palettes? Structures? Background?

    // Spawn Areas
    // Place sections between a spawnStartingHeight and spawnBottomHeight?
    // Create random circles or boxes that generate random points to spawn Fish into
    // - Acts like a spawner
    // - Spawn in patterns possibly, or randomly

    // Differ obstacles from fish, although possibly recycle the spawn areas code
}
