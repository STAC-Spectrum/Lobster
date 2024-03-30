using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Player _player;
    private Rigidbody _playerRigidbody;
    private Collider _collider;

    [SerializeField] private LayerMask[] _layer;
    
    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _playerRigidbody = _player.GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (_playerRigidbody.velocity.y >= Mathf.Epsilon || _player.IsDown)
        {
            transform.gameObject.layer = 0;
            _collider.isTrigger = true;
        }
        else
        {
            transform.gameObject.layer = 7;
            _collider.isTrigger = false;
        }
    }
}
