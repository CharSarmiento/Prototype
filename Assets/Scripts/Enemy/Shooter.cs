using UnityEngine;

public class Shooter : Enemy
//Shooter class uses same bahavior as Enemy class but adds shooting functionality

{
    [Header("Objects")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Shooting Settings")]
    public float fireRate = 1f;
    public float bulletSpeed = 10f;

    //private float nextFireTime = 0f;

    public void Shoot()
        //Shooting is called in the character's animation
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            float facing = Mathf.Sign(transform.localScale.x);
            Vector2 direction = new Vector2(facing, 0) * bulletSpeed;
            bulletScript.SetDirection(direction);
        }
    }
}

