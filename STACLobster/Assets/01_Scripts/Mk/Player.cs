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
        _inputReader.AttackEvent += AttackHandle;

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
            _rigidbody.velocity = dir * _lightPower;
            //_rigidbody.AddForce(dir * _lightPower, ForceMode.Impulse);
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

        Vector2 inputVector = _inputReader._playerActions.Player.Movement.ReadValue<Vector2>();
        _rigidbody.velocity = new Vector2(inputVector.x * _moveSpeed, _rigidbody.velocity.y);

        if (inputVector != Vector2.zero)
            transform.right = new Vector3(_rigidbody.velocity.x, 0, 0);
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

    [SerializeField] private float _lightMoveDelayTime = 1.2f;

    private IEnumerator LightMoveDelay()
    {
        yield return new WaitForSeconds(_lightMoveDelayTime);
        _visual.gameObject.SetActive(true);
        _testLight.SetActive(false);
        _rigidbody.useGravity = true;
        _isLightMove = false;
    }
    #endregion


    //? 일단 급한 불 끄는 격으로
    //TODO : 프로토타입 끝나고 삭제

    [Header("Attack Value")]
    //* 적 체력을 100정도로
    /// <summary>
    /// Basic Player Attack Damage = 20
    /// </summary>
    public int _playerAttackDamage = 20;
    /// <summary>
    /// Player Attack Count
    /// </summary>
    private int _playerAttackCount = 0;


    public int PlayerAttackDamage
    {
        get { return _playerAttackDamage; }
        set
        {
            if (_playerAttackDamage >= 30)
            {
                _playerAttackDamage += 10;
            }
            else
            {
                _playerAttackDamage = value;
            }
            _playerAttackDamage = Mathf.Clamp(_playerAttackDamage, 20, 40);
        }
    }

    [Header("Ray Value")]
    [SerializeField] private float _rayRadius = 5f;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _rayMaxDistance = 3f;
    [SerializeField] private float _rayInterpolation = 0.5f;


    public void AttackHandle()
    {
        // 일단 플레이어 기본으로 주먹을 만들어야 함 관련 변수는 위에 추가하기로 하고

        Vector3 startPos = GetRayStartPos();
        if (Physics.SphereCast(
            startPos,
            _rayRadius, transform.right, out RaycastHit hit, _rayMaxDistance, _enemyLayer))
        {
            print("피격!!!");
            // 맞으면
            // 에너미한테 데미지 주기

            AttackDamageCombo();  // hit count 상승으로 다음 데미지 상승
        }

    }

    private void AttackDamageCombo()
    {
        PlayerAttackDamage += 5;  // 적 타격시 상승 데미지 폭 / 데미지가 30일 경우 10 상승 / 최대 40 데미지
        // TODO : 어택 콤보 스택 계산 필요함 / 어떻게 콤보 이을것 이고 끊을지 만들어야함
    }

    private Vector3 GetRayStartPos()
    {
        return transform.position + transform.right * -_rayInterpolation * 2;
    }

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

        //*

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(GetRayStartPos(), _rayRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GetRayStartPos() + transform.right * _rayMaxDistance, _rayRadius);
    }
}
