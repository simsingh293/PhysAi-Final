using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetGetterBehaviour : MonoBehaviour
{
    public UnityEvent onTargetFound = new UnityEvent();
    public UnityEvent onTargetLost = new UnityEvent();

    public Transform targetTransform = null;

    float gizmoRadius = 0;

    void Start()
    {
        gizmoRadius = GetComponent<SphereCollider>().radius;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Player Found");
            targetTransform = other.gameObject.transform;
            
            onTargetFound?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(targetTransform != null && other.gameObject.transform == targetTransform)
        {
            Debug.Log("Player out of Range");
            targetTransform = null;
            onTargetLost?.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, gizmoRadius);
    }
}
