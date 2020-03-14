using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBehaviour : MonoBehaviour
{
    public Transform lookAtTarget = null;
    public Rigidbody rigid = null;

    public TargetGetterBehaviour targetGetter = null;


    private Quaternion originalRotation = Quaternion.Euler(Vector3.zero);
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        originalRotation = rigid.rotation;

        targetGetter = GetComponentInParent<TargetGetterBehaviour>();
    }

    void Update()
    {
        if(lookAtTarget != null)
        {
            ChangeRotation();
        }
    }

    void ChangeRotation()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = lookAtTarget.position;
        Vector3 directionOfTarget = targetPosition - currentPosition;

        rigid.MoveRotation(Quaternion.LookRotation(directionOfTarget));
    }

    public void AddTarget()
    {
        if(lookAtTarget == null)
        {
            lookAtTarget = targetGetter.targetTransform;
        }
    }

    public void RemoveTarget()
    {
        if(lookAtTarget != null)
        {
            lookAtTarget = null;
            rigid.MoveRotation(originalRotation);
        }
    }
}
