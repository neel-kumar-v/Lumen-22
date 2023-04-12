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


    private void Update() {
        CheckForKey();
        RotateMirror();
    }

    private void RotateMirror() {
        if (objectGrabbable == null) return;
        if (Input.GetKey(KeyCode.RightArrow)) {
            Rotate(Vector3.down);
        }

        if (Input.GetKey(KeyCode.LeftArrow)) {
            Rotate(Vector3.up);
        }

        if (Input.GetKey(KeyCode.UpArrow)) {
            Rotate(Vector3.back);
        }

        if (Input.GetKey(KeyCode.DownArrow)) {
            Rotate(Vector3.forward);
        }

        bool shiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (!shiftPressed) return;
            
        if (Input.GetKey(KeyCode.UpArrow)) {
            Rotate(Vector3.right);
        }

        if (Input.GetKey(KeyCode.DownArrow)) {
            Rotate(Vector3.left);
        }
            


        // if (Input.GetKey (KeyCode.RightArrow)) {
        //     objectGrabbable.gameObject.transform.Rotate(0, turnSpeed*Time.deltaTime, 0, Space.Self);
        // }
        // if (Input.GetKey (KeyCode.LeftArrow)) {
        //     objectGrabbable.gameObject.transform.Rotate(0, -turnSpeed*Time.deltaTime, 0, Space.Self);
        // }
        // if (Input.GetKey (KeyCode.UpArrow)) {
        //     objectGrabbable.gameObject.transform.Rotate(turnSpeed*Time.deltaTime, 0, 0, Space.Self);
        // }
        // if (Input.GetKey (KeyCode.DownArrow)) {
        //     objectGrabbable.gameObject.transform.Rotate(-turnSpeed*Time.deltaTime, 0, 0, Space.Self);
        // }
    }

    private void Rotate(Vector3 vector) {
        Vector3 rotation = playerCamera.transform.TransformDirection(vector) * turnSpeed * Time.deltaTime;
        objectGrabbable.gameObject.transform.Rotate(rotation, Space.World);
    }

    private void CheckForKey() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (objectGrabbable == null) {
                float pickupDistance = 6f;
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward,
                        out RaycastHit raycastHit, pickupDistance, pickupLayerMask)) {
                    Debug.Log(raycastHit.transform.gameObject);
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable)) {
                        objectGrabbable.Grab(objectGrabPointTransform);
                        updateNecessary = true;
                        // Debug.Log(objectGrabPointTransform.position);
                    }

                    laser.pickedUpObject = objectGrabPointTransform;
                }
            }
            else {
                objectGrabbable.Drop();
                updateNecessary = false;
                laser.pickedUpObject = null;
                objectGrabbable = null;
            }
        }
    }
}