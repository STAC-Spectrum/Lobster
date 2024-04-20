using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : MonoBehaviour
{

    public Dictionary<BossStateEnum, BossState> stateDictionary = new Dictionary<BossStateEnum, BossState>();
    public BossState CurrentState { get; private set; }
    private Boss _boss;

    private void Awake()
    {
        


    }

    public void Initialize(BossStateEnum stateEnum, Boss boss)
    {

        _boss = boss;
        CurrentState = stateDictionary[stateEnum];
        CurrentState.Enter();

    }

    public void ChangeState(BossStateEnum stateEnum)
    {
        CurrentState.Exit();
        CurrentState = stateDictionary[stateEnum];
        CurrentState.Enter();
    }

    public void AddState(BossStateEnum stateEnum, BossState state)
    {
        stateDictionary.Add(stateEnum, state);
    }

}
