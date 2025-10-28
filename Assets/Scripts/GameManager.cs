using UnityEngine;

public enum GameState
{
    Start,
    InGame,
    Pause,
    GameOver
}

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
    public GameState gameState = GameState.Start;
    public FishingState fishingState;
    public Hook hook;

    // Properties
    public bool InGame { get { return gameState == GameState.InGame; } }

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
