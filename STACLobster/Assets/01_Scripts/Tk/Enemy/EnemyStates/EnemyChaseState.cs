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
        // 왜 스테이트가 무한으로 바뀌는지 모르겠지만 일단 잘되기 떄문에 걍 냅둠
    }

    public override void Exit()
    {
        // 왜 자꾸 나가지지?
        // 왜 자꾸 나가지지?
        // 왜 자꾸 나가지지?
        // 왜 자꾸 나가지지?
        // 왜 자꾸 나가지지?
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
            _enemy.Rigid.velocity = _enemy.transform.forward * _enemy.moveSpeed;
        }
    }
}
