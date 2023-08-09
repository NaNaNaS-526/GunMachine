using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun")] 
    [SerializeField] private Transform gun;
    [Range(0.0f, 50.0f)] [SerializeField] private float maxRotation;

    [Header("Bullet")] 
    public Rigidbody2D bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [Range(0.0f, 1000.0f)] [SerializeField] private float bulletForce;

    public void Shoot(Rigidbody2D bullet)
    {
        var newBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        newBullet.AddForce(bulletSpawnPoint.right * bulletForce, ForceMode2D.Impulse);
        Destroy(newBullet.gameObject, 4.0f);
    }

    public void RotateGun(float rotationCoefficient)
    {
        var rotationAmount = rotationCoefficient * maxRotation;
        gun.rotation = Quaternion.Euler(0.0f, 0.0f, rotationAmount);
    }
}