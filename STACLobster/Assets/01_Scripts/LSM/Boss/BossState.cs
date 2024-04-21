using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState
{

    protected Boss _boss;
    protected BossStateMachine _stateMachine;
    protected int _animationName;

    public BossState(Boss boss,BossStateMachine bossStateMachine,string animationName)
    {
        _boss = boss;
        _stateMachine = bossStateMachine;
        _animationName = Animator.StringToHash( animationName);
    }

    public virtual void Enter()
    {

        _boss.AnimatorCompo.SetBool(_animationName, true);

    }

    public virtual void Exit()
    {
        _boss.AnimatorCompo.SetBool(_animationName, false);
    }

    public virtual void UpdateState()
    {

    }

}
