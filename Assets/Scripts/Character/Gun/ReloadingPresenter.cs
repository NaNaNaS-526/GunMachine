using UI;
using Zenject;

namespace Character.Gun
{
    public class ReloadingPresenter
    {
        private readonly Gun _gun;
        private readonly ReloadingView _reloadingView;

        [Inject]
        public ReloadingPresenter(Gun gun, ReloadingView reloadingView)
        {
            _gun = gun;
            _reloadingView = reloadingView;
            RegisterSubscriptions();
        }

        private void RegisterSubscriptions()
        {
            _gun.OnShot += _reloadingView.Reloading;
        }
    }
}