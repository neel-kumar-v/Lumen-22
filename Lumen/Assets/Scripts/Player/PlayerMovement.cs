using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float sprintSpeed = 20f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 3f;

    public float gravity = -9.81f;

    private Vector3 velocity;
    private bool isGrounded;
    public UnityEvent onRespawn;

    private AudioManager audioManager;

    private Vector3 startPos;

    private void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("FREAK OUT!: No AudioManager Found In Scene");
        }
        
        startPos = transform.position;
    }

    private void Update()
    {
        

        Move();

        Jump();

        if (IsMoving())
        {
            audioManager.PlaySound("Footsteps");
        }

        if (transform.position.y <= -5f)
        {
            Debug.Log("Went Below");
            Reset();
        }
    }

    private void Move() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : speed;
        controller.Move(move * currentSpeed * Time.deltaTime);
    }

    private void Jump() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = gravity / 5;
        }
        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private bool IsMoving()
    {
        return !isGrounded && (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f);
    }

    private void Reset()
    {
        transform.eulerAngles = new Vector3(0f, 90f, 0f);
        transform.position = startPos;
        velocity = Vector3.zero;
        onRespawn.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
