using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask pickupLayerMask;
    [SerializeField] private Transform objectGrabPointTransform;
    

    private ObjectGrabbable objectGrabbable;
    private MeshRenderer rend;
    public Material mat;
    [SerializeField] private PickUpLaser laser;

    private void Start() {
        laser = GetComponent<PickUpLaser>();
    }
    private void Update() {
        

        if (Input.GetKeyDown(KeyCode.E))
        {
            float pickupDistance = 7f;
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickupDistance, pickupLayerMask)) {
                if (raycastHit.transform.TryGetComponent(out objectGrabbable)) {
                    objectGrabbable.Grab(objectGrabPointTransform);
                    Debug.Log(objectGrabPointTransform.position);
                }
                laser.pickedUpObject = objectGrabPointTransform;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && objectGrabbable != null) {
            objectGrabbable.Drop();
            laser.pickedUpObject = null;
            objectGrabbable = null;
        }
    }
    
}