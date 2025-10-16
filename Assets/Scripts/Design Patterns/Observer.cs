using UnityEngine;

public enum UIUpdateEvent
{
    Health,
    Value
}
public abstract class Observer : MonoBehaviour
{
    public abstract void OnNotify(GameObject gObject, UIUpdateEvent uiUE);
}
