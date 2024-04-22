using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CrystalBossLaserPatternState : CrystalBossState
{
    
    public CrystalBossLaserPatternState(CrystalBoss boss, CrystalBossStateMachine bossStateMachine, string animationName) : base(boss, bossStateMachine, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Spawn();
        //mySequence.
        //    Append();

    }

    public void Spawn()
    {
        _boss.LaserSpawn(_boss.LaserPrefab,3);
        int angle = 0;
        for (int i=0;i<_boss.prefabList.Count;++i)
        {
            _boss.prefabList[i].transform.DOScaleY(5, 3);
            angle = 360 / _boss.prefabList.Count * i;
            _boss.prefabList[i].transform.rotation = Quaternion.Euler(new Vector3(angle,0,0));
            _boss.prefabList[i].transform.Translate(Vector3.up*5);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {

        //if (isPattern == false)

    }

}
