using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public sealed class PlayerInput : MonoBehaviour, IInputService
{
    public event Action<float> OnGunRotationSliderValueChanged;
    public event Action OnShootAction;

    [SerializeField] private Slider gunRotationSlider;
    [SerializeField] private Button shootingButton;

    private PlayerInputActions _playerInputActions;
    private CompositeDisposable _disposable = new();

    private void Update()
    {
        GetInputDirection();
    }

    public float GetInputDirection()
    {
        float inputDirection = _playerInputActions.Player.Move.ReadValue<float>();
        return inputDirection;
    }

    private void OnEnable()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Shoot.performed += Shoot_OnPerformed;
        gunRotationSlider.onValueChanged.AddListener(_ =>
        {
            OnGunRotationSliderValueChanged?.Invoke(gunRotationSlider.value);
        });
        shootingButton.OnClickAsObservable().Subscribe(_=>{OnShootAction?.Invoke();}).AddTo(_disposable);
    }

    private void Shoot_OnPerformed(InputAction.CallbackContext obj)
    {
        OnShootAction?.Invoke();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void RemoveListeners()
    {
        _playerInputActions.Dispose();
        gunRotationSlider.onValueChanged.RemoveAllListeners();

    }
}