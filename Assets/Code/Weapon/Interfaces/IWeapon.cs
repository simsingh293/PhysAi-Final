using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    // Interface for all weapons to derive from

    float FireRate { get; }
    float BulletSpeed { get; }
    BulletPooler Magazine { get; }
    Transform FirePoint { get; }

    GameObject GetBullet();
    // function to SHOOT weapon
    void Shoot(GameObject bullet);

    // function to RELOAD weapon
    void Reload();


}
