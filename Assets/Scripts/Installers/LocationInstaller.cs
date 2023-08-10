using UnityEngine;
using Zenject;

namespace Installers
{
    public sealed class LocationInstaller : MonoInstaller
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private Player playerPrefab;
        [SerializeField] private Transform spawnPoint;

        public override void InstallBindings()
        {
            BindPlayerInput();
            BindPlayer();
        }
        private void BindPlayer()
        {
            var player = Container
                .InstantiatePrefab(playerPrefab, spawnPoint.position, Quaternion.identity, null);
            
            Container
                .Bind<Transform>()
                .FromInstance(player.transform)
                .AsSingle();
        }

        private void BindPlayerInput()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            Container
                .Bind<IInputService>()
                .To<PlayerInput>()
                .FromComponentInNewPrefab(playerInput)
                .AsSingle()
                .NonLazy();
#endif
        }
    }
}