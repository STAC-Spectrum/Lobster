using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : MonoBehaviour
{

    public Dictionary<BossStateEnum, BossState> stateDictionary = new Dictionary<BossStateEnum, BossState>();
    public BossState CurrentState { get; private set; }

    private void Awake()
    {
        


    }

    public void Initialize(BossStateEnum stateEnum)
    {

    }

    public void ChangeState(BossStateEnum stateEnum)
    {

    }

    public void AddState(BossStateEnum stateEnum, BossState state)
    {
        stateDictionary.Add(stateEnum, state);
    }

}
