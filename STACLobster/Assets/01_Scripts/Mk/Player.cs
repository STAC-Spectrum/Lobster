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
    [SerializeField] private float _maxDistance = 100f;
    private PlayerInput _playerInput;
    private Rigidbody _rigidbody;
    private Vector3 _velocity;
    private float _verticalVelocity;
    private Vector3 mousePos;
    private Camera _main;
    private bool _isJump = false;

    #region 수정 해야함 야발 프로토타입

    [SerializeField] private GameObject _lightTrail;
    [SerializeField] private GameObject _lightAnlge;

    [SerializeField] private float _lightPower = 8f;

    private bool _isMouse = false;
    private float _angle;

    #endregion

    public Action MouseMoveEvent;
    private Vector2 _mousePos;

    private void Awake()
    {
        _main = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
        //playerinput???
        PlayerInput playerInput = new PlayerInput();
        playerInput.Player.Enable();
        _inputReader.JumpEvent += JumpHandle;
        _inputReader.BulletTimeEvent += BulletTimeHandle;
        _inputReader.LightRush += LightRushHandle;
        MouseMoveEvent += MouseMoveHandle;
        //_lightTrail.SetActive(false);
    }


    private void LightRushHandle(double pos)
    {
        //print($"마우스 위치 {_mouseCam.ScreenToWorldPoint(pos)}");

        print(pos);

        /*if (_inputReader._playerActions.Player.BulletTime.WasReleasedThisFrame())
        {
            print(_mousePos);
            _mousePos = _mouseCam.ScreenToWorldPoint(pos);
            _rigidbody.AddForce(_mousePos, ForceMode.Impulse);
        }*/

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

    private void Update()
    {
        print(_rigidbody.velocity);
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            //print("마우스 눌림");
            mousePos = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane));
            //print(mousePos);
            Vector3 moveDir = new Vector3(mousePos.x, mousePos.y, 0).normalized;
            _rigidbody.AddForce(moveDir * _lightPower, ForceMode.Impulse);
            if (Physics.Raycast(mousePos, Vector3.forward, out RaycastHit hit, _maxDistance, _layerMask))
            {
                print("맞음");
                //여기 레이 맞으면 충돌 코드 추가해야함
            }
        }

        // 테스트 큐브 바라보기
        // 방향 벡터 = 목표 벡터 - 시작 벡터
        // 이 함수 나중에 조져야 함
        Vector3 targetPos = MousePositionCalculate();
        Vector3 dir = targetPos - _lightAnlge.transform.position;
        Quaternion rot = Quaternion.LookRotation(dir.normalized);

        _lightAnlge.transform.rotation = rot;
        _lightAnlge.transform.eulerAngles = new Vector3(_lightAnlge.transform.eulerAngles.x + 180,
            _lightAnlge.transform.eulerAngles.y, 0);

        

        if (_isMouse)
        {
            _lightTrail.SetActive(true);
            /*transform.position = Vector2.MoveTowards(
                transform.position,
                new Vector2(mousePos.x * 10, mousePos.y * 10).normalized * testpower, 
                _mouseSpeed * Time.deltaTime);*/
        }

    }

    private Vector3 MousePositionCalculate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(mousePos.x, mousePos.y);
    }

    private void MouseMoveHandle()
    {
        _angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;


        //_lightAnlge.transform.LookAt(new Vector3(mousePos.x, _lightTrail.transform.rotation.y, 0));
        _lightAnlge.transform.rotation = Quaternion.AngleAxis(_angle - 90, Vector3.right);

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
        if(_isJump == true) return;
        _isJump = true;

        if (Physics.Raycast(transform.position, Vector2.down, out RaycastHit hit, _maxDistance, _layerMask))
        {
            //_rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _jumpPower, _rigidbody.velocity.z);
            //AddForce할때는 왜 움직이면 잘 안나가지?
            //_rigidbody.AddForce(Vector2.up * _jumpPower, ForceMode.Impulse);
            //print(_rigidbody.velocity);
        }
        _isJump = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * 1.2f);
        Gizmos.DrawRay(mousePos, Vector3.forward * _maxDistance);
    }
}
