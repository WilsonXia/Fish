using UnityEngine;
using UnityEngine.InputSystem;

public class HookMovement : MonoBehaviour
{
    // Movement ---------------------------------- 
    [SerializeField]
    float currentSpeed = 0f;
    [SerializeField]
    float descendSpeed = 2.0f;
    [SerializeField]
    float horizontalSpeed = 5.0f;
    [SerializeField]
    float speedBoost = 3.0f;
    // Bounds
    public Transform leftBoundPoint, rightBoundPoint;
    Vector2 leftBound, rightBound;

    InputAction reelAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        reelAction = InputSystem.actions.FindAction("Reel");
        // Recover bounds
        leftBound = leftBoundPoint.position;
        rightBound = rightBoundPoint.position;
    }

    public void MoveCheck()
    {
        if (GameManager.instance.InGame)
        {
            if (GameManager.instance.fishingState != FishingState.Caught)
            {
                // Vertical Movement
                if (reelAction.IsPressed())
                {
                    // print("Boosting");
                    currentSpeed += speedBoost;
                }
                // Constant Speed
                currentSpeed += descendSpeed;

                Movement(currentSpeed);
                // Reset the speed for future calc
                currentSpeed = 0;
            }
        }
    }

    // movement
    // TODO: Check if acceleration feels nicer
    void Movement(float speed)
    {
        Vector2 newPosition = new Vector2(transform.position.x, transform.position.y);
        // Horizontal Movement

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        // sanitize it to fit in bounds
        mousePos.x = Mathf.Clamp(mousePos.x, leftBound.x, rightBound.x);
        // mouse - hook positions
        float toMouse = mousePos.x - transform.position.x;
        // Base it off the distance left it needs to cover
        // The farther it is from the mouse, the faster it moves
        if (toMouse > 0)
        {
            newPosition.x += horizontalSpeed * toMouse / 2 * Time.deltaTime;
        }
        else if (toMouse < 0)
        {
            newPosition.x += horizontalSpeed * toMouse / 2 * Time.deltaTime;
        }

        // Vertical Movement depends on FishingState
        switch (GameManager.instance.fishingState)
        {
            case FishingState.Sink: // Go Down
                newPosition += Vector2.down * speed * Time.deltaTime;
                break;
            case FishingState.Hooked: // Go Up
                newPosition += Vector2.up * speed * Time.deltaTime;
                break;
            case FishingState.Caught: // No movement
                break;
            default:
                break;
        }
        transform.position = newPosition;
    }
}
