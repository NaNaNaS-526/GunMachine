using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Character.Gun
{
    public class BulletPool
    {
        private readonly Rigidbody2D _bulletPrefab;
        public bool AutoExpand { get; set; }
        private readonly Transform _parent;
        private List<Rigidbody2D> _pool;

        public BulletPool(Rigidbody2D bulletPrefab, int amount, Transform parent)
        {
            _bulletPrefab = bulletPrefab;
            _parent = parent;
            CreatePool(amount);
        }

        private void CreatePool(int amount)
        {
            _pool = new List<Rigidbody2D>();
            for (int i = 0; i < amount; i++)
            {
                CreateBullet();
            }
        }

        private void CreateBullet(bool isActiveByDefault = false)
        {
            var newBullet = Object.Instantiate(_bulletPrefab, _parent);
            newBullet.gameObject.SetActive(isActiveByDefault);
            _pool.Add(newBullet);
        }

        private bool HasFreeBullet(out Rigidbody2D bullet)
        {
            foreach (var element in _pool)
            {
                if (!element.gameObject.activeSelf)
                {
                    bullet = element;
                    element.gameObject.SetActive(true);
                    element.transform.position = _parent.position;
                    return true;
                }
            }

            bullet = null;
            return false;
        }

        public Rigidbody2D GetFreeBullet()
        {
            if (HasFreeBullet(out var bullet))
            {
                return bullet;
            }

            if (AutoExpand)
            {
                CreateBullet(true);
            }

            throw new Exception("There's no free bullet in pool ");
        }
    }
}