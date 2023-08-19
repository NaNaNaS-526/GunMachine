using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ReloadingView : MonoBehaviour
    {
        [SerializeField] private Image reloadingIndicator;

        public async UniTaskVoid Reloading(float reloadingTime)
        {
            var time = 0.0f;
            while (time <= reloadingTime)
            {
                time += Time.deltaTime;
                var currentAmount = Mathf.Lerp(0.0f, 1.0f, time / reloadingTime);
                reloadingIndicator.fillAmount = currentAmount;
                await UniTask.Yield();
            }
        }
    }
}