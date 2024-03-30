using System;
using System.Collections;
using UnityEngine;

public class Player : Agent
{
    [SerializeField] private InputReader _inputReader;
    private Camera _main;

    #region Lights

    /// <summary>
    /// Light Laser Object
    /// </summary>
    //[SerializeField] private GameObject _lightObj;

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
        # region Camera
        
        _main = Camera.main;
        
        # endregion
        
        # region Components
        
        _rigidbody = GetComponent<Rigidbody>();
        
        # endregion

        #region Object

        _visual = transform.Find("Visual").GetComponent<Transform>();

        #endregion
        
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
        print(_visual);
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
    
    #endregion
    

    # region Event Handles
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
    }
    
    private bool _isLightMove;
    [SerializeField] private float _lightSpeed = 8f;

    private void LightMove()
    {
        _isLightMove = true;
        
        _visual.gameObject.SetActive(false);
        Vector2 vec = GetMousePos();
        _rigidbody.AddForce(vec.normalized * _lightSpeed, ForceMode.Impulse);
        StartCoroutine(LightMoveDelay());

    }
    
    private IEnumerator LightMoveDelay()
    {
        yield return new WaitForSeconds(1.2f);
        _visual.gameObject.SetActive(true);
        _isLightMove = false;
    }
    
    public void SetPosition(Vector3 movement) //보류
    {
        _velocity = movement * Time.fixedDeltaTime;

        if (_velocity.sqrMagnitude > 0)
        {
            // rotation
        }
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
