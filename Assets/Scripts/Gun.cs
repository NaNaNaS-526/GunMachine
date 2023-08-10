using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun")] 
    [SerializeField] private Transform gun;
    [Range(0.0f, 50.0f)] [SerializeField] private float maxRotation;
    [Range(0.0f, 20.0f)] [SerializeField] private float reloadingTime;

    [Header("Bullet")] public Rigidbody2D bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

    [Range(0.0f, 1000.0f)] [SerializeField] private float bulletForce;

    private float _lastShotTime;
    private bool _isReady = true;

    public void Shoot(Rigidbody2D bullet)
    {
        if (_isReady)
        {
            var newBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            newBullet.AddForce(bulletSpawnPoint.right * bulletForce, ForceMode2D.Impulse);
            
            EventBusUi.OnShot?.Invoke(reloadingTime);
            _lastShotTime = Time.time;
            Destroy(newBullet.gameObject, 4.0f);
        }

        _isReady = _lastShotTime + reloadingTime < Time.time;
    }

    public void Rotate(float rotationCoefficient)
    {
        var rotationAmount = rotationCoefficient * maxRotation;
        gun.rotation = Quaternion.Euler(0.0f, 0.0f, rotationAmount);
    }
}