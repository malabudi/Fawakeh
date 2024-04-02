using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private BoxCollider2D boundaries;
    [SerializeField] private Transform fruitThrowTransform;

    public static bool IsDropping { get; set; }

    private PlayerInput playerInput;

    private InputAction touchPositionAction;
    private InputAction touchPressAction;

    private bool isTouching;

    private Bounds bounds;

    private float offset;

    private float leftBound;
    private float rightBound;
    private float startingLeftBound;
    private float startingRightBound;

    private void Awake()
    {
        // Initialize Input
        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions["TouchPress"];
        touchPositionAction = playerInput.actions["TouchPosition"];

        // Restrain Movement
        bounds = boundaries.bounds;

        offset = player.transform.position.x - fruitThrowTransform.position.x;

        leftBound = bounds.min.x + offset;
        rightBound = bounds.max.x + offset;

        startingLeftBound = leftBound;
        startingRightBound = rightBound;
    }

    private void OnEnable()
    {
        touchPressAction.started += TouchStarted;
        touchPressAction.performed += TouchPerformed;
        touchPressAction.canceled += TouchCanceled;
    }

    private void OnDisable()
    {
        touchPressAction.started -= TouchStarted;
        touchPressAction.performed -= TouchPerformed;
        touchPressAction.canceled -= TouchCanceled;
    }

    private void Update()
    {
        if (isTouching)
        {
            UpdatePlayerPosition();
        }
    }

    public void ChangeBoundary(float extraWidth)
	{
        leftBound = startingLeftBound;
        rightBound = startingRightBound;

        leftBound += ThrowFruitController.instance.fruitBounds.extents.x + extraWidth + 0.1f;
        rightBound -= ThrowFruitController.instance.fruitBounds.extents.x + extraWidth;
    }

    private void TouchStarted(InputAction.CallbackContext context)
    {
        isTouching = true;
        UpdatePlayerPosition();
    }

    // This method will handle dropping the fruit specifically
    private void TouchPerformed(InputAction.CallbackContext context)
	{
        if (ThrowFruitController.instance.CanDrop)
		{
            isTouching = false;
            IsDropping = true;
        }
    }

    private void TouchCanceled(InputAction.CallbackContext context)
    {
        isTouching = false;
        IsDropping = false;
    }

    private void UpdatePlayerPosition()
    {
        Vector2 touchPosition = touchPositionAction.ReadValue<Vector2>();
        Vector2 viewportPosition = Camera.main.ScreenToViewportPoint(touchPosition);
        Vector3 worldPosition = Camera.main.ViewportToWorldPoint(viewportPosition);

        worldPosition.z = player.transform.position.z;

        // Clamp the horizontal position within the specified bounds
        float clampedX = Mathf.Clamp(worldPosition.x, leftBound, rightBound);
        worldPosition.x = clampedX;

        // Keep the vertical position fixed
        worldPosition.y = player.transform.position.y;

        player.transform.position = worldPosition;
    }
}
