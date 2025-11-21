using UnityEngine;

// public enum Observables
// {
//     Health,
//     Value,
//     GameOver,
// }
public abstract class Observer : MonoBehaviour
{
    public abstract void OnNotify(GameObject gObject, Observables observable);
}
