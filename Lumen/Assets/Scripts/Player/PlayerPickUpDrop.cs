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
    public float turnSpeed = 10f;

    private void Start() {
        laser = GetComponent<PickUpLaser>();
        UpdateTurnSpeed();
    }

    public void UpdateTurnSpeed() {
        turnSpeed = PlayerPrefs.GetFloat("turn speed");
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectGrabbable == null)
                {
                float pickupDistance = 6f;
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward,
                        out RaycastHit raycastHit, pickupDistance, pickupLayerMask))
                {
                    Debug.Log(raycastHit.transform.gameObject);
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        objectGrabbable.Grab(objectGrabPointTransform);
                        // Debug.Log(objectGrabPointTransform.position);
                    }

                    laser.pickedUpObject = objectGrabPointTransform;
                }
            }
            else
            {
                objectGrabbable.Drop();
                laser.pickedUpObject = null;
                objectGrabbable = null;
            }
        }
        if (objectGrabbable != null) {
            if (Input.GetKey (KeyCode.RightArrow)) {
                objectGrabbable.gameObject.transform.Rotate(0, turnSpeed*Time.deltaTime, 0, Space.Self);
            }
            if (Input.GetKey (KeyCode.LeftArrow)) {
                objectGrabbable.gameObject.transform.Rotate(0, -turnSpeed*Time.deltaTime, 0, Space.Self);
            }
            if (Input.GetKey (KeyCode.UpArrow)) {
                objectGrabbable.gameObject.transform.Rotate(-turnSpeed*Time.deltaTime, 0, 0, Space.Self);
            }
            if (Input.GetKey (KeyCode.DownArrow)) {
                objectGrabbable.gameObject.transform.Rotate(-turnSpeed*Time.deltaTime, 0, 0, Space.Self);
            }
        }
    }
}