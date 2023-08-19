using Character.Gun;
using Character.Movement;
using Input;
using UniRx;
using UnityEngine;
using Zenject;

namespace Player
{
    public sealed class Player : MonoBehaviour
    {
        [SerializeField] private MovementController movementController;
        [SerializeField] private Gun gun;

        private IInputService _inputService;

        private readonly CompositeDisposable _disposable = new();

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void InputServiceOnGunRotationChanged(float rotationCoefficient)
        {
            gun.Rotate(rotationCoefficient);
        }

        private void InputServiceOnShootAction()
        {
            gun.Shoot();
        }
        private void InputService_OnGunPowderCoefficientChanged(float coefficient)
        {
            gun.ChangePowderAmount(coefficient);
        }

        private void OnEnable()
        {
            Observable.EveryUpdate()
                .Subscribe(_ => { movementController.Move(_inputService.GetInputDirection()); })
                .AddTo(_disposable);
        
            _inputService.OnGunRotationChanged += InputServiceOnGunRotationChanged;
            _inputService.OnShootAction += InputServiceOnShootAction;
            _inputService.OnGunPowderCoefficientChanged += InputService_OnGunPowderCoefficientChanged;
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
            _inputService.OnShootAction -= InputServiceOnShootAction;
        }
    }
}