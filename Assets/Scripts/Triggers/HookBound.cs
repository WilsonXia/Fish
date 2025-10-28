using UnityEngine;
public class HookBound : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hook"))
        {
            // TODO: Notify peeps
            print("Hook bound reached");
            GameManager.instance.AdvanceFishState();
        }
    }
}
