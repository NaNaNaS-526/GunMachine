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

        [SerializeField] private Player.Player player;
        [SerializeField] private Gun gun;

        [SerializeField] private ReloadingView reloadingView;

        public override void InstallBindings()
        {
            BindPlayerInput();
            BindPlayer();
            BindReloadingItems();
        }

        private void BindPlayerInput()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor ||
                Application.platform == RuntimePlatform.WindowsPlayer)
            {
                Container
                    .Bind<IInputService>()
                    .To<ComputerInputService>()
                    .FromInstance(computerInputService)
                    .AsSingle();
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                Container
                    .Bind<IInputService>()
                    .To<MobileInputService>()
                    .FromInstance(mobileInputService)
                    .AsSingle();
            }
        }

        private void BindPlayer()
        {
            Container
                .Bind<Player.Player>()
                .FromInstance(player)
                .AsSingle();
            Container
                .Bind<Gun>()
                .FromInstance(gun)
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
    }
}