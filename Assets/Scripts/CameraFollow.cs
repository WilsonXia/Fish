using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // Update y-pos for camera to match hook
        transform.position = new Vector3(transform.position.x, GameManager.instance.hook.transform.position.y, transform.position.z);
    }
}
