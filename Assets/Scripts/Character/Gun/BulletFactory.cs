using UnityEngine;

namespace Character.Gun
{
    public class BulletFactory
    {
        private readonly Rigidbody2D _bulletPrefab;
        private readonly Transform _parent;
        public BulletFactory(Rigidbody2D bulletPrefab, Transform parent)
        {
            _bulletPrefab = bulletPrefab;
            _parent = parent;
        }
        public Rigidbody2D CreateBullet(bool isActiveByDefault = false)
        {
            var newBullet = Object.Instantiate(_bulletPrefab, _parent);
            newBullet.gameObject.SetActive(isActiveByDefault);
            return newBullet;
        }
    }
}