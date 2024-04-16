using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Boss : MonoBehaviour
{
    
    public BossStateMachine StateMachine { get; private set; }

    private void Awake()
    {
        StateMachine.AddState(BossStateEnum.Walk, new BossWalkState(this,StateMachine,"Walk"));
        StateMachine.AddState(BossStateEnum.Idle, new BossWalkState(this,StateMachine,"Idle"));
    }

    private void Start()
    {
        StateMachine.Initialize(BossStateEnum.Idle);
    }

}
