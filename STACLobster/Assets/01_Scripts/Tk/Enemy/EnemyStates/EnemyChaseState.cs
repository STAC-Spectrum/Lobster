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
        // �� ������Ʈ�� �������� �ٲ���� �𸣰����� �ϴ� �ߵǱ� ������ �� ����
    }

    public override void Exit()
    {
        // �� �ڲ� ��������?
        // �� �ڲ� ��������?
        // �� �ڲ� ��������?
        // �� �ڲ� ��������?
        // �� �ڲ� ��������?
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
