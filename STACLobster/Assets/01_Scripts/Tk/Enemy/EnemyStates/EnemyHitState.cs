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
        if(_endTriggerCalled) // �˹� �ִϸ��̼��� ������ �°��ִ� ���¿��� Idle�� ��ȯ�ϴ� ����
        {
            _stateMachine.ChangeState(EnemyStateEnum.Idle);
        }
    }
}
