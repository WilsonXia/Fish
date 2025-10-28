using UnityEngine;

public class StartingTrigger : MonoBehaviour
{
    public void TriggerStart()
    {
        // Switch the game mode to InGame
        GameManager.instance.gameState = GameState.InGame;
        // Hide the StartTrigger
        gameObject.SetActive(false);
    }
}
