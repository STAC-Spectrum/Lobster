using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, string animName) : base(enemy, stateMachine, animName)
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

        if(_enemy.playerPos != Vector3.zero) 
        {
            Debug.Log("Transition to Chase State");
            _stateMachine.ChangeState(EnemyStateEnum.Chase);
        }

        Move();
    }
    private void Move()
    {
        _enemy.Rigid.velocity = _enemy.transform.right * _enemy.moveSpeed;
    }
}
