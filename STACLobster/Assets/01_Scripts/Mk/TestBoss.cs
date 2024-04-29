using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoss : MonoBehaviour
{
    [SerializeField] private LayerMask _player;

    void Update()
    {
        RaycastHit[] hits = new RaycastHit[3];
        int player = Physics.SphereCastNonAlloc(transform.position, 10f, Vector3.right, hits, _player);

        if (player >= 1)
        {
            gameObject.SetActive(true);
        }
    }
}
