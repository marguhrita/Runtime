using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
    PlayerInput _playerInput;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private bool shouldFaceMoveDirection;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector3 velocity;

    //  Input action data
    private InputAction _jumpAction;
    private InputAction _moveAction;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        _playerInput = GameManager.Singleton.playerInput;
        _jumpAction = _playerInput.actions["Jump"];
        _moveAction = _playerInput.actions["Move"];
    }

    public bool isMoving()
    {
        Vector2 moveVec = _moveAction.ReadValue<Vector2>();
        return moveVec.magnitude >= 0.2f;
    }

    public bool isJumping()
    {
        return _jumpAction.IsPressed() && velocity.y <= 4;
    }

    void Update()
    {

        // Movement Inputs
        moveInput = _moveAction.ReadValue<Vector2>();

        // Jumping
        if (_jumpAction.IsPressed() && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;
        controller.Move(moveDirection * speed * Time.deltaTime);

        if (shouldFaceMoveDirection && moveDirection.sqrMagnitude > 0.001f)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

}
