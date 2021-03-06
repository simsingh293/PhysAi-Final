﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour, IProjectile
{
    public float ProjectileSpeed 
    {
        set
        {
            bulletSpeed = value;
        }
    }

    public Vector3 ShootingDirection
    {
        get
        {
            return shotDirection;
        }
        set
        {
            shotDirection = value;
        }
    }

    public bool hitSomething = false;
    public float DisableTime = 3;
    public bool ApplyGravity = false;
    public int PredictionStepsPerFrame = 6;


    private float bulletSpeed = 0;
    private Vector3 bulletVelocity = Vector3.zero;
    private Vector3 shotDirection = Vector3.zero;


    private Vector3 currentPosition = Vector3.zero;
    private Vector3 travelDirection = Vector3.zero;
    private Vector3 amountToOffset = Vector3.zero;
    private Vector3 targetPosition = Vector3.zero;
    private Vector3 newDirection = Vector3.zero;

    private Coroutine currentCo = null;


    void Update()
    {
        if (!hitSomething)
        {
            Travel();
        }

        if (hitSomething)
        {
            Impact();
        }
    }


    // Initialize projectile by setting forward direction and velocity, 
    // as well as start a coroutine to disable the bullet after some time
    /// <summary>
    /// Initialize projectile by setting forward direction and velocity, 
    // as well as start a coroutine to disable the bullet after some time
    /// </summary>
    public void Init()
    {
        SetVelocity();
        SetForward();
        currentCo = StartCoroutine(DisableBullet(DisableTime));
    }
    public void Travel()
    {
        currentPosition = transform.position;
        travelDirection = transform.forward;

        float stepSize = 1.0f / PredictionStepsPerFrame;

        for(float step = 0; step < 1; step += stepSize)
        {

            bulletVelocity += AdditionalVelocity() * stepSize;
            

            amountToOffset = bulletVelocity * stepSize * Time.deltaTime;

            targetPosition = currentPosition + amountToOffset;

            newDirection = targetPosition - currentPosition;


            Ray ray = new Ray(currentPosition, newDirection.normalized);

            if(Physics.Raycast(ray, newDirection.magnitude))
            {
                Debug.Log("Hit Something");
                hitSomething = true;
                break;
            }


            currentPosition = targetPosition;
            travelDirection = newDirection;
        }

        transform.position = currentPosition;
        transform.forward = travelDirection;
    }

    public void Impact()
    {
        Debug.Log("Impact Logic");

        DealDamage();
        DeactivateBullet();
        
        //Instantiate(BulletPooler.Instance.SpawnToPosRot(BulletPooler.ProjectileType.Rocket, transform.position, Quaternion.Euler(-travelDirection))); 
    }

    public virtual Vector3 AdditionalVelocity()
    {
        Vector3 velocityToAdd = Vector3.zero;

        //velocityToAdd = Physics.gravity * Time.deltaTime;

        return velocityToAdd;
    }

    void SetVelocity()
    {
        bulletVelocity = bulletSpeed * shotDirection;
    }

    void SetForward()
    {
        transform.forward = shotDirection;
    }

    void ResetVariables()
    {
        hitSomething = false;

        currentPosition = Vector3.zero;
        travelDirection = Vector3.zero;
        amountToOffset = Vector3.zero;
        targetPosition = Vector3.zero;
        newDirection = Vector3.zero;

        if(currentCo != null)
        {
            StopCoroutine(currentCo);
            currentCo = null;
        }
    }

    void DeactivateBullet()
    {
        ResetVariables();
        gameObject.SetActive(false);
    }


    void DealDamage()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, travelDirection);

        if(Physics.Raycast(ray, out hit, 0.1f))
        {
            if(hit.collider.gameObject.TryGetComponent(out HealthSystem HPSys))
            {
                HPSys.Damage(5);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 point1 = transform.position;
        float stepSize = 0.01f;
        Vector3 predictedBulletVelocity = bulletVelocity;
        for (float step = 0; step < 1; step += stepSize)
        {
            if (ApplyGravity)
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

    public IEnumerator DisableBullet(float time)
    {
        yield return new WaitForSeconds(time);

        DeactivateBullet();
    }
}
