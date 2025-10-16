using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextUpdater : Observer
{
    [SerializeField]
    string message;
    [SerializeField]
    UIUpdateEvent specificEvent;
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

    public override void OnNotify(GameObject gObject, UIUpdateEvent uiUE)
    {
        // Read parameters
        bool check = uiUE == specificEvent;
        Hook hookRef = gObject.GetComponent<Hook>();
        string updatedValue = "";
        // Check if we received the right event
        if (check)
        {
            // update value dependent upon UIUpdateEvent
            switch (uiUE)
            {
                case UIUpdateEvent.Health:
                    updatedValue = hookRef.Health.ToString();
                    break;
                case UIUpdateEvent.Value:
                    updatedValue = hookRef.Value.ToString();
                    break;
                default:
                    break;
            }
            // Update UI
            textMesh.text = message + " " + updatedValue;
        }
    }
}
