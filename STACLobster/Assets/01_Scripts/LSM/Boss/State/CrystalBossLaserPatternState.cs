using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CrystalBossLaserPatternState : CrystalBossPatternState
{
    bool isLaser;
    float time = 0;
    private Transform crystalParent;
    //List<GameObject> LaserList = new List<GameObject>();
    public CrystalBossLaserPatternState(CrystalBoss boss, CrystalBossStateMachine bossStateMachine, string animationName) : base(boss, bossStateMachine, animationName)
    {
        crystalParent = _boss.transform.Find("LaserParent");
    }

    public override void Enter()
    {
        base.Enter();
        crystalParent.localRotation = Quaternion.Euler(Vector3.zero);
        if (crystalParent.childCount == 0)
            _boss.PrefabSpawn(_boss.PrefabList[0], "LaserParent", _boss.LaserCount, this);
        _boss.transform.DOMoveY(_boss.transform.position.y + 5, 0.7f);
 
        _boss.StartCoroutine(Spawn());
        
        //mySequence.
        //    Append();

    }


    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(0.8f);
        int angle = 0;
        crystalParent.gameObject.SetActive(true);
        for (int i = 0; i < prefabList.Count; ++i)
        {
            angle = 360 / prefabList.Count * i;
            prefabList[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            prefabList[i].transform.Translate(Vector3.up * 5);
            //s.prefabList[i].transform.DOScaleY(2, 0.1f);
            prefabList[i].SetActive(true);
            prefabList[i].transform.DOScale(new Vector3(0.1f, 10, 0.1f), 0.1f);

        }
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < prefabList.Count; ++i)
        {
            prefabList[i].transform.DOScale(new Vector3(1,10,1), 0.1f);

        }
        yield return new WaitForSeconds(1.5f);
        isLaser = true;
        
    }

    private IEnumerator StopLaserPattern()
    {
        for(int i =0;i<prefabList.Count;++i)
        {
            prefabList[i].transform.DOScale(new Vector3(0.1f, 10, 0.1f), 1);
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < prefabList.Count; ++i)
        {
            prefabList[i].transform.Translate(Vector3.down * 5);
        }
        crystalParent.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        _stateMachine.ChangeState(CrystalBossStateEnum.PillarAttack);

    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void UpdateState()
    {
        if(isLaser == true)
        {
            if (time < 3)
            {
                time += Time.deltaTime;
                crystalParent.Rotate(Vector3.left * 50 * Time.deltaTime);
            }
            else
            {
                isLaser = false;
                time = 0;
                _boss.StartCoroutine(StopLaserPattern());

            }
        }

    }

}

    //public void Spawn()
    //{
     
    //    {
            
    //        //mySequence
    //        //    .Append(_boss.prefabList[i].transform.DORotate(new Vector3(angle, 0, 0), 0.1f))
    //        //    .Join(_boss.prefabList[i].transform.DOMoveY(5, 1))
    //        //    .Insert(3, _boss.prefabList[i].transform.DOScaleY(5, 1.5f));

                
    //    }


    //}