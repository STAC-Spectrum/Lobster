using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStateEnum
{
    Idle,
    Chase,
    Attack,
    Die,
}

public abstract class Enemy : MonoBehaviour
{
    [Header("Setting Values")]
    [SerializeField] protected float attackRange;
    public float moveSpeed;
    public float changeDirTime;
    public Vector2 randomDirection;

    public Rigidbody Rigid { get; private set; }
    public EnemyStateMachine StateMachine { get; protected set; }

    public bool CanStateChangeable { get; private set; } = true;
    public bool PlayerInRange { get; private set; } = false;
    
    public virtual void Awake()
    {
        Transform visualTrm = transform.Find("Visual");
        Rigid = GetComponent<Rigidbody>();

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

    public virtual void ChaseRangeCast()
    {

    }
}
