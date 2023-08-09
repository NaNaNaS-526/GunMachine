using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Installers
{
    public sealed class LocationInstaller : MonoInstaller
    {
        [SerializeField] private Slider gunSlider;
        [FormerlySerializedAs("playerController")] [SerializeField] private MovementController movementController;
        [SerializeField] private Transform spawnPoint;

        public override void InstallBindings()
        {
            BindGunRotationSlider();
            BindPlayerController();
        }

        private void BindGunRotationSlider()
        {
            Container.Bind<Slider>().FromInstance(gunSlider).NonLazy();
        }

        private void BindPlayerController()
        {
            var player = Container.InstantiatePrefabForComponent<MovementController>(movementController,
                spawnPoint.position,
                Quaternion.identity, null);
            Container.Bind<Transform>().FromInstance(player.transform).NonLazy();
        }
    }
}