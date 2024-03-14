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
        Debug.Log("�÷��̾ �i�� ��");
    }

    public override void Exit()
    {
        Debug.Log("�÷��̾� ���� ����");
        base.Exit();
    }

    public override void UpdateState()
    {
        if (!_enemy.PlayerInRange)
        {
            _stateMachine.ChangeState(EnemyStateEnum.Idle);
        }
        else
        {
            _enemy.Rigid.velocity = new Vector3(_enemy.randomDirection.x, 0, 
                _enemy.randomDirection.y) * _enemy.moveSpeed;
        }
        base.UpdateState();
    }
}
