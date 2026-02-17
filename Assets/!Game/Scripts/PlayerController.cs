using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    [Header("Rotation")]
    public float rotationSpeed = 10f;

    public InputActionReference moveAction;
    
    private CharacterController controller;
    private Vector2 moveInput;
    private Vector3 velocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    
    private void OnEnable()
    {
        moveAction.action.Enable();
        moveAction.action.performed += OnMove;
        moveAction.action.canceled += OnMove;
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        moveAction.action.performed -= OnMove;
        moveAction.action.canceled -= OnMove;
    }

    // Called automatically by PlayerInput component
    private void OnMove(InputAction.CallbackContext value)
    {
        moveInput = value.ReadValue<Vector2>();
    }

    private void Update()
    {
        HandleMovement();
        ApplyGravity();
        controller.Move(velocity);
    }

    private void HandleMovement()
    {
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);

        if (move.magnitude > 1f)
            move.Normalize();

        if (move != Vector3.zero)
        {
            // Calculate target rotation
            Quaternion targetRotation = Quaternion.LookRotation(move);
        
            // Smoothly rotate toward movement direction
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        move *= moveSpeed * Time.deltaTime;
        
        velocity = new Vector3(move.x, velocity.y, move.z);
    }


    private void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small downward force to keep grounded
        }

        velocity.y += gravity * Time.deltaTime;
    }
}