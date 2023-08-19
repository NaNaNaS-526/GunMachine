using System;
using UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Input
{
    public sealed class MobileInputService : MonoBehaviour, IInputService
    {
        public event Action<float> OnGunRotationChanged;
        public event Action<float> OnGunPowderCoefficientChanged;
        public event Action OnShootAction;

        [SerializeField] private MovementButton moveLeftButton;
        [SerializeField] private MovementButton moveRightButton;
        [SerializeField] private Slider gunRotationSlider;
        [SerializeField] private Slider gunPowderSlider;
        [SerializeField] private Button shootButton;

        private readonly CompositeDisposable _disposable = new();

        public float GetInputDirection()
        {
            if (moveLeftButton.IsPressed)
            {
                return -1;
            }

            if (moveRightButton.IsPressed)
            {
                return 1;
            }

            return 0;
        }

        private void OnEnable()
        {
            SetupControls();

            OnGunPowderCoefficientChanged?.Invoke(gunPowderSlider.value);
        }

        private void SetupControls()
        {
            gunRotationSlider.onValueChanged.AddListener(_ =>
            {
                OnGunRotationChanged?.Invoke(gunRotationSlider.value);
            });

            shootButton.OnClickAsObservable().Subscribe(_ => { OnShootAction?.Invoke(); }).AddTo(_disposable);
            gunPowderSlider.onValueChanged.AddListener(_ =>
            {
                OnGunPowderCoefficientChanged?.Invoke(gunPowderSlider.value);
            });
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
            gunPowderSlider.onValueChanged.RemoveAllListeners();
            gunRotationSlider.onValueChanged.RemoveAllListeners();
        }
    }
}