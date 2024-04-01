using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    protected EnemyStateMachine _stateMachine;

    protected Enemy _enemy;

    protected int _animBoolHash;

    protected bool _endTriggerCalled = false;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, string animName)
    {
        _enemy = enemy;
        _stateMachine = stateMachine;
        _animBoolHash = Animator.StringToHash(animName);
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void UpdateState()
    {

    }
}
