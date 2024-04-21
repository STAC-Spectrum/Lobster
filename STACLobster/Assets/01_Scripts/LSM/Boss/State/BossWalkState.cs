using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWalkState : BossState
{

    private BossMovement movementCompo;

    public BossWalkState(Boss boss, BossStateMachine bossStateMachine, string animationName) : base(boss, bossStateMachine, animationName)
    {

        movementCompo = boss.MovemetCompo as BossMovement;

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

        if (_boss.IsPlayerDetection() == null)
            _stateMachine.ChangeState(BossStateEnum.Idle);
        if ((_boss.target.transform.position - _boss.transform.position ).sqrMagnitude <= 3)
        {
            _boss.MovemetCompo.Stop();
            return;

        }
        Vector3 direction = _boss.target.position.z - _boss.transform.position.z > 1 ? Vector3.forward : Vector3.back;
        _boss.MovemetCompo.Move(direction);

        Vector3 bossRotation = (_boss.target.position - _boss.transform.position);
        bossRotation.y = 0;
        _boss.transform.rotation = Quaternion.LookRotation(bossRotation);

    }
}
