using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : NetworkBehaviour
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
    
    [SerializeField] private float acceleration = 0.5f;

    public bool isSecondPlayer = false;

    public static bool paused = false;
    
   

    private void Start()
    {
        startPos = transform.position;
        audioManager = AudioManager.instance;
        if (audioManager != null) return;
        Debug.LogError("FREAK OUT!: No AudioManager Found In Scene");

    }

    private void Update() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = gravity / 5;
        }
        controller.Move(velocity * Time.deltaTime);

        if (paused) return;
        // if(!IsOwner) return;

        if (isSecondPlayer) {
            MoveSecond();
        }
        else {
            Move();
        }

        Jump();

        // if (IsMoving()) audioManager.PlaySound("Footsteps");
        
        if (isGrounded && velocity.x == 0)
        {
            audioManager.PlaySound("Footsteps");
        }
        else
        {
            return;
        }

        if (!(transform.position.y <= -5f)) return;
        Debug.Log("Went Below");
        Reset();
    }

    private void Move() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? Mathf.Lerp(speed, sprintSpeed, acceleration) : speed;
        controller.Move(move * currentSpeed * Time.deltaTime);
    }
    private void MoveSecond() {
        // // float x = Input.GetAxis("Horizontal2");
        // // float z = Input.GetAxis("Vertical2");
        //
        // Vector3 move = transform.right * x + transform.forward * z;
        // float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? Mathf.Lerp(speed, sprintSpeed, acceleration) : speed;
        // controller.Move(move * currentSpeed * Time.deltaTime);
    }

    private void Jump() {
        
        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // private void CheckMovement()
    // {
    //     
    //     
    //     //return !isGrounded && (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f);
    // }

    private void Reset()
    {
        onRespawn.Invoke();
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
