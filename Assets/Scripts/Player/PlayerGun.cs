using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;
    private float nextFireTime;
    public float bulletSpeed = 10f;

    private Player playerController;

    private void Start()
    {
        playerController = GetComponentInParent<Player>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) //&& Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
            
        }
    }

    void Shoot()
    {
        if (bulletPrefab is not null || firePoint is not null)
        {
            
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            Vector2 bulletDirection = playerController.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            bullet.GetComponent<Bullet>().SetDirection(bulletDirection);

          
        }

  
    }
}
