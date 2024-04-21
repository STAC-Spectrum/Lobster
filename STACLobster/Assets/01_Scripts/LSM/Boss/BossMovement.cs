using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour,IMovement
{

    public float moveSpeed;

    private Rigidbody rigid;
    private Boss _boss;


    public void Initialize(Boss boss)
    {
        _boss = boss;
        rigid = GetComponent<Rigidbody>();
    }


    public void Move(Vector3 vec)
    {

        rigid.velocity = vec * moveSpeed;
    }

    public void Stop()
    {
        rigid.velocity = Vector3.zero;
    }


    //private void Update()
    //{
    //    public Collider = _boss.IsRangeDetection();

    //    //if (playerCollider.Length > 0)
    //    //{
    //    //    //for(int i = 0; i < playerCollider.Length; ++i)
    //    //    //{
    //    //    //    //if(collider[i].gameObject.CompareTag("Player"))
    //    //    //}
    //    //    Vector3 direction= playerCollider[0].transform.position - transform.position;
    //    //    rigid.velocity = direction.normalized * moveSpeed;

    //    //}


    //}



}
