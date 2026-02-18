using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    
    private static readonly int Speed = Animator.StringToHash("Speed");

    [SerializeField] private Animator animator;
    
    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        UpdateAnimator(characterController.velocity);
    }

    private void UpdateAnimator(Vector3 velocity)
    {
        Vector3 horizontalVelocity = new Vector3(velocity.x, 0f, velocity.z);
        float speed = horizontalVelocity.magnitude;
        animator.SetFloat(Speed, speed);
    }
}
