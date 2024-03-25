using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Agent
{
    [SerializeField] private InputReader _inputReader;
    private Camera _main;

    #region Move related variables
    
    [SerializeField] private float _moveSpeed = 5f, _jumpPower = 7f, _forceGravity = 15f;
    
    #endregion

    #region Rigidbody related variables

    private Rigidbody _rigidbody;
    private Vector3 _velocity;
    
    #endregion

    #region Ray related variables

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _maxDistance = 100f;
    
    #endregion
    
    private Vector3 mousePos;  // 현재 gizmos 그리기 용
    public Action MouseMoveEvent;

    private void Awake()
    {
        _main = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
        PlayerInput playerInput = new PlayerInput();
        playerInput.Player.Enable();
        _inputReader.JumpEvent += JumpHandle;
        _inputReader.BulletTimeEvent += BulletTimeHandle;
        MouseMoveEvent += MouseMoveHandle;
    }

    private void BulletTimeHandle(bool isPress)
    {
        if (isPress)
            Time.timeScale = 0.4f;
        else if (isPress == false)
            Time.timeScale = 1f;

    }
    
    public void SetPosition(Vector3 movement) //보류
    {
        _velocity = movement * Time.fixedDeltaTime;

        if (_velocity.sqrMagnitude > 0)
        {
            // rotation
        }
    }

    // 이 함수는 new input system이 대체 가능 
    // 나중에 삭제 예정
    private Vector3 GetMousePos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane));
        return mousePos;
    }

    private Vector3 MousePositionCalculate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(mousePos.x, mousePos.y);
    }

    private void MouseMoveHandle()
    {
        
    }

    private void GravityCalculate()
    {
        Physics.gravity = Vector2.down * _forceGravity;
        //_rigidbody.AddForce(Vector2.down * _forceGravity);
    }

    private void FixedUpdate()
    {
        GravityCalculate();
        Vector2 inputVector = _inputReader._playerActions.Player.Movement.ReadValue<Vector2>();
        _rigidbody.velocity = new Vector2(inputVector.x * _moveSpeed, _rigidbody.velocity.y);
    }

    public void JumpHandle()
    {
        if (Physics.Raycast(transform.position, Vector2.down, out RaycastHit hit, _maxDistance, _layerMask))
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _jumpPower, _rigidbody.velocity.z);
        }
    }

    // Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * _maxDistance);
        Gizmos.DrawRay(mousePos, Vector3.forward * _maxDistance);
    }
}
