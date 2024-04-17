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

    private void Update()
    {
        StateMachine.CurrentState.UpdateState();

        if (!checker.CheckGround())
        {
            moveSpeed *= -1;
        }

        ChaseRangeCast();
    }

    public override Vector3 ChaseRangeCast()
    {
        base.ChaseRangeCast();
        return base.ChaseRangeCast();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,
            attackRange);
    }
}
