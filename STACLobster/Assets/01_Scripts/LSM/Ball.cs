using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public Vector3 direction;
    public float speed;
    private Collider sphereCollider;


    private void Awake()
    {
        sphereCollider = GetComponentInChildren<SphereCollider>();
    }

    private void Start()
    {
        transform.forward = direction.normalized;
    }

    private void Update()
    {
        //transform.position += Vector3.forward;
        transform.position += transform.forward * speed * Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
  
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            direction = Vector3.Reflect(direction, collision.GetContact(0).normal);
            transform.forward = direction.normalized;
        }
    }
}
