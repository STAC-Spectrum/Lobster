using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CrystalBossLaserPatternState : CrystalBossState
{
    bool isLaser;
    float time = 0;
    public CrystalBossLaserPatternState(CrystalBoss boss, CrystalBossStateMachine bossStateMachine, string animationName) : base(boss, bossStateMachine, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _boss.StartCoroutine(Spawn());
        //mySequence.
        //    Append();

    }


    private IEnumerator Spawn()
    {
        _boss.LaserSpawn(_boss.LaserPrefab, 6);
        int angle = 0;
        for (int i = 0; i < _boss.prefabList.Count; ++i)
        {
            angle = 360 / _boss.prefabList.Count * i;
            _boss.prefabList[i].transform.rotation = Quaternion.Euler(new Vector3(angle, 0, 0));
            _boss.prefabList[i].transform.Translate(Vector3.up * 3);
            _boss.prefabList[i].transform.DOScaleY(2, 0.1f);
            _boss.prefabList[i].SetActive(true);

        }
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < _boss.prefabList.Count; ++i)
        {
            _boss.prefabList[i].transform.DOScale(new Vector3(1,2,1), 0.1f);

        }
        yield return new WaitForSeconds(1.5f);
        isLaser = true;
        

        


    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {
        if(isLaser == true)
        {
            if(time < 3)
            {
                time += Time.deltaTime;

                _boss.parentObj.Rotate(Vector3.left * 50 * Time.deltaTime);
            }
            else
            {
                isLaser = false;
                time = 0;

            }
        }
        //if (isPattern == false)

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