using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWalkState : BossState
{
    public BossWalkState(Boss boss, BossStateMachine bossStateMachine, string animationName) : base(boss, bossStateMachine, animationName)
    {
    }
}
