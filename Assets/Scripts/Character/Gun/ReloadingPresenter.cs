using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;
using Zenject;

namespace Character.Gun
{
    public class ReloadingPresenter
    {
        private readonly SimpleGun _gun;
        private readonly ReloadingView _reloadingView;

        [Inject]
        public ReloadingPresenter(SimpleGun gun, ReloadingView reloadingView)
        {
            _gun = gun;
            _reloadingView = reloadingView;
            RegisterSubscriptions();
        }

        private void RegisterSubscriptions()
        {
            _gun.OnShot += Reloading;
        }
        private async UniTaskVoid Reloading(float reloadingTime)
        {
            var time = 0.0f;
            while (time <= reloadingTime)
            {
                time += Time.deltaTime;
                var currentAmount = Mathf.Lerp(0.0f, 1.0f, time / reloadingTime);
                _reloadingView.UpdateReloadingIndicator(currentAmount);
                await UniTask.Yield();
            }
        }
    }
}