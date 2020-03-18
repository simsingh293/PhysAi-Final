using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public Transform transformToFollow = null;

    private void Start()
    {
        if(transformToFollow == null)
        {
            Destroy(this);
        }
    }

    void Update()
    {
        if(transformToFollow != null)
        {
            transform.position = transformToFollow.position;
        }
    }
}
