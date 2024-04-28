using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyState
{
    //private int _deadLayer = LayerMask.NameToLayer("DeadLayer"); 
    public EnemyDeadState(Enemy enemy, EnemyStateMachine stateMachine, string animName) : base(enemy, stateMachine, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //_enemy.gameObject.layer = _deadLayer;
        GameObject.Destroy(_enemy.gameObject, 5f);
    }
}
