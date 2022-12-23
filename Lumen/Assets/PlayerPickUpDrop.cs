using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask pickupLayerMask;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E) ) {
            float pickupDistance = 2f;
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickupDistance, pickupLayerMask)) {
                Debug.Log(raycastHit.transform);
            }
        }
    }
}