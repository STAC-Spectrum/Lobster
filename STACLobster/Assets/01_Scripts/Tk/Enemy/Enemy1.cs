using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{
    public override void Awake()
    {
        base.Awake();

        StateMachine.Initialize(EnemyStateEnum.Idle, this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,
            _chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,
            _attackRange);
    }
}
