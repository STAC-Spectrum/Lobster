using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrytalBossPillarPatternState : CrystalBossPatternState
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
        if(ground ==null)
            ground = _boss.transform.Find("Ground");
        _boss.StartCoroutine(PillarPattern());
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
        //if (_boss.IsPlayerCubeDetection(ground.localScale/2))
            //Debug.Log(1);
        ground.gameObject.SetActive(false);



    }



    public override void UpdateState()
    {

        //time = Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E))
        {
            _boss.StartCoroutine(PillarPattern());
        }


    }


}
