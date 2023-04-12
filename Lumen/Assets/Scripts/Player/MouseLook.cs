using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSens = 100f;

    public Transform playerBody;

    public Transform centerDot;

    float xRotation = 0f;

    [Range(-50f, 0f)]
    public float topView;
    [Range(0f, 50f)]
    public float bottomView;

    public static bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        UpdateMouse();
    }

    public void UpdateMouse() {
        mouseSens = PlayerPrefs.GetFloat("sensitivity");
    }

    // Update is called once per frame
    void Update() {
        if(paused) return;
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, topView, bottomView);


        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
