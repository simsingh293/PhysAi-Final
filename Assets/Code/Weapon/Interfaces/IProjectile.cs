using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    // Interface for all projectiles to derive from

    float ProjectileSpeed { set; }
    Vector3 ShootingDirection { set;  }

    // function to initialize any necesary values
    void Init();

    // function for projectile TRAVEL
    void Travel();

    // function for when projectile IMPACTS with something
    void Impact();

    IEnumerator DisableBullet(float time);
}
