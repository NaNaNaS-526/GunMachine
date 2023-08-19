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

        private float _currentBulletForce;
        private BulletPool _pool;
        private float _lastShotTime;
        private bool _isReady = true;

        private void Start()
        {
            _pool = new BulletPool(bulletPrefab, 5, bulletSpawnPoint)
            {
                AutoExpand = true
            };
            _currentBulletForce = baseBulletForce;
        }

        public void Shoot()
        {
            if (_lastShotTime > 0.0f) _isReady = _lastShotTime + reloadingTime <= Time.time;
            if (_isReady)
            {
                _lastShotTime = Time.time;
                var newBullet = _pool.GetFreeBullet();
                newBullet.AddForce(bulletSpawnPoint.right * _currentBulletForce, ForceMode2D.Impulse);
                OnShot?.Invoke(reloadingTime);
            }
        }

        public void ChangePowderAmount(float coefficient)
        {
            _currentBulletForce = baseBulletForce * coefficient;
        }

        public void Rotate(float rotationCoefficient)
        {
            var rotationAmount = rotationCoefficient * maxRotation;
            gun.rotation = Quaternion.Euler(0.0f, 0.0f, rotationAmount);
        }
    }
}