using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(Enemy enemy, EnemyStateMachine stateMachine, string animName) : base(enemy, stateMachine, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Vector3 pTrm = _enemy.ChaseRangeCast();
        if (pTrm == Vector3.zero)
        {
            _enemy.StateMachine.ChangeState(EnemyStateEnum.Idle);
        }
        if(pTrm != null)
        {
            _enemy.transform.rotation = Quaternion.LookRotation((pTrm - _enemy.transform.position).normalized);
            _enemy.Rigid.velocity = _enemy.transform.right * _enemy.moveSpeed;
        }
    }
}
