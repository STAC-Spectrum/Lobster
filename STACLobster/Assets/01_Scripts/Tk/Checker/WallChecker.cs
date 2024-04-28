using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsWall;
    [SerializeField] private float checkDistance;

    public bool CheckWall()
    {
        Ray ray = new Ray(transform.position, transform.right);
        Debug.DrawRay(transform.position, transform.right, Color.red);
        if (Physics.Raycast(ray, checkDistance, _whatIsWall))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right * checkDistance);
    }
}
