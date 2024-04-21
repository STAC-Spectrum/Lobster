using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Boss : MonoBehaviour
{

    [Header("Setting")]
    public float runDistance;
    public LayerMask plyaerMask;

    [Header("AttackSetting")]
    public float attackCoolTIme;
    public float attackDamage;
    public float attackDistance;

    public BossStateMachine StateMachine { get; set; }

    public Animator AnimatorCompo { get; set; }

    private Collider[] detectionCollider;
    public  IMovement MovemetCompo { get; set; }
    [HideInInspector] public Transform target;
    

    


    private void Awake()
    {
        StateMachine = new BossStateMachine();
        MovemetCompo = GetComponent<IMovement>();
        MovemetCompo.Initialize(this);
        Transform visual = transform.Find("Visual");
        AnimatorCompo = visual.GetComponent<Animator>();
        StateMachine.AddState(BossStateEnum.Walk, new BossWalkState(this,StateMachine,"Walk"));
        StateMachine.AddState(BossStateEnum.Idle, new BossIdleState(this,StateMachine,"Idle"));
    }
    private void Start()
    {
        StateMachine.Initialize(BossStateEnum.Idle,this);

    }

    private void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public virtual Collider IsPlayerDetection()
    {

        detectionCollider = Physics.OverlapSphere(transform.position, runDistance, plyaerMask);

        return detectionCollider.Length >= 1 ? detectionCollider[0] : null;

    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, runDistance);
    }

    //private void OnDrawGizmosSelected()
    //{

    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position, runDistance);

    //}

}
