using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState
{

    protected Boss _boss;
    protected BossStateMachine _stateMachine;
    protected string _animationName;

    public BossState(Boss boss,BossStateMachine bossStateMachine,string animationName)
    {
        _boss = boss;
        _stateMachine = bossStateMachine;
        _animationName = animationName;
    }

    public virtual void Enter()
    {

        //_stateMachine

    }

    public virtual void Exit()
    {

    }

    public virtual void UpdateState()
    {

    }

}
