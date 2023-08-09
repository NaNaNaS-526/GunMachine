using UnityEngine;
using Zenject;

namespace Installers
{
    public sealed class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private PlayerInput playerInputPrefab;

        public override void InstallBindings()
        {
            BindPlayerInput();
        }

        private void BindPlayerInput()
        {
            var playerInput = Container.InstantiatePrefabForComponent<PlayerInput>(playerInputPrefab);
            Container.Bind<PlayerInput>().FromInstance(playerInput).AsSingle();
        }
    }
}