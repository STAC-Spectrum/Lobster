using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Boss : MonoBehaviour
{

    [Header("Setting")]
    public float moveSpeed;
    public float runDistance;
    public LayerMask plyaerMask;

    [Header("AttackSetting")]
    public float attackCoolTIme;
    public float attackDamage;
    public float attackDistance;

    public BossStateMachine StateMachine { get; set; }

    private Collider[] detectionCollider;
    private Animator animator;

    private IMovement movemetnCompo;

    


    private void Awake()
    {
        movemetnCompo = GetComponent<IMovement>();
        movemetnCompo.Initialize(this);
        StateMachine.AddState(BossStateEnum.Walk, new BossWalkState(this,StateMachine,"Walk"));
        StateMachine.AddState(BossStateEnum.Idle, new BossWalkState(this,StateMachine,"Idle"));
    }
    private void Start()
    {
        StateMachine.Initialize(BossStateEnum.Idle,this);

    }

    public virtual Collider IsRangeDetection()
    {

        detectionCollider = Physics.OverlapSphere(transform.position, runDistance, plyaerMask);

        return detectionCollider.Length >= 1 ? detectionCollider[1] : null;

    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position,1f);
    }


}
