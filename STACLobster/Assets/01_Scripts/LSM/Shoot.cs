using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Header("¼¼ÆÃ")]
    public float laserDistance;
    public int numberReflection;
    public Transform firePos;
    public LayerMask layerMask;
    
    private RaycastHit _hit;
    private LineRenderer _lineRenderer;

    private Ray _ray;
    private Vector3 _direction;

    private void Awake()
    {
        _lineRenderer = GetComponentInChildren<LineRenderer>();
    }


    private void Update()
    {
        ReflectLaser();
    }

    public void ReflectLaser()
    {

        _ray = new Ray(firePos.position, firePos.forward);

        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, firePos.position);

        float remainLength = laserDistance;

        for(int i = 0; i < numberReflection; ++i)
        {
            if (Physics.Raycast(_ray.origin, _ray.direction, out _hit, remainLength, layerMask))
            {
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _hit.point);
                remainLength -= Vector3.Distance(_ray.direction, _hit.normal);
                _ray = new Ray(_hit.point, Vector3.Reflect(_ray.direction, _hit.normal));

            }
            else
            {
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _ray.origin + (_ray.direction * remainLength));
            }
        }

        
    }

    public void NormalLaser()
    {
        _lineRenderer.SetPosition(0, firePos.position);
        //Debug.DrawRay(firePos.position, firePos.forward*lightDistance, Color.red);
        if (
            Physics.Raycast(firePos.position, firePos.forward, out _hit, laserDistance, layerMask))
        {
            _lineRenderer.SetPosition(1, _hit.point);

        }
        else
        {
            _lineRenderer.SetPosition(1, firePos.position + (transform.forward * laserDistance));
        }
    }





}
