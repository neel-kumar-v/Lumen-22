using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;
    private BoxCollider boxCollider;
    private Vector3 scaleChange;
    private Material mat;
    [Range(1, 20)]
    public float scale; // changed it from something that subtracts to something that divides and multiplies

    public float moveSpeed = 10f;
    
    private Vector3 normalScale;
    private Vector3 shrinkScale;
    private Vector3 normalColliderScale;
    private Vector3 shrinkColliderScale;

    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
        mat = GetComponent<MeshRenderer>().material;
        
        // Precalculating the shrink size 
        normalScale = gameObject.transform.localScale;
        scaleChange = new Vector3(scale, scale, scale);
        shrinkScale = new Vector3(
            normalScale.x / scaleChange.x,
            normalScale.y / scaleChange.y,
            normalScale.z / scaleChange.z
        );
        
        // Precalculating the expanded box collider size
        // We need to expand the box collider so that we cant put the box/mirror anywhere
        // it wouldn't fit in its normal size
        boxCollider = GetComponent<BoxCollider>();
        normalColliderScale = boxCollider.size;
        shrinkColliderScale = new Vector3(
            normalColliderScale.x * scaleChange.x,
            normalColliderScale.y * scaleChange.y,
            normalColliderScale.z * scaleChange.z
        );

    }

    public void Start() {
        // this says that there wont be any collisions between player objects and PickedUpObj objects
        Physics.IgnoreLayerCollision(3, 8); 
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidbody.useGravity = false;

        gameObject.transform.localScale = shrinkScale;
        // boxCollider.size = shrinkColliderScale;

        // this is changing the layer to PickedUpObj so that it won't collide with the player
        gameObject.layer = 8;

    }
    
    

    public void Drop()
    {
        objectGrabPointTransform = null;
        objectRigidbody.useGravity = true;

        gameObject.transform.localScale = normalScale;
        // boxCollider.size = normalColliderScale;
        
        gameObject.layer = 7; // sets it back to PickUpObj so that there are collisions again.
    }

    private void FixedUpdate() {
        if (!objectGrabPointTransform) return;
        Vector3 newPosition = Vector3.Lerp(transform.position,objectGrabPointTransform.position, Time.deltaTime * moveSpeed);
        objectRigidbody.MovePosition(newPosition);
    }
}
