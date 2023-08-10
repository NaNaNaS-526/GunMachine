using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ReloadingUi : MonoBehaviour
    {
        [SerializeField] private Image reloadingIndicator;

        private void Start()
        {
            EventBusUi.OnShot += StartReloading;
        }

        private void StartReloading(float reloadingTime)
        {
            reloadingIndicator.fillAmount = Mathf.Lerp(0.0f, 1.0f, Time.time / reloadingTime);
        }
    }
}