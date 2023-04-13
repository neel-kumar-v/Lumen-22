using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

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

    // ControllerControls controls;


    // private void Awake()
    // {
    //     controls = new ControllerControls();
    //     controls.Player.Jump.performed += ctx => ControllerLook();
    // }

    // private void ControllerLook()
    // {
    //     float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
    //     float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
    //
    //     xRotation -= mouseY;
    //     xRotation = Mathf.Clamp(xRotation, topView, bottomView);
    //
    //
    //     transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    //     playerBody.Rotate(Vector3.up * mouseX);
    // }

    // void OnEnable()
    // {
    //     controls.Player.Enable();
    // }
    //
    // void OnDisable()
    // {
    //     controls.Player.Disable();
    // }


    private void Start()
    {
        startPos = transform.position;
        audioManager = AudioManager.instance;
        if (audioManager != null) return;
        Debug.LogError("FREAK OUT!: No AudioManager Found In Scene");
        audioManager.PlaySound("Footsteps");
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

        // if (isSecondPlayer) {
        //     MoveSecond();
        // }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Debug.Log($"{x}, {z}");

        Vector3 move = transform.right * x + transform.forward * z;
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? Mathf.Lerp(speed, sprintSpeed, acceleration) : speed;
        controller.Move(move * currentSpeed * Time.deltaTime);
        

        Jump();
        Debug.Log(audioManager.IsSoundPlaying("Footsteps"));
        if (IsMoving()) audioManager.UnpauseSound("Footsteps");
        else audioManager.PauseSound("Footsteps");

        if (!(transform.position.y <= -5f)) return;
        Debug.Log("Went Below");
        Reset();
    }

   
    

    private void Jump() {
        
        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private bool IsMoving()
    {
        return isGrounded && (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.3f || Mathf.Abs(Input.GetAxis("Vertical")) > 0f);
    }

    private void Reset()
    {
        onRespawn.Invoke();
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
