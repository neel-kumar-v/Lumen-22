using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;
    public GameObject obj;
    private Vector3 scaleChange;
    public float scale;

    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
        scaleChange = new Vector3(-scale, -scale, -scale);
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidbody.useGravity = false;
        obj.transform.localScale += scaleChange;
    }

    public void Drop()
    {
        this.objectGrabPointTransform = null;
        objectRigidbody.useGravity = true;
        obj.transform.localScale -= scaleChange;
    }

    private void FixedUpdate()
    {
        if (this.objectGrabPointTransform != null)
        {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(transform.position,objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            objectRigidbody.MovePosition(newPosition);
        }
    }
}
