using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour
{
    // Potentially have differents list of observers for different events,
    // To avoid calling all observers when only a few are observing a specified event
    List<Observer> observers = new List<Observer>();

    public virtual void AddObserver(Observer observer)
    {
        observers.Add(observer);
    }

    public virtual void RemoveObserver(Observer observer)
    {
        observers.Remove(observer);
    }

    protected virtual void Notify(GameObject gObject, Observables observable)
    {
        foreach(Observer observer in observers)
        {
            observer.OnNotify(gObject, observable);
        }
    }
}
