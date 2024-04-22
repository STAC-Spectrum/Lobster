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
        _enemy.isChasing = true;
    }

    public override void Exit()
    {
        _enemy.isChasing = false;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.AttackRangeCast())
        {
            Debug.Log("Transition to Attack State");
            _enemy.StateMachine.ChangeState(EnemyStateEnum.Attack);
        }

        Vector3 pTrm = _enemy.ChaseRangeCast(); // 범위 내에 플레이어가 있으면 pTrm에 플레이어의 Transform을 넣어줌
        if (pTrm == Vector3.zero)
        {
            Debug.Log("Transition to Idle State");
            _enemy.StateMachine.ChangeState(EnemyStateEnum.Idle);
        }
        if(pTrm != null)
        {          
            if(pTrm.y > _enemy.transform.position.y)
            {
                _enemy.Rigid.velocity = Vector3.zero;
            }

            if(pTrm.x >= _enemy.transform.position.x)
            {
                _enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                _enemy.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            _enemy.Rigid.velocity = _enemy.transform.right * _enemy.chaseSpeed;
        }
    }
}
