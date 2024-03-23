using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Player _player;
    private Rigidbody _playerRigidbody;
    private Collider _collider;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _playerRigidbody = _player.GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (_playerRigidbody.velocity.y >= 0)
            _collider.isTrigger = true;
        else
            _collider.isTrigger = false;
    }
}
