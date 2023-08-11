using UnityEngine;


public class Gun : MonoBehaviour
{
    [Header("Gun")] [SerializeField] 
    private Transform gun;
    [Range(0.0f, 50.0f)] [SerializeField] private float maxRotation;
    [Range(0.0f, 20.0f)] [SerializeField] private float reloadingTime;

    [Header("Bullet")] 
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

    [Range(0.0f, 1000.0f)] [SerializeField] private float bulletForce;

    private BulletPool _pool;
    private float _lastShotTime;
    private bool _isReady = true;

    private void Start()
    {
        _pool = new BulletPool(bulletPrefab, 3, bulletSpawnPoint)
        {
            AutoExpand = true
        };
    }

    public void Shoot()
    {
        if (_lastShotTime > 0.0f) _isReady = _lastShotTime + reloadingTime <= Time.time;
        if (_isReady)
        {
            _lastShotTime = Time.time;
            var newBullet = _pool.GetFreeBullet();
            newBullet.AddForce(bulletSpawnPoint.right * bulletForce, ForceMode2D.Impulse);
            EventBusUi.OnShot?.Invoke(reloadingTime);
        }
    }

    public void Rotate(float rotationCoefficient)
    {
        var rotationAmount = rotationCoefficient * maxRotation;
        gun.rotation = Quaternion.Euler(0.0f, 0.0f, rotationAmount);
    }
}