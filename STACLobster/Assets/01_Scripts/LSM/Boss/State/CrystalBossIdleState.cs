using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBossIdleState : CrystalBossState
{
    public CrystalBossIdleState(CrystalBoss boss, CrystalBossStateMachine bossStateMachine, string animationName) : base(boss, bossStateMachine, animationName)
    {
    }
    public override void UpdateState()
    {

        Collider player = _boss.IsPlayerCubeDetection();
        if (player == null) return;
        _boss.target = player.transform;
        _stateMachine.ChangeState(CrystalBossStateEnum.PillarAttack);

    }


}
