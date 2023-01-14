using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 12f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 3f;

    public float gravity = -9.81f;

    Vector3 velocity;
    bool isGrounded;

    public AudioManager audioManager;
    
    private Vector3 prevPosition;

    public bool turnOff = false;
    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("FREAK OUT!: No AudioManager Found In Scene");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!turnOff)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);


            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);


            if (!isGrounded && (x != 0f || z != 0f) && prevPosition != transform.position)
            {
                audioManager.PlaySound("Footsteps");
            }
            else if (prevPosition == transform.position)
            {
                audioManager.PlaySound("Footsteps");
            }

            prevPosition = transform.position;
        }

    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
