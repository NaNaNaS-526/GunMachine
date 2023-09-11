using System.Collections.Generic;
using UnityEngine;
namespace Character.Gun
{
    public class BulletPool
    {
        private readonly List<Rigidbody2D> _pool;
        private readonly BulletFactory _bulletFactory;
        private readonly Transform _bulletSpawnPoint;
        public bool AutoExpand { get; set; }

        public BulletPool(BulletFactory bulletFactory, int initialSize, Transform bulletSpawnPoint)
        {
            _bulletFactory = bulletFactory;
            _bulletSpawnPoint = bulletSpawnPoint;
            _pool = new List<Rigidbody2D>(initialSize);
            for (int i = 0; i < initialSize; i++)
            {
                _pool.Add(_bulletFactory.CreateBullet());
            }
        }

        public Rigidbody2D GetFreeBullet()
        {
            foreach (var bullet in _pool)
            {
                if (!bullet.gameObject.activeSelf)
                {
                    bullet.transform.position = _bulletSpawnPoint.transform.position;
                    bullet.gameObject.SetActive(true);
                    return bullet;
                }
            }

            if (AutoExpand)
            {
                var newBullet = _bulletFactory.CreateBullet(true);
                _pool.Add(newBullet);
                return newBullet;
            }

            return null;
        }
    }
}