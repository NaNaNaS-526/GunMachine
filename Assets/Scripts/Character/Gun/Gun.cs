using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Character.Gun
{
    public abstract class Gun : MonoBehaviour
    {
        public Func<float, UniTaskVoid> OnShot;

        [Header("Gun")] 
        [SerializeField] protected Transform gun;
        [Range(0.0f, 50.0f)] [SerializeField] protected float maxRotation;
        [Range(0.0f, 20.0f)] [SerializeField] protected float reloadingTime;

        [Header("Bullet")] 
        [SerializeField] protected Rigidbody2D bulletPrefab;
        [SerializeField] protected Transform bulletSpawnPoint;

        [Range(0.0f, 1000.0f)] [SerializeField]protected float baseBulletForce;

        private BulletPool _pool;
        private float _currentBulletForce;
        private float _lastShotTime;
        private bool _isReadyToShoot = true;

        protected void Awake()
        {
            _currentBulletForce = baseBulletForce;
        }

        protected void Start()
        {
            _pool = new BulletPool(new BulletFactory(bulletPrefab, transform), 5, bulletSpawnPoint);
        }

        public void Shoot()
        {
            if (_lastShotTime > 0.0f) _isReadyToShoot = _lastShotTime + reloadingTime <= Time.time;
            if (_isReadyToShoot)
            {
                _lastShotTime = Time.time;
                var newBullet = _pool.GetFreeBullet();
                if(newBullet==null) return;
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

    public class SimpleGun : Gun
    {
        
    }
}