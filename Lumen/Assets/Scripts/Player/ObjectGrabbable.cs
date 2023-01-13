using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;
    private BoxCollider boxCollider;
    private Vector3 scaleChange;
    private Vector3 colliderScaleChange;
    private Material mat;
    [Range(1, 20)]
    public float scale; // changed it from something that subtracts to something that divides and multiplies
    public float colliderScale;
    
    public float moveSpeed = 8f;
    
    private Vector3 normalScale;
    private Vector3 shrinkScale;
    private Vector3 normalColliderScale;
    private Vector3 shrinkColliderScale;
    public Vector3 newPosition;
    public Vector3 oldPosition;
    public bool allowMirrorCollisions;
    
    [SerializeField] private Transform playerCameraTransform;

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
        colliderScaleChange = new Vector3(colliderScale, colliderScale, colliderScale);
        shrinkColliderScale = new Vector3(
            normalColliderScale.x * colliderScaleChange.x,
            normalColliderScale.y * colliderScaleChange.y,
            normalColliderScale.z * colliderScaleChange.z
        );

    }

    public void Start() {
        // this says that there wont be any collisions between player objects and PickedUpObj objects
        if (allowMirrorCollisions!)
        {
            Physics.IgnoreLayerCollision(3, 8);
        }
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        gameObject.transform.localScale = shrinkScale;
        boxCollider.size = shrinkColliderScale;
        
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidbody.useGravity = false;
        
        


    }
    
    

    public void Drop()
    {
        objectRigidbody.velocity = Vector3.zero;
        objectGrabPointTransform = null;

        if (!this.CompareTag("Mirror"))
        {
            objectRigidbody.useGravity = true;
        }

        gameObject.transform.localScale = normalScale;
        boxCollider.size = normalColliderScale;
    }

    private void Update() {
        if (!objectGrabPointTransform) return;
        newPosition = Vector3.SlerpUnclamped(transform.position,objectGrabPointTransform.position, (Time.deltaTime * moveSpeed)/0.5f);
        objectRigidbody.MovePosition(newPosition);
    }
}
