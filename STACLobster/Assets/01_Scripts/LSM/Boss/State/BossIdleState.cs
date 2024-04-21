using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossState
{
    public BossIdleState(Boss boss, BossStateMachine bossStateMachine, string animationName) : base(boss, bossStateMachine, animationName)
    {
    }
    public override void UpdateState()
    {

        Collider player = _boss.IsPlayerDetection();
        if (player == null) return;
        _boss.target = player.transform;
        _stateMachine.ChangeState(BossStateEnum.Walk);

    }


}
