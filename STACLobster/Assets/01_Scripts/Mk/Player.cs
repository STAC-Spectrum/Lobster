using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Agent
{
    [SerializeField] private InputReader _inputReader;
    private Camera _main;

    #region Lights

    [SerializeField] private GameObject _testLight;

    #endregion

    #region Object

    private Transform _visual;

    #endregion

    #region Move related variables

    [SerializeField] private float _moveSpeed = 5f, _jumpPower = 7f, _forceGravity = 15f;
    public bool IsDown { get; private set; }

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
        _main = Camera.main;
        
        _rigidbody = GetComponent<Rigidbody>();
        _visual = transform.Find("Visual").GetComponent<Transform>();
        
        PlayerInput playerInput = new PlayerInput();
        playerInput.Player.Enable();
        
        _testLight.SetActive(false);
        
        #region Events

        _inputReader.JumpEvent += JumpHandle;
        _inputReader.BulletTimeEvent += BulletTimeHandle;
        MouseMoveEvent += MouseMoveHandle;

        #endregion
    }

    #region Unity Life Cycles
    private new void Awake()
    {
        Initalize();
    }

    private void Update()
    {
        IsDown = _inputReader._playerActions.Player.MoveDown.IsPressed();
        
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, raycastMask))
        {
            Vector3 dir = hit.point;
            dir.z = 0;
            _testTargetMouse.transform.position = dir;
        }
    }

    [SerializeField] private LayerMask raycastMask;
    [SerializeField] private float _lightPower;
    [SerializeField] private GameObject _testTargetMouse;
    
    private void MoveToMousePos()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, raycastMask))
        {
            Vector3 dir = hit.point - transform.position;
            dir.z = 0;
            dir.Normalize();
            _rigidbody.AddForce(dir * _lightPower, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        GravityCalculate();
        Movement();
        
    }

    #endregion


    #region Event Handles
    private void BulletTimeHandle(bool isPress)
    {
        Time.timeScale = isPress ? 0.4f : 1f;

        // 일단 여기다가 빛변신 움직임 구현

        if (isPress) return;
        LightMove();
    }

    private void MouseMoveHandle()
    {
        // 마우스 좌표에 의해 빛 발사
    }

    private void JumpHandle()
    {
        // smart rider this is very efficient

        if (!Physics.Raycast(transform.position, Vector2.down, _maxDistance, _jumpLayerMask)) return;

        var velocity = _rigidbody.velocity;
        velocity = new Vector3(velocity.x, _jumpPower, velocity.z);
        _rigidbody.velocity = velocity;
    }

    #endregion

    #region MoveMethod


    private void Movement()
    {
        if (_isLightMove) return;
        
        print("움직임");
        Vector2 inputVector = _inputReader._playerActions.Player.Movement.ReadValue<Vector2>();
        _rigidbody.velocity = new Vector2(inputVector.x * _moveSpeed, _rigidbody.velocity.y);
    }

    private bool _isLightMove;
    [SerializeField] private float _lightSpeed = 8f;

    [SerializeField] private LayerMask _hitLightMove;

    private void LightMove()
    {
        if (_isLightMove) return;
        
        _visual.gameObject.SetActive(false);  // 캐릭터 숨기고
        _testLight.SetActive(true); // 빛 이펙트 보이고
        _isLightMove = true;
        _rigidbody.useGravity = false;  // 중력 제거
        
        MoveToMousePos();
        StartCoroutine(LightMoveDelay());

    }
    
    private IEnumerator LightMoveDelay()
    {
        yield return new WaitForSeconds(1.2f);
        _visual.gameObject.SetActive(true);
        _testLight.SetActive(false);
        _rigidbody.useGravity = true;
        _isLightMove = false;
    }
    #endregion


    // 이 함수는 new input system이 대체 가능 
    // 나중에 삭제 예정
    private Vector3 GetMousePos()
    {
        Vector3 mousePos = Camera.main!.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane));
        return mousePos;
    }

    private void GravityCalculate()
    {
        Physics.gravity = Vector2.down * _forceGravity;
    }

    // Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * _maxDistance);
        Gizmos.DrawRay(_mousePos, Vector3.forward * _maxDistance);
    }
}
