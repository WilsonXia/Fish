using UnityEngine;

public class UITriggers : Observer
{
    // UI Pages
    public GameObject startingPage, endingPage;

    void Start()
    {
        // Initializing here so pages load consistently
        startingPage.SetActive(true);
        endingPage.SetActive(false);

        GameManager.instance.AddObserver(this);
    }

    public void TriggerStart()
    {
        // Switch the game mode to InGame
        GameManager.instance.gameState = GameState.InGame;
        // Hide the StartTrigger
        startingPage.SetActive(false);
    }

    public override void OnNotify(GameObject gObject, Observables observable)
    {
        if(observable == Observables.GameOver)
        {
            endingPage.SetActive(true);
        }
    }
}
