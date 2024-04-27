using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBossIdleState : CrystalBossState
{
    float time;
    int skill;
    public CrystalBossIdleState(CrystalBoss boss, CrystalBossStateMachine bossStateMachine, string animationName) : base(boss, bossStateMachine, animationName)
    {
    }
    public override void UpdateState()
    {

        //Collider player = _boss.IsPlayerSphereDetection();
        //if (player == null) return;
        //_boss.target = player.transform;
        //_stateMachine.ChangeState(CrystalBossStateEnum.PillarAttack);

        time += Time.deltaTime;
        if(time >3)
        {
            _stateMachine.ChangeState(_stateMachine.SkillList[skill]);
            skill++;
            if (_stateMachine.SkillList.Count-1 < skill)
                skill = 0;
            time = 0;
            //_stateMachine.SkillList[]
        }

    }


}
