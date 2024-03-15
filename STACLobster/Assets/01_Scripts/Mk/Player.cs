using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Agent
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float _moveSpeed = 5f, _jumpPower = 7f, _forceGravity = 15f;
    [SerializeField] private LayerMask _layerMask;

    private Rigidbody _rigidbody;
    private PlayerInput _playerInput;

    public float testpower;
    
    private Vector3 _velocity;
    private Vector3 mousePos;
    private float _verticalVelocity;

    #region 수정 해야함 야발 프로토타입

    [SerializeField] private GameObject _lightTrail;
    [SerializeField] private GameObject _lightAnlge;

    [SerializeField] private Camera _mouseCam;

    private bool _isMouse = false;
    [SerializeField] private float _mouseSpeed = 30;
    private float _angle;

    #endregion
    

    public Action MouseMoveEvent;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        PlayerInput playerInput = new PlayerInput();
        playerInput.Player.Enable();
        _inputReader.JumpEvent += JumpHandle;
        MouseMoveEvent += MouseMoveHandle;
        _lightTrail.SetActive(false);
    }


    public void SetPosition(Vector3 movement) //보류
    {
        _velocity = movement * Time.fixedDeltaTime;

        if (_velocity.sqrMagnitude > 0)
        {
            // rotation
        }
    }

    private void Update()
    {
        MouseCheck(); //Input 따로 받아서 처리해야함

        if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            mousePos = _mouseCam.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, _mouseCam.farClipPlane));
            MouseMoveEvent?.Invoke();
        }

        if (_isMouse)
        {
            _lightTrail.SetActive(true);
            transform.position = Vector2.MoveTowards(
                transform.position,
                new Vector2(mousePos.x * 10, mousePos.y * 10).normalized * testpower, 
                _mouseSpeed * Time.deltaTime);
        }
        
    }
    
    private void MouseMoveHandle()
    {
        _angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;
        
        
        //_lightAnlge.transform.LookAt(new Vector3(mousePos.x, _lightTrail.transform.rotation.y, 0));
        _lightAnlge.transform.rotation = Quaternion.AngleAxis(_angle-90, Vector3.right);
        
        _lightAnlge.transform.rotation = Quaternion.Euler(_lightAnlge.transform.eulerAngles.x * -1, 90, 0);
        
        _lightTrail.SetActive(true);

        _isMouse = true;
        
        //_rigidbody.AddForce(new Vector3(mousePos.x * 10000, mousePos.y, 0).normalized * testpower, ForceMode.Impulse);
        StartCoroutine(MouseMoveStop());
    }

    private IEnumerator MouseMoveStop()
    {
        yield return new WaitForSeconds(0.32f);
        _isMouse = false;
        _lightTrail.SetActive(false);
    }

    private void MouseCheck()
    {
        if (Input.GetMouseButton(1))
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0.5f, Time.unscaledDeltaTime * 3);
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void GravityCalculate()
    {
        Physics.gravity = Vector2.down * _forceGravity;
        //_rigidbody.AddForce(Vector2.down * _forceGravity);
    }

    private void FixedUpdate()
    {
        GravityCalculate();
        //_playerInput.Player.Movement.ReadValue<Vector2>();
        Vector2 inputVector = _inputReader._playerActions.Player.Movement.ReadValue<Vector2>();
        _rigidbody.velocity = new Vector2(inputVector.x * _moveSpeed, _rigidbody.velocity.y);
    }

    public void JumpHandle()
    {
        bool hit = Physics.Raycast(transform.position, Vector2.down, 1.2f, _layerMask);

        if (hit)
        {
            _rigidbody.AddForce(Vector2.up * _jumpPower);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * 1.2f);
    }
}
