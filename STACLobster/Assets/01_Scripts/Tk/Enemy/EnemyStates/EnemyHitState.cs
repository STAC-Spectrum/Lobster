using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : EnemyState
{
    public EnemyHitState(Enemy enemy, EnemyStateMachine stateMachine, string animName) : base(enemy, stateMachine, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(_endTriggerCalled) // 넉백 애니메이션이 끝나면 맞고있는 상태에서 Idle로 전환하는 거임
        {
            _stateMachine.ChangeState(EnemyStateEnum.Idle);
        }
    }
}
