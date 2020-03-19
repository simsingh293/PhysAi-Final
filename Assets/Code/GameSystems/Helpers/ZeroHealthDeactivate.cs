using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroHealthDeactivate : MonoBehaviour
{
    HealthSystem healthSystem;



    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
    }

    void Update()
    {
        if (healthSystem.IsDead())
        {
            gameObject.SetActive(false);
        }
    }
}
