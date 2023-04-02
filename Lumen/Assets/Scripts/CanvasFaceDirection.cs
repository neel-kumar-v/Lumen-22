using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CanvasFaceDirection : MonoBehaviour
{
    public Transform target;
    public float minDistance = 5f;
    public float maxDistance = 10f;
    private Quaternion oldRotation;

    public void Awake() {
        oldRotation = transform.rotation;
    }

    void FixedUpdate()
    {

        float dist = Vector3.Distance(transform.position, target.position);
        if (dist > minDistance && dist < maxDistance)
        {
            Vector3 relativePos = transform.position - target.position;

            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;
        }
    }

    public void RespawnUpdate() {
        transform.rotation = oldRotation;
    }
}
