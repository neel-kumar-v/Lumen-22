using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask pickupLayerMask;
    [SerializeField] private Transform objectGrabPointTransform;
    

    private ObjectGrabbable objectGrabbable;
    private void Update() {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectGrabbable == null)
            {
                float pickupDistance = 7f;
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward,
                        out RaycastHit raycastHit, pickupDistance, pickupLayerMask))
                {
                    Debug.Log(raycastHit.transform);
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        objectGrabbable.Grab(objectGrabPointTransform);
                        Debug.Log(objectGrabbable);
                    }
                }
            }
            else
            {
                objectGrabbable.Drop();
                objectGrabbable = null;
            }
        }
    }
}