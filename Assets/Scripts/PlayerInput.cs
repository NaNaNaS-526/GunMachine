using System;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerInput : MonoBehaviour
{
    public event Action OnShootAction;

    private PlayerInputActions _playerInputActions;

    public float GetInputDirection()
    {
        float inputDirection = _playerInputActions.Player.Move.ReadValue<float>();
        return inputDirection;
    }

    private void Update()
    {
        GetInputDirection();
    }

    private void OnEnable()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Shoot.performed += Shoot_OnPerformed;
    }

    private void Shoot_OnPerformed(InputAction.CallbackContext obj)
    {
        OnShootAction?.Invoke();
    }

    private void OnDisable()
    {
        _playerInputActions.Disable();
    }

    private void OnDestroy()
    {
        _playerInputActions.Disable();
    }
}