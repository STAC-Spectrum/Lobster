using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBossStateMachine : MonoBehaviour
{

    public Dictionary<CrystalBossStateEnum, CrystalBossState> stateDictionary = 
        new Dictionary<CrystalBossStateEnum, CrystalBossState>();

    public CrystalBossState CurrentState { get; private set; }
    public List<CrystalBossState> SkillList = new List<CrystalBossState>();
    private CrystalBoss _boss;

    private void Awake()
    {
        
    }

    public void Initialize(CrystalBossStateEnum stateEnum, CrystalBoss boss)
    {

        _boss = boss;
        CurrentState = stateDictionary[stateEnum];
        CurrentState.Enter();

    }

    public void ChangeState(CrystalBossStateEnum stateEnum)
    {
        CurrentState.Exit();
        CurrentState = stateDictionary[stateEnum];
        CurrentState.Enter();
    }

    public void AddState(CrystalBossStateEnum stateEnum, CrystalBossState state)
    {
        stateDictionary.Add(stateEnum, state);
    }

}
