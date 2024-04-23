using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class CrystalBoss : MonoBehaviour
{

    [Header("Setting")]
    public float runDistance;
    public LayerMask plyaerMask;

    [Header("AttackSetting")]
    public GameObject LaserPrefab;

    public CrystalBossStateMachine StateMachine { get; set; }

    public Animator AnimatorCompo { get; set; }

    private Collider[] detectionCollider;
    public List<GameObject> prefabList = new List<GameObject>();
    [HideInInspector] public Transform target;
    [HideInInspector] public Transform parentObj;





    private void Awake()
    {
        StateMachine = new CrystalBossStateMachine();
        Transform visual = transform.Find("Visual");
        AnimatorCompo = visual.GetComponent<Animator>();
        parentObj = transform.Find("LaserParent");
        StateMachine.AddState(CrystalBossStateEnum.Idle, new CrystalBossIdleState(this,StateMachine,"Idle"));
        StateMachine.AddState(CrystalBossStateEnum.Laser, new CrystalBossLaserPatternState(this,StateMachine,"Laser"));
    }
    private void Start()
    {
        StateMachine.Initialize(CrystalBossStateEnum.Idle,this);

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

    public void LaserSpawn(GameObject prefab,int count)
    {
        for (int i =0;i<count;++i)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.transform.parent = parentObj;
            obj.transform.localScale = Vector3.one * 0.1f;
            obj.SetActive(false);
            prefabList.Add(obj);
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, runDistance);
    }


}
