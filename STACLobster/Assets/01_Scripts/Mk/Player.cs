using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Agent
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float _speed = 5;

    private Rigidbody _rigidbody;
    
    private Vector3 _velocity;
    private float _verticalVelocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnDrawGizmos()
    {
        _inputReader.MovementEvent += Move;
    }

    public void SetPosition(Vector3 movement)
    {
        _velocity = movement * Time.fixedDeltaTime;

        if (_velocity.sqrMagnitude > 0)
        {
            // rotation
        }
    }

    public void Move(Vector2 vec)
    {
        _rigidbody.velocity = vec * _speed;
    }
    
}
