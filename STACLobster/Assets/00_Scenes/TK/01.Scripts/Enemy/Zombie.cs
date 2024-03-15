using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    public override void Awake()
    {
        base.Awake();

        StateMachine.Initialize(EnemyStateEnum.Idle, this);
    }

    private void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void ChaseRangeCast()
    {
        base.ChaseRangeCast();
        RaycastHit[] playerInRange = Physics.SphereCastAll(transform.position, 
            attackRange / 2f, transform.forward);
        foreach(RaycastHit hit in playerInRange)
        {
            //if (hit.transform.TryGetComponent<Player>(out Player player))
            //{
            //    randomDirection = hit.point - transform.position;
            //    StateMachine.ChangeState(EnemyStateEnum.Chase);
            //}
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,
            attackRange / 2f);
    }
}
