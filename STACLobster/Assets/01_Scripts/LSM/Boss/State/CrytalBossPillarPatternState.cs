using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrytalBossPillarPatternState : CrystalBossState
{
    private Transform ground;
    private float time;
    public CrytalBossPillarPatternState(CrystalBoss boss, CrystalBossStateMachine bossStateMachine, string animationName) : base(boss, bossStateMachine, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _boss.PrefabSpawn(_boss.PrefabList[1], "CrystalGround", 1,this);
        ground = _boss.transform.Find("Ground");
        ground.gameObject.SetActive(true);
        //if(_boss.IsPlayerCubeDetection())
        //{
        //    Debug.Log(1);
        //}
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public IEnumerator PillarPattern()
    {
        ground.gameObject.SetActive(true);
        ground.GetComponent<MeshRenderer>().material.DOColor(Color.red, 2);
        yield return new WaitForSeconds(1);
        ground.gameObject.SetActive(false);



    }



    public override void UpdateState()
    {

        //time = Time.deltaTime;
        //if()
        //{
        //    ground.GetComponent<MeshRenderer>().material.DOColor(Color.red,2);
        //}
        

    }


}
