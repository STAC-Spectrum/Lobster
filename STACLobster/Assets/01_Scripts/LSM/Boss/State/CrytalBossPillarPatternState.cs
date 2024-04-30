using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrytalBossPillarPatternState : CrystalBossPatternState
{
    private Transform ground;
    private float time;
    private Transform crystalParent;
    private RaycastHit hit;

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
        if (Physics.Raycast(_boss.transform.position, Vector3.down, out hit, 10, 1 << LayerMask.NameToLayer("Ground")))
        {
            ground.transform.position = hit.point;
        }
        ground.gameObject.SetActive(true);
        ground.GetComponent<MeshRenderer>().material.DOColor(Color.red, 2);
        yield return new WaitForSeconds(1);
        ground.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        prefabList[0].transform.localScale = _boss.PrefabList[1].transform.localScale;
        _boss.transform.DOMoveY(_boss.transform.position.y -5, 0.2f);
        yield return new WaitForSeconds(0.2f);
        prefabList[0].SetActive(true);
        prefabList[0].transform.position = hit.point;
        //_boss.IsPlayerCubeDetection(_boss.transform.position,ground.localScale);
        yield return new WaitForSeconds(1.5f);
        prefabList[0].SetActive(false);
        _stateMachine.ChangeState(CrystalBossStateEnum.Idle);

    }



    public override void UpdateState()
    {


    }


}
