using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class CrystalBoss : MonoBehaviour
{

    [Header("Setting")]
    public float laserPatternDistance;
    public Vector3 paillPatternSize;
    public LayerMask plyaerMask;

    [Header("AttackSetting")]
    public List<GameObject> PrefabList  = new List<GameObject>();

    public CrystalBossStateMachine StateMachine { get; set; }

    public Animator AnimatorCompo { get; set; }

    private Collider[] detectionCollider;
    [HideInInspector] public Transform target;
    [HideInInspector] public Transform parentObj;





    private void Awake()
    {
        StateMachine = new CrystalBossStateMachine();
        Transform visual = transform.Find("Visual");
        AnimatorCompo = visual.GetComponent<Animator>();
        //parentObj = transform.Find("LaserParent");
        StateMachine.AddState(CrystalBossStateEnum.Idle, new CrystalBossIdleState(this,StateMachine,"Idle"));
        StateMachine.AddState(CrystalBossStateEnum.Laser, new CrystalBossLaserPatternState(this,StateMachine,"Laser"));
        StateMachine.AddState(CrystalBossStateEnum.PillarAttack, new CrytalBossPillarPatternState(this,StateMachine,"Pillar"));
    }
    private void Start()
    {
        StateMachine.Initialize(CrystalBossStateEnum.Idle,this);

    }

    private void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public virtual Collider IsPlayerSphereDetection()
    {

        detectionCollider = Physics.OverlapSphere(transform.position, laserPatternDistance, plyaerMask);

        return detectionCollider.Length >= 1 ? detectionCollider[0] : null;
        
    }

    public virtual Collider IsPlayerCubeDetection(Vector3 boxSize)
    {

        detectionCollider = Physics.OverlapBox(transform.position, boxSize, Quaternion.identity,plyaerMask);

        return detectionCollider.Length >= 1 ? detectionCollider[0] : null;

    }

    public void PrefabSpawn(GameObject prefab,string parent,int count,CrystalBossState state)
    {
        for (int i =0;i<count;++i)
        {
            parentObj = transform.Find(parent);
            GameObject obj = Instantiate(prefab, transform);
            obj.transform.parent = parentObj.transform;
            obj.transform.localScale = new Vector3(0.1f,2,0.1f);
            obj.SetActive(false);
            state.prefabList.Add(obj);
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, laserPatternDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(50,1,100));
    }


}
