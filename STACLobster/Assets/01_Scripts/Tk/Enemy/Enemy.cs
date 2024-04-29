using System;
using System.Collections;
using UnityEngine;

public enum EnemyStateEnum
{
    Idle,
    Chase,
    Attack,
    Dead,
    Hit, // �¾��� ����
}

public abstract class Enemy : MonoBehaviour, IHitable
{
    #region ���� ����� �ϴ� ����
    [Header("Setting Values")]
    [SerializeField] protected float _chaseRange;
    [SerializeField] protected float _attackRange;
    [SerializeField] private Transform _footTrm;
    [SerializeField] private LayerMask _whatIsPlayer;
    public float health;
    public float lastAttackTime;
    public float attackCooldown;
    public float moveSpeed;
    public float chaseSpeed;
    [HideInInspector] public float defaultMoveSpeed;
    [HideInInspector] public Vector3 playerPos;
    protected Transform _visualTrm;
    private bool _isFlip = false;
    private Collider[] _playerContainer;
    #endregion

    #region ���̶� �� �������ִ� �ֵ�
    [Header("Checkers")] // ���� ���� Ž���ϴ� ��ġ��
    public GroundChecker groundChecker;
    public WallChecker wallChecker;
    #endregion

    #region ������Ƽ��
    [HideInInspector] public Rigidbody Rigid { get; private set; }

    [HideInInspector] public Animator AnimatorCompo { get; private set; }

    [HideInInspector] public EnemyStateMachine StateMachine { get; protected set; }
    [HideInInspector] public bool CanStateChangeable { get; protected set; } = true;
    [HideInInspector] public bool IsDead { get; protected set; } = false;
    #endregion ������Ƽ��

    public virtual void Awake()
    {
        _visualTrm = transform.Find("Visual");
        Rigid = GetComponent<Rigidbody>();
        AnimatorCompo = GetComponentInChildren<Animator>();
        defaultMoveSpeed = moveSpeed;
        _playerContainer = new Collider[1];

        StateMachine = new EnemyStateMachine();

        foreach (EnemyStateEnum state in Enum.GetValues(typeof(EnemyStateEnum)))
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

        if (!groundChecker.CheckGround()) // �տ� ���� ���� ��Ȳ���� ȸ��
        {
            RotateEnemy();
        }
        else if (wallChecker.CheckWall()) // ���� ������ �����ִ� ��Ȳ���� ȸ��
        {
            RotateEnemy();
        }
        ChaseRangeCast(); // ���� �� ���� üũ����
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
        if (playerInRange.Length > 0)
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

    public virtual Collider AttackRangeCast()
    {
        int playerInRange = Physics.OverlapSphereNonAlloc(transform.position,
            _attackRange, _playerContainer, _whatIsPlayer);
        return playerInRange >= 1 ? _playerContainer[0] : null;
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

    public void HitProcess(float damage)
    {
        KnockbackProcess(damage * 0.1f);
        health -= damage;

        if (!IsDead)
        {
            Dead();
        }
    }

    private void KnockbackProcess(float knockbackForce = 2f)
    {
        Vector3 knockbackDir = transform.position - playerPos;
        knockbackDir.Normalize();
        Rigid.AddForce(knockbackDir * knockbackForce, ForceMode.Force);
    }

    public void Dead()
    {
        if (health <= 0)
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
