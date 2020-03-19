using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent
{
    private float currentHP;
    private float maxHP;

    public float CurrentHealth { get { return currentHP; } }


    public float MaxHealth { get{ return maxHP; } }

    public HealthComponent(float MaxHealth)
    {
        maxHP = MaxHealth;
        currentHP = maxHP;
    }



    public void AddHealth(float ValueToAdd)
    {
        float total = currentHP + ValueToAdd;

        if(total > maxHP)
        {
            currentHP = maxHP;
        }

        else
        {
            currentHP = total;
        }
    }

    public void SubtractHealth(float ValueToSubtract)
    {
        float total = currentHP - ValueToSubtract;

        if(total < 0)
        {
            currentHP = 0;
        }

        else
        {
            currentHP = total;
        }
    }
}
