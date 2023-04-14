using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    [SerializeField] private KeyboardControls keyboard;
    [SerializeField] private ControllerControls controllerControls;
    [SerializeField] private PlayerControls playerControls;
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction move;
    private InputAction turbo;

    public StartPositions pos;

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
    private float currentSpeed = 3f;





    void Awake()
    {
        // keyboard = new KeyboardControls();
        // controllerControls = new ControllerControls();
        //
        // playerControls = new PlayerControls();
        
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");

    }
    

    private void OnEnable()
    {
        // controllerControls.Enable();
        // keyboard.Enable();
        player.FindAction("Jump").started += ctx => Jump();
        player.FindAction("Turbo").started += ctx => Jump();
        move = player.FindAction("Move");
        turbo = player.FindAction("Turbo");
        player.Enable();
        
        audioManager = AudioManager.instance;
        if (audioManager != null) return;
        Debug.LogError("FREAK OUT!: No AudioManager Found In Scene");
        audioManager.PlaySound("Footsteps");
    }
    
    private void OnDisable()
    {
        // controllerControls.Disable();
        // keyboard.Disable();
        player.FindAction("Jump").started -= ctx => Jump();
        player.FindAction("Turbo").started -= ctx => Jump();
        player.Disable();
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


        // if (isSecondPlayer)
        // {
        //     var moveDirection = controllerControls.Player.Move.ReadValue<Vector2>();
        //     x = -moveDirection.y;
        //     z = moveDirection.x;
        // }
        // else
        // {
        //     var moveDirection = keyboard.Player.Move.ReadValue<Vector2>();
        //     x = -moveDirection.y;
        //     z = moveDirection.x;
        // }
        
        
        var moveDirection = move.ReadValue<Vector2>();
        float x = -moveDirection.y;
        float z = moveDirection.x;

        // Debug.Log($"{x}, {z}");
        Vector3 moveVector = transform.right * x + transform.forward * z;
        
        float targetSpeed = turbo.ReadValue<float>() == 1f ? sprintSpeed : speed;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
        controller.Move(moveVector * currentSpeed * Time.deltaTime);
        
        
        
        // Debug.Log(audioManager.IsSoundPlaying("Footsteps"));
        // if (IsMoving()) audioManager.UnpauseSound("Footsteps");
        // else audioManager.PauseSound("Footsteps");

        if (!(transform.position.y <= -5f)) return;
        Reset();
    }
    
    

    private void Jump()
    {
        // bool jumped;
        // if (isSecondPlayer)
        // {
        //     jumped = controllerControls.Player.Jump.ReadValue<float>() == 1f;
        // }
        // else
        // {
        //     jumped = keyboard.Player.Jump.ReadValue<float>() == 1f;
        // }
        
        if (isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

       
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
