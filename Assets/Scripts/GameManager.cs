using UnityEngine;

public enum FishingState
{
    Sink,
    Hooked,
    Caught
}

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager instance;

    // Fields
    public Hook hook;
    public FishingState fishingState;
    public bool onPause = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void AdvanceFishState()
    {
        switch (fishingState)
        {
            case FishingState.Sink:
                fishingState = FishingState.Hooked;
                break;
            case FishingState.Hooked:
                fishingState = FishingState.Caught;
                break;
            case FishingState.Caught:
                break;
            default:
                break;
        }
    }
}
