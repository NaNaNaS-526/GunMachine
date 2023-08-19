using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Input
{
    public sealed class ComputerInputService : MonoBehaviour, IInputService
    {
        public event Action<float> OnGunRotationChanged;
        public event Action<float> OnGunPowderCoefficientChanged;
        public event Action OnShootAction;

        [SerializeField] private Slider gunRotationSlider;
        [SerializeField] private Slider gunPowderSlider;
        [SerializeField] private Button shootButton;

        private PlayerInputActions _playerInputActions;
        private readonly CompositeDisposable _disposable = new();

        public float GetInputDirection()
        {
            float inputDirection = _playerInputActions.Player.Move.ReadValue<float>();
            return inputDirection;
        }

        private void OnEnable()
        {
            SetupPlayerInputActions();
            SetupControls();

            OnGunPowderCoefficientChanged?.Invoke(gunPowderSlider.value);
        }

        private void SetupPlayerInputActions()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();
        }

        private void SetupControls()
        {
            _playerInputActions.Player.Shoot.performed += Shoot_OnPerformed;
            gunRotationSlider.onValueChanged.AddListener(_ =>
            {
                OnGunRotationChanged?.Invoke(gunRotationSlider.value);
            });
            gunPowderSlider.onValueChanged.AddListener(_ =>
            {
                OnGunPowderCoefficientChanged?.Invoke(gunPowderSlider.value);
            });
            shootButton.OnClickAsObservable().Subscribe(_ => { OnShootAction?.Invoke(); }).AddTo(_disposable);
        }

        private void Shoot_OnPerformed(InputAction.CallbackContext ctx)
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
            _disposable.Clear();
            _playerInputActions.Dispose();
            gunPowderSlider.onValueChanged.RemoveAllListeners();
            gunRotationSlider.onValueChanged.RemoveAllListeners();
        }
    }
}