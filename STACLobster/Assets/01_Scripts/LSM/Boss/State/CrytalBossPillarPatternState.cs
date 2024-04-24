using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrytalBossPillarPatternState : CrystalBossState
{
    public CrytalBossPillarPatternState(CrystalBoss boss, CrystalBossStateMachine bossStateMachine, string animationName) : base(boss, bossStateMachine, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _boss.PrefabSpawn(_boss.PrefabList[1], "CrystalGround", 1,this);
    }

    public override void Exit()
    {
        base.Exit();
    }

    //public IEnumerator PillarPattern()
    //{



    //}

    public override void UpdateState()
    {
        base.UpdateState();
    }
}
