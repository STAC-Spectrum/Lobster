using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Agent
{
    [SerializeField] private InputReader _inputReader;

    private Vector3 _velocity;
    private float _verticalVelocity;

    public void SetPosition(Vector3 movement)
    {
        _velocity = movement * Time.fixedDeltaTime;

        if (_velocity.sqrMagnitude > 0)
        {
            // rotation
        }
    }
    
}
