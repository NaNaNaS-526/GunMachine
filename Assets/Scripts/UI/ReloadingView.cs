using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ReloadingView : MonoBehaviour
    {
        [SerializeField] private Image reloadingIndicator;

        public void UpdateReloadingIndicator(float currentAmount)
        {
            reloadingIndicator.fillAmount = currentAmount;
        }
    }
}