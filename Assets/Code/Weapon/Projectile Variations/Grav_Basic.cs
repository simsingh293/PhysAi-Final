using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grav_Basic : BaseBullet
{
    public override Vector3 AdditionalVelocity()
    {
        Vector3 velocityToAdd = Vector3.zero;

        velocityToAdd = Physics.gravity * Time.deltaTime;

        return velocityToAdd;
    }
}
