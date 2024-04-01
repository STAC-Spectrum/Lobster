using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private float randomDirX;
    private float randomDirZ;
    private float currentTime;
    private float lastChangeDirTime;

    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, string animName) : base(enemy, stateMachine, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        ChangeDirection();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.ChaseRangeCast() != Vector3.zero)
        {
            _stateMachine.ChangeState(EnemyStateEnum.Chase);
        }

        Move();

        currentTime = Time.time;

        if(currentTime > _enemy.changeDirTime + lastChangeDirTime)
        {
            lastChangeDirTime = Time.time;
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        randomDirX = Random.Range(Random.Range(-1f, -2.5f), Random.Range(1f, 2.5f));
        randomDirZ = Random.Range(Random.Range(-1f, -2.5f), Random.Range(1f, 2.5f));

        _enemy.randomDirection = new Vector2(randomDirX, randomDirZ);
    }

    private void Move()
    {
        _enemy.Rigid.velocity = new Vector3(
            _enemy.randomDirection.x, 0, _enemy.randomDirection.y);
    }
}
