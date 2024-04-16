using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{

    public float moveSpeed;

    private Collider[] playerCollider;
    Rigidbody rigid;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        playerCollider = Physics.OverlapSphere(transform.position,10,1 << LayerMask.NameToLayer("Player"));
        if (playerCollider.Length > 0)
        {
            //for(int i = 0; i < playerCollider.Length; ++i)
            //{
            //    //if(collider[i].gameObject.CompareTag("Player"))
            //}
            Vector3 direction= playerCollider[0].transform.position - transform.position;
            rigid.velocity = direction.normalized * moveSpeed;

        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 10);
    }

}
