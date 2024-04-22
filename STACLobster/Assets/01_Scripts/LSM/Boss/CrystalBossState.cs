using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CrystalBossState
{

    protected CrystalBoss _boss;
    protected CrystalBossStateMachine _stateMachine;
    protected int _animationName;
    protected bool isPattern;
    protected Sequence mySequence = DOTween.Sequence();
    public CrystalBossState(CrystalBoss boss,CrystalBossStateMachine bossStateMachine,string animationName)
    {
        _boss = boss;
        _stateMachine = bossStateMachine;
        _animationName = Animator.StringToHash( animationName);
    }

    public virtual void Enter()
    {

        //_boss.AnimatorCompo.SetBool(_animationName, true);

    }

    public virtual void Exit()
    {
        //_boss.AnimatorCompo.SetBool(_animationName, false);
    }

    public virtual void UpdateState()
    {

    }

}
