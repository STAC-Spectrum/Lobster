using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine, string animName) : base(enemy, stateMachine, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("공격 실행");
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_endTriggerCalled)
        {
            _stateMachine.ChangeState(EnemyStateEnum.Idle);
        }

        if (!_enemy.AttackRangeCast())
        {
            Debug.Log("Transition to Chase State");
            _stateMachine.ChangeState(EnemyStateEnum.Chase);
        }

        _enemy.StopImmediately();
    }

    public override void Exit()
    {
        _endTriggerCalled = true;
        _enemy.lastAttackTime = Time.time;
        base.Exit();
    }
}
