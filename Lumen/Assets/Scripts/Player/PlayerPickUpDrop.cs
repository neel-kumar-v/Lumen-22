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
    private Camera playerCamera;

    public static bool updateNecessary = false;
    private Quaternion initialRotation;
    private void Start() {
        
        laser = GetComponent<PickUpLaser>();
        UpdateTurnSpeed();
        playerCamera = Camera.main;
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
                        updateNecessary = true;
                        // Debug.Log(objectGrabPointTransform.position);
                    }

                    laser.pickedUpObject = objectGrabPointTransform;
                }
            }
            else
            {
                objectGrabbable.Drop();
                updateNecessary = false;
                laser.pickedUpObject = null;
                objectGrabbable = null;
            }
        }
        if (objectGrabbable != null) {

            if (Input.GetKey(KeyCode.RightArrow)) {
                Vector3 rotation = playerCamera.transform.TransformDirection(Vector3.down) * turnSpeed * Time.deltaTime;
                objectGrabbable.gameObject.transform.Rotate(rotation, Space.World);
            }
            if (Input.GetKey(KeyCode.LeftArrow)) {
                Vector3 rotation = playerCamera.transform.TransformDirection(Vector3.up) * turnSpeed * Time.deltaTime;
                objectGrabbable.gameObject.transform.Rotate(rotation, Space.World);
            }
            if (Input.GetKey(KeyCode.UpArrow)) {
                Vector3 rotation = playerCamera.transform.TransformDirection(Vector3.right) * turnSpeed * Time.deltaTime;
                objectGrabbable.gameObject.transform.Rotate(rotation, Space.World);
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                Vector3 rotation = playerCamera.transform.TransformDirection(Vector3.left) * turnSpeed * Time.deltaTime;
                objectGrabbable.gameObject.transform.Rotate(rotation, Space.World);
            }


        }
        
    }
}