using UnityEngine;

[RequireComponent(typeof(LevelData))]

public class LevelSetup : MonoBehaviour
{
    LevelData levelData;
    [SerializeField]
    GameObject leftBound, rightBound, bottomBound;
    public float LeftBound { get { return leftBound.transform.position.x; } }
    public float RightBound { get { return rightBound.transform.position.x; } }

    void Awake()
    {
        levelData = GetComponent<LevelData>();
    }

    void Start()
    {
        bottomBound.transform.position = new Vector3(0, -levelData.Depth, 0);
    }
}
