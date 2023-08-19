using Character.Gun;
using Input;
using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public sealed class LocationInstaller : MonoInstaller
    {
        [SerializeField] private ComputerInputService computerInputService;
        [SerializeField] private MobileInputService mobileInputService;

        [SerializeField] private Player.Player playerPrefab;
        [SerializeField] private Transform spawnPoint;

        [SerializeField] private ReloadingView reloadingView;

        public override void InstallBindings()
        {
            BindPlayerInput();
            BindPlayer();
            BindReloadingItems();
        }

        private void BindPlayer()
        {
            var player = Container
                .InstantiatePrefab(playerPrefab, spawnPoint.position, Quaternion.identity, null);

            Container
                .Bind<Transform>()
                .FromInstance(player.transform)
                .AsSingle();
            Container
                .Bind<Gun>()
                .FromComponentOn(player)
                .AsSingle();
        }

        private void BindReloadingItems()
        {
            Container.Bind<ReloadingView>()
                .FromInstance(reloadingView)
                .AsSingle();
            Container.Bind<ReloadingPresenter>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerInput()
        {
            // if (Application.platform == RuntimePlatform.WindowsEditor ||
            //     Application.platform == RuntimePlatform.WindowsPlayer)
            // {
            //     Container
            //         .Bind<IInputService>()
            //         .To<ComputerInputService>()
            //         .FromInstance(computerInputService)
            //         .AsSingle()
            //         .NonLazy();
            //     Destroy(mobileInputService);
            // }
            if (Application.platform == RuntimePlatform.Android ||
                Application.platform == RuntimePlatform.WindowsEditor ||
                Application.platform == RuntimePlatform.WindowsPlayer)
            {
                Container
                    .Bind<IInputService>()
                    .To<MobileInputService>()
                    .FromInstance(mobileInputService)
                    .AsSingle()
                    .NonLazy();
                Destroy(computerInputService);
            }
        }
    }
}