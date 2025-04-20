using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private new Transform camera;
    
    [SerializeField] private float minimumSpeed = 15f;

    [SerializeField] private float turnSmoothTime = 0.25f;
    float turnSmoothVelocity;

    private PlayerAnimationController _animationController;

    public void Initialize(PlayerAnimationController animationController)
    {
        characterController = GetComponent<CharacterController>();
        _animationController = animationController;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        float inputMagnitude = Mathf.Clamp01(direction.magnitude);

        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && _animationController.isGrounded())
        {
            inputMagnitude *= 2;
        }

        float speed = inputMagnitude * minimumSpeed;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection.normalized * speed * Time.deltaTime);

        }
        _animationController.SetMagnitude(inputMagnitude, 0.1f, Time.deltaTime);
    }
}
