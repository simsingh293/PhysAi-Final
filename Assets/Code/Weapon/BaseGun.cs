using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGun : MonoBehaviour, IWeapon
{
    public float FireRate { get; private set; }
    public float BulletSpeed { get; private set; }

    public BulletPooler Magazine { get; private set; }

    public Transform FirePoint { get; private set; }


    public float fireRate = 1;
    public float bulletSpeed = 5;
    BulletPooler bulletPooler = null;
    public Transform firePoint = null;



    public bool ApplyGravityToBulletPrediction = false;
    private GameObject bulletObj = null;

    // Initialization
    void Start()
    {
        FireRate = fireRate;
        BulletSpeed = bulletSpeed;

        bulletPooler = BulletPooler.Instance;
        Magazine = bulletPooler;

        if(firePoint != null)
        {
            FirePoint = firePoint;
            Debug.Log("Connected to Transform of Muzzle");
        }
        else
        {
            Debug.Log("Need to assign Transform of Muzzle");
        }
    }

    void Update()
    {
        ApplyGravityToBulletPrediction = Input.GetMouseButton(1) ? true : false;


        if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1))
        {
            bulletObj = GetBullet();
            Shoot(bulletObj);
        }
        else if(Input.GetMouseButtonDown(0) && Input.GetMouseButton(1))
        {
            bulletObj = GetGravityBullet();
            Shoot(bulletObj);
        }

    }

    // TODO Implement reloading functionality
    public void Reload()
    {
        throw new System.NotImplementedException();
    }


    // Shooting Implementation
    public void Shoot(GameObject bullet)
    {
        //bulletObj = Magazine.SpawnToPos(BulletPooler.ProjectileType.Basic, FirePoint.position);

        if(bullet.TryGetComponent<IProjectile>(out var projectile))
        {
            projectile.ShootingDirection = FirePoint.forward;
            projectile.ProjectileSpeed = bulletSpeed;

            projectile.Init();
        }

        bulletObj = null;
    }


    // Shot prediction on weapon
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 point1 = firePoint.position;
        float stepSize = 0.01f;
        Vector3 predictedBulletVelocity = bulletSpeed * firePoint.forward;
        for (float step = 0; step < 1; step += stepSize)
        {
            if (ApplyGravityToBulletPrediction)
            {
                predictedBulletVelocity += Physics.gravity * stepSize;
            }
            else
            {
                predictedBulletVelocity += Vector3.zero * stepSize;
            }
            Vector3 point2 = point1 + predictedBulletVelocity * stepSize;
            Gizmos.DrawLine(point1, point2);
            point1 = point2;
        }
    }

    public GameObject GetBullet()
    {
        return Magazine.SpawnToPos(BulletPooler.ProjectileType.Basic, FirePoint.position);
    }

    private GameObject GetGravityBullet()
    {
        return Magazine.SpawnToPos(BulletPooler.ProjectileType.Gravity, FirePoint.position);
    }
}
