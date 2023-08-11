using UniRx;
using UnityEngine;
using Zenject;

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

    private void InputService_OnGunRotationSliderValueChanged(float rotationCoefficient)
    {
        gun.Rotate(rotationCoefficient);
    }

    private void InputServiceOnShootAction()
    {
        gun.Shoot();
    }

    private void OnEnable()
    {
        Observable.EveryUpdate().Subscribe(_ => { movementController.Move(_inputService.GetInputDirection()); })
            .AddTo(_disposable);
        _inputService.OnGunRotationSliderValueChanged += InputService_OnGunRotationSliderValueChanged;
        _inputService.OnShootAction += InputServiceOnShootAction;
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