using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementProcessor : MonoBehaviour
{
    public static MovementProcessor Current { get; private set; }
    public Vector2 MoveInput => _moveInput;

    [SerializeField]
    private Camera _currentCamera;

    private Vector2 _moveInput;

    private void Awake()
    {
        if (Current == null)
            Current = this;
        else
            Destroy(this);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public Vector3 GetMoveDirection()
    {
        Vector2 unprocessedDirection = _moveInput;

        Vector3 forward = _currentCamera.transform.forward;
        Vector3 right = _currentCamera.transform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 processedDirection = unprocessedDirection.y * forward + unprocessedDirection.x * right;

        return processedDirection;
    }
}