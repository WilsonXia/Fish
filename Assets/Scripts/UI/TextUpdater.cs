using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextUpdater : Observer
{
    [SerializeField]
    string message;
    [SerializeField]
    Observables specificEvent;
    TextMeshProUGUI textMesh;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        // Connect with Hook
        Hook hookRef = GameManager.instance.hook;
        if (hookRef != null)
        {
            hookRef.AddObserver(this);
            OnNotify(hookRef.gameObject, specificEvent);
        }
        else
        {
            throw new Exception("A Hook reference was not found!");
        }
    }

    public override void OnNotify(GameObject gObject, Observables observable)
    {
        // Read parameters
        bool check = observable == specificEvent;
        Hook hookRef = gObject.GetComponent<Hook>();
        string updatedValue = "";
        // Check if we received the right event
        if (check)
        {
            // update value dependent upon UIUpdateEvent
            switch (observable)
            {
                case Observables.Health:
                    updatedValue = hookRef.Data.Health.ToString();
                    break;
                case Observables.Value:
                    updatedValue = hookRef.Data.Value.ToString();
                    break;
                default:
                    break;
            }
            // Update UI
            textMesh.text = message + " " + updatedValue;
        }
    }
}
