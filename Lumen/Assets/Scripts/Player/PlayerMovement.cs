using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour
{
    public CharacterController controller;
    [SerializeField] private KeyboardControls keyboard;
    [SerializeField] private ControllerControls controllerControls;

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





    void Awake()
    {
        keyboard = new KeyboardControls();
        controllerControls = new ControllerControls();
        // if (isSecondPlayer)
        // {
        //     controllerControls.Player.Jump.performed += ctx => Jump();
        // }
        // else
        // {
        //     keyboard.Player.Jump.performed += ctx => Jump();
        // }
    }
    
    private void OnEnable()
    {
        controllerControls.Enable();
        keyboard.Enable();
    }
    
    private void OnDisable()
    {
        controllerControls.Disable();
        keyboard.Disable();
    }

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


        float x;
        float z;
        
        if (isSecondPlayer)
        {
            var moveDirection = controllerControls.Player.Move.ReadValue<Vector2>();
            x = moveDirection.x;
            z = moveDirection.y;
        }
        else
        {
            var moveDirection = keyboard.Player.Move.ReadValue<Vector2>();
            x = moveDirection.x;
            z = moveDirection.y;
        }
        
        // Debug.Log($"{x}, {z}");

        Vector3 move = transform.right * x + transform.forward * z;
        float currentSpeed;
        if (isSecondPlayer)
        {
            currentSpeed = controllerControls.Player.Turbo.ReadValue<float>() == 1f ? Mathf.Lerp(speed, sprintSpeed, acceleration) : speed;
        }
        else
        {
            currentSpeed = keyboard.Player.Turbo.ReadValue<float>() == 1f ? Mathf.Lerp(speed, sprintSpeed, acceleration) : speed;
        }
        controller.Move(move * currentSpeed * Time.deltaTime);
     

        Jump();
        
        Debug.Log(audioManager.IsSoundPlaying("Footsteps"));
        // if (IsMoving()) audioManager.UnpauseSound("Footsteps");
        // else audioManager.PauseSound("Footsteps");

        if (!(transform.position.y <= -5f)) return;
        Debug.Log("Went Below");
        Reset();
    }

   
    

    private void Jump()
    {
        bool jumped;
        if (isSecondPlayer)
        {
            jumped = controllerControls.Player.Jump.ReadValue<float>() == 1f;
        }
        else
        {
            jumped = keyboard.Player.Jump.ReadValue<float>() == 1f;
        }
        
        if (isGrounded && jumped) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // private bool IsMoving()
    // {
    //     return isGrounded && (Mathf.Abs(controllerActions.ReadValue<Vector2>().x) > 0.3f || Mathf.Abs(controllerActions.ReadValue<Vector2>().y) > 0f);
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
