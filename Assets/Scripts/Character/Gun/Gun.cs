using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Character.Gun
{
    public class Gun : MonoBehaviour
    {
        public Func<float, UniTaskVoid> OnShot;

        [Header("Gun")] 
        [SerializeField] private Transform gun;
        [Range(0.0f, 50.0f)] [SerializeField] private float maxRotation;
        [Range(0.0f, 20.0f)] [SerializeField] private float reloadingTime;

        [Header("Bullet")] 
        [SerializeField] private Rigidbody2D bulletPrefab;
        [SerializeField] private Transform bulletSpawnPoint;

        [Range(0.0f, 1000.0f)] [SerializeField]private float baseBulletForce;

        private BulletPool _pool;
        private float _currentBulletForce;
        private float _lastShotTime;
        private bool _isReadyToShoot = true;

        private void Awake()
        {
            _currentBulletForce = baseBulletForce;
        }

        private void Start()
        {
            _pool = new BulletPool(bulletPrefab, 5, bulletSpawnPoint)
            {
                AutoExpand = true
            };
        }

        public void Shoot()
        {
            if (_lastShotTime > 0.0f) _isReadyToShoot = _lastShotTime + reloadingTime <= Time.time;
            if (_isReadyToShoot)
            {
                _lastShotTime = Time.time;
                var newBullet = _pool.GetFreeBullet();
                newBullet.AddForce(bulletSpawnPoint.right * _currentBulletForce, ForceMode2D.Impulse);
                OnShot?.Invoke(reloadingTime);
            }
        }

        public void Rotate(float rotationCoefficient)
        {
            var rotationAmount = rotationCoefficient * maxRotation;
            gun.rotation = Quaternion.Euler(0.0f, 0.0f, rotationAmount);
        }

        public void ChangePowderAmount(float coefficient)
        {
            _currentBulletForce = baseBulletForce * coefficient;
        }
    }
}