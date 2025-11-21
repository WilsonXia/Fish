using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class BarUpdater : Observer
{
    [SerializeField]
    Observables specificEvent = Observables.ReelGauge; // Reel Gauge for now
    Slider slider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider = GetComponent<Slider>();
        // Connect with Hook
        Hook hookRef = GameManager.instance.hook;
        if (hookRef != null)
        {
            hookRef.AddObserver(this);
            switch (specificEvent)
            {
                case Observables.ReelGauge:
                    slider.maxValue = hookRef.Data.MaxReelGauge;
                    break;
                default:
                    break;
            }
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
        // Check if we received the right event
        if (check)
        {
            // read value dependent upon UIUpdateEvent
            switch (observable)
            {
                case Observables.ReelGauge:
                    // Update UI
                    slider.value = Mathf.Lerp( hookRef.Data.MaxReelGauge, 0, hookRef.Data.ReelGauge / hookRef.Data.MaxReelGauge);
                    break;
                default:
                    break;
            }
        }
    }
}
