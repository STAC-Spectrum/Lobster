using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrytalBossPillarPatternState : CrystalBossPatternState
{
    private Transform ground;
    private float time;
    private Transform crystalParent;
    public CrytalBossPillarPatternState(CrystalBoss boss, CrystalBossStateMachine bossStateMachine, string animationName) : base(boss, bossStateMachine, animationName)
    {
        crystalParent = _boss.transform.Find("CrystalGround");
    }

    public override void Enter()
    {
        base.Enter();
        if(crystalParent.childCount ==0)
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
        yield return new WaitForSeconds(0.3f);
        prefabList[0].transform.localScale = _boss.PrefabList[1].transform.localScale;
        prefabList[0].SetActive(true);
        _boss.IsPlayerCubeDetection(ground.localScale/2);
        yield return new WaitForSeconds(1.5f);
        prefabList[0].SetActive(false);
        _stateMachine.ChangeState(CrystalBossStateEnum.Idle);

    }



    public override void UpdateState()
    {

        //time = Time.deltaTime;
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    _boss.StartCoroutine(PillarPattern());
        //}


    }


}
