using UnityEngine;

[RequireComponent(typeof(LevelData))]

public class LevelSetup : MonoBehaviour
{
    [SerializeField]
    GameObject bottomBound;
    LevelData levelData;

    void Awake()
    {
        levelData = GetComponent<LevelData>();
    }

    void Start()
    {
        bottomBound.transform.position = new Vector3(0, levelData.Depth, 0);
    }
}
