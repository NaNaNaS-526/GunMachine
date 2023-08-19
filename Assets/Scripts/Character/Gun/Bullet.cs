using UnityEngine;

namespace Character.Gun
{
    public class Bullet : MonoBehaviour
    {
        private void OnCollisionEnter2D()
        {
            gameObject.SetActive(false);
        }
    }
}