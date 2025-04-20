using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerAnimationController _animationController;

    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance = 0.65f;


    Vector3 velocity;

    public void Initialize(PlayerAnimationController animationController)
    {
        characterController = GetComponent<CharacterController>();
        _animationController = animationController;
    }

    private void Update()
    {
        Gravity();
        Jump();
    }

    private void Gravity()
    {
        _animationController.SetGrounded(Physics.CheckSphere(groundCheck.position, groundDistance, groundMask));

        if (_animationController.isGrounded() && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetButton("Jump") && _animationController.isGrounded())
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            _animationController.PlayJump(true);

        }

        else
        {
            _animationController.PlayJump(false);
        }
    }
}
