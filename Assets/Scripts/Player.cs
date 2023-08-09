using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class Player : MonoBehaviour
{
    [SerializeField] private MovementController movementController;
    [SerializeField] private Gun gun;

    private PlayerInput _playerInput;
    private Slider _gunRotationSlider;

    [Inject]
    private void Construct(PlayerInput playerInput, Slider gunSlider)
    {
        _playerInput = playerInput;
        _gunRotationSlider = gunSlider;
    }

    private void Start()
    {
        Observable.EveryUpdate().Subscribe(_ => { movementController.Move(_playerInput.GetInputDirection()); });
        _gunRotationSlider.onValueChanged.AddListener(_ => { gun.RotateGun(_gunRotationSlider.value); });
        _playerInput.OnShootAction += (() => gun.Shoot(gun.bulletPrefab));
    }
}