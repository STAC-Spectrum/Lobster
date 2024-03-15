using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState CurrentState { get; private set; }
    public Dictionary<EnemyStateEnum, EnemyState> enemyStateDictionary;

    private Enemy _enemy;

    public EnemyStateMachine()
    {
        enemyStateDictionary = new Dictionary<EnemyStateEnum, EnemyState>();
    }

    public void Initialize(EnemyStateEnum stateEnum, Enemy enemy)
    {
        _enemy = enemy;
        CurrentState = enemyStateDictionary[stateEnum];
        CurrentState.Enter();
    }

    public void ChangeState(EnemyStateEnum newState)
    {
        if (_enemy.CanStateChangeable == false) return;

        CurrentState.Exit();
        CurrentState = enemyStateDictionary[newState];
        CurrentState.Enter();
    }

    public void AddState(EnemyStateEnum stateEnum, EnemyState state)
    {
        enemyStateDictionary.Add(stateEnum, state);
    }
}
