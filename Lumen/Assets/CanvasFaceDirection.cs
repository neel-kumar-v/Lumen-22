using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFaceDirection : MonoBehaviour
{
    public Transform target;
    public float distance = 5f;

    void FixedUpdate()
    {

        float dist = Vector3.Distance(transform.position, target.position);
        if (dist > distance)
        {
            Vector3 relativePos = transform.position - target.position;

            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;
        }
    }
}