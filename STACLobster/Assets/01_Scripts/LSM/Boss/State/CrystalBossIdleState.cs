using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBossIdleState : CrystalBossState
{
    float time;
    int skill=0;
    bool isGroundPlayer = false;
    public CrystalBossIdleState(CrystalBoss boss, CrystalBossStateMachine bossStateMachine, string animationName) : base(boss, bossStateMachine, animationName)
    {
    }
    public override void UpdateState()
    {

        //Collider player = _boss.IsPlayerSphereDetection();
        //if (player == null) return;
        //_boss.target = player.transform;
        //_stateMachine.ChangeState(CrystalBossStateEnum.PillarAttack);
        Vector3 vec = new Vector3(_boss.transform.position.x, _boss.transform.position.y + _boss._bossRoomSize.y / 2, _boss.transform.position.z);
        if (!(_boss.IsPlayerCubeDetection(vec, _boss._bossRoomSize) == null)) isGroundPlayer = true;
        else if (isGroundPlayer == false) return;
        time += Time.deltaTime;
        if(time >3 && isGroundPlayer)
        {
            
            if (_stateMachine.SkillList.Count - 1 < skill)
                skill = 0;
            _stateMachine.ChangeState(_stateMachine.SkillList[0]);
            skill++;
            
            time = 0;
            //_stateMachine.SkillList[]
        }

    }


}
