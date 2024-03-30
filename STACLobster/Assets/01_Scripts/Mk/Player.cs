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

    [SerializeField] private LayerMask _jumpLayerMask;
    [SerializeField] private float _maxDistance = 100f;
    
    #endregion
    
    private Vector3 _mousePos;  // 현재 gizmos 그리기 용
    public Action MouseMoveEvent;

    private void Initalize()
    {
        # region Camera
        
        _main = Camera.main;
        
        # endregion
        
        # region Components
        
        _rigidbody = GetComponent<Rigidbody>();
        
        # endregion
        
        # region Input
        
        PlayerInput playerInput = new PlayerInput();
        playerInput.Player.Enable();
        
        # endregion
        
        # region Events
        
        _inputReader.JumpEvent += JumpHandle;
        _inputReader.BulletTimeEvent += BulletTimeHandle;
        MouseMoveEvent += MouseMoveHandle;
        
        # endregion
    }

    #region Unity Life Cycles
    private new void Awake()
    {
        Initalize();
    }

    private void Update()
    {
        IsDown = _inputReader._playerActions.Player.MoveDown.IsPressed();
    }

    private void FixedUpdate()
    {
        GravityCalculate();
        Movement();
    }

    private void Movement()
    {
        Vector2 inputVector = _inputReader._playerActions.Player.Movement.ReadValue<Vector2>();
        _rigidbody.velocity = new Vector2(inputVector.x * _moveSpeed, _rigidbody.velocity.y);
    }
    
    #endregion
    

    # region Event Handles
    private void BulletTimeHandle(bool isPress)
    {
        Time.timeScale = isPress ? 0.4f : 1f;
    }
    
    private void MouseMoveHandle()
    {
        
    }
    
    private void JumpHandle()
    {
        // smart rider this is very efficient
        
        if (!Physics.Raycast(transform.position, Vector2.down, _maxDistance, _jumpLayerMask)) return;
        
        var velocity = _rigidbody.velocity;
        velocity = new Vector3(velocity.x, _jumpPower, velocity.z);
        _rigidbody.velocity = velocity;
    }
    
    public bool IsDown { get; private set; }
    
    #endregion
    
    
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
        Vector3 mousePos = Camera.main!.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane));
        return mousePos;
    }

    private Vector3 MousePositionCalculate()
    {
        Vector3 mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(mousePos.x, mousePos.y);
    }

    private void GravityCalculate()
    {
        Physics.gravity = Vector2.down * _forceGravity;
        //_rigidbody.AddForce(Vector2.down * _forceGravity);
    }
    
    // Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * _maxDistance);
        Gizmos.DrawRay(_mousePos, Vector3.forward * _maxDistance);
    }
}
