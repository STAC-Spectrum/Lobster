using System;
using System.Collections;
using UnityEngine;

public enum EnemyStateEnum
{
    Idle,
    Chase,
    Attack,
    Dead,
}

public abstract class Enemy : MonoBehaviour
{
    #region 설정 해줘야 하는 값들
    [Header("Setting Values")]
    [SerializeField] protected float _chaseRange;
    [SerializeField] protected float _attackRange;
    [SerializeField] private Transform _footTrm;
    public float health;
    public float lastAttackTime;
    public float attackCooldown;
    public float moveSpeed;
    public float chaseSpeed;
    public float defaultMoveSpeed;
    public Vector3 playerPos;
    protected Transform _visualTrm;
    private bool _isFlip = false;
    #endregion

    #region 벽이랑 땅 감지해주는 애들
    [Header("Checkers")] // 벽과 땅을 탐지하는 장치들
    public GroundChecker groundChecker;
    public WallChecker wallChecker;
    #endregion

    #region 프로퍼티들
    [HideInInspector] public Rigidbody Rigid { get; private set; }
    [HideInInspector] public EnemyStateMachine StateMachine { get; protected set; }
    [HideInInspector] public bool CanStateChangeable { get; protected set; } = true;
    [HideInInspector] public bool IsDead { get; protected set; } = false;
    #endregion 프로퍼티들

    public virtual void Awake()
    {
        _visualTrm = transform.Find("Visual");
        Rigid = GetComponent<Rigidbody>();
        defaultMoveSpeed = moveSpeed;

        StateMachine = new EnemyStateMachine();

        foreach(EnemyStateEnum state in Enum.GetValues(typeof(EnemyStateEnum)))
        {
            string stateName = state.ToString();

            try
            {
                Type t = Type.GetType($"Enemy{stateName}State");
                EnemyState enemyState = Activator.CreateInstance
                    (t, this, StateMachine, stateName) as EnemyState;
                StateMachine.AddState(state, enemyState);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }

    private void Update()
    {
        StateMachine.CurrentState.UpdateState();

        if (!groundChecker.CheckGround()) // 앞에 땅이 없는 상황에는 회전
        {
            RotateEnemy();         
        }
        else if (wallChecker.CheckWall()) // 앞이 벽으로 막혀있는 상황에는 회전
        {
            RotateEnemy();
        }
        ChaseRangeCast(); // 범위 내 적을 체크해줌
        Dead();
    }

    public Coroutine StartDelayCallback(float time, Action action)
    {
        return StartCoroutine(DelayCoroutine(time, action));
    }

    private IEnumerator DelayCoroutine(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }

    public virtual void Attack()
    {

    }

    public virtual Vector3 ChaseRangeCast()
    {
        playerPos = Vector3.zero;
        Collider[] playerInRange = Physics.OverlapSphere(transform.position,
            _chaseRange);
        if(playerInRange.Length > 0)
        {
            foreach (Collider hit in playerInRange)
            {
                if (hit.transform.TryGetComponent<TTesstt>(out TTesstt ts))
                {
                    playerPos = ts.transform.position;
                }
            }
        }
        else
        {
            playerPos = Vector3.zero;
        }
        return playerPos;
    }

    public virtual bool AttackRangeCast()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position,
            _attackRange);
        if (playerInRange.Length > 0)
        {
            foreach (Collider hit in playerInRange)
            {
                if (hit.transform.TryGetComponent<TTesstt>(out TTesstt ts))
                {
                    return true;
                }
            }
        }
        else
        {
            return false;
        }
        return false;
    }

    public void RotateEnemy()
    {
        if (!_isFlip)
        {
            _isFlip = true;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            _isFlip = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void Dead()
    {
        if(health <= 0 && !IsDead)
        {
            Debug.Log("Transition to Dead State");
            IsDead = true;
            StateMachine.ChangeState(EnemyStateEnum.Dead);
            CanStateChangeable = false;
        }
    }

    public void StopImmediately()
    {
        Rigid.velocity = Vector3.zero;
    }
}
