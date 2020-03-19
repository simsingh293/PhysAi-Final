using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    HealthComponent Health;
    HealthEffector HealthEffector;

    [SerializeField] private int MaxHealth = 100;
    [SerializeField] private float CurrentHealth = 0;

    void Awake()
    {
        float fmax = (float)MaxHealth;
        Health = new HealthComponent(fmax);
        CurrentHealth = Health.CurrentHealth;

        HealthEffector = new HealthEffector(Health);
    }

    private void Update()
    {
        CurrentHealth = Health.CurrentHealth;
    }

    public void Damage(float DamageValue)
    {
        HealthEffector.Affect(false, DamageValue);
    }

    public void Heal(float HealValue)
    {
        HealthEffector.Affect(true, HealValue);
    }

    public bool IsDead()
    {
        return CurrentHealth <= 0;
    }
}
