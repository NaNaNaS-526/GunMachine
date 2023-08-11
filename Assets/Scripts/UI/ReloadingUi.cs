using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ReloadingUi : MonoBehaviour
    {
        [SerializeField] private Image reloadingIndicator;

        private async UniTaskVoid Reloading(float reloadingTime)
        {
            float time = 0.0f;
            while (time <= reloadingTime)
            {
                time += Time.deltaTime;
                var currentAmount = Mathf.Lerp(0.0f, 1.0f, time / reloadingTime);
                reloadingIndicator.fillAmount = currentAmount;
                await UniTask.Yield();
            }
        }

        private void OnEnable()
        {
            EventBusUi.OnShot += Reloading;
        }


        private void OnDisable()
        {
            EventBusUi.OnShot -= Reloading;
        }
    }
}