using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] Vector3 offset;

    void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector3 newPos = target.position + offset;
        newPos.y = offset.y;

        transform.position = newPos;
    }
}
