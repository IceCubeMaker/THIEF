using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public bool isFollow;
    [Tooltip("Percentage from 0-1 per second to travel")]
    public float lerpSpeed;

    void LateUpdate()
    {
        if(target && isFollow) { FollowTarget(); }
    }
    private void FollowTarget()
    {
        Vector3 toMove = Vector3.Lerp(transform.position, target.position,lerpSpeed * Time.deltaTime);
        toMove.z = transform.position.z;
        transform.position = toMove;
    }
}
