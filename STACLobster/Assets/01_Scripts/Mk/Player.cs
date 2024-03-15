using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Agent
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float _moveSpeed = 5, _jumpPower = 7;

    private Rigidbody _rigidbody;
    private PlayerInput _playerInput;
    
    private Vector3 _velocity;
    private float _verticalVelocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        PlayerInput playerInput = new PlayerInput();
        playerInput.Player.Enable();
        _inputReader.JumpEvent += JumpHandle;
    }

    public void SetPosition(Vector3 movement)
    {
        _velocity = movement * Time.fixedDeltaTime;

        if (_velocity.sqrMagnitude > 0)
        {
            // rotation
        }
    }

    private void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            print("눌리고 있음");
        }
        
    }

    private void FixedUpdate()
    {
        //_playerInput.Player.Movement.ReadValue<Vector2>();
        Vector2 inputVector = _inputReader._playerActions.Player.Movement.ReadValue<Vector2>();
        _rigidbody.velocity = new Vector2(inputVector.x * _moveSpeed, _rigidbody.velocity.y);
    }

    public void JumpHandle(bool check)
    {
        _rigidbody.AddForce(Vector2.up * _jumpPower);
    }

}
