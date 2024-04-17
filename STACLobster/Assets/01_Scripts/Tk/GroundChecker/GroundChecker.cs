using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsGround;
    private float rot = 0f;

    public bool CheckGround()
    {
        Ray ray = new Ray(transform.position, Quaternion.Euler(0, rot, -45f) * (transform.right));
        if (Physics.Raycast(ray, 100f, _whatIsGround))
        {
            Debug.DrawRay(transform.position, Quaternion.Euler(0, rot, -45f)
                * (transform.right) * 100f, Color.red);
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, Quaternion.Euler(0, rot, -45f)
                * (transform.right) * 100f, Color.green);
            rot += 180f;
            return false;
        }
    }
}
