using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEffector 
{
    HealthComponent HP;

    public HealthEffector(HealthComponent HealthToAffect)
    {
        HP = HealthToAffect;
    }

    public void Affect(bool AddHealth, float Value)
    {
        if (AddHealth)
        {
            HP.AddHealth(Value);
        }
        else
        {
            HP.SubtractHealth(Value);
        }
    }
}
