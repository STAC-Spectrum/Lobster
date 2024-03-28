using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Header("세팅")]
    public float laserDistance;
    public int numberReflection;
    public Transform firePos;
    public LayerMask layerMask;
    
    [Header("세팅:다음레이저로가는 시간, 잔상 시간")]
    [SerializeField] private float laserDealayTime;
    [SerializeField] private float laserAfterimagetiime;

    private RaycastHit _hit;
    private LineRenderer _lineRenderer;

    private Ray _ray;
    private Vector3 _direction;
    private bool _isOnShoot;

    private void Awake()
    {
        _lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    private void Start()
    {
        StartCoroutine(ReflectLaser());
    }

    private void Update()
    {
        //ReflectLaser_1();
        
    }

    public void ReflectLaser_1()
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


    public IEnumerator ReflectLaser()
    {

        while (true)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) && _isOnShoot==false);
            _isOnShoot = true;
            _ray = new Ray(firePos.position, firePos.forward);

            _lineRenderer.positionCount = 1;
            _lineRenderer.SetPosition(0, firePos.position);

            float remainLength = laserDistance;

            for (int i = 0; i < numberReflection; ++i)
            {
                yield return new WaitForSeconds(laserDealayTime);
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
            yield return new WaitForSeconds(laserAfterimagetiime);
            _lineRenderer.positionCount = 1;
            _lineRenderer.SetPosition(0, firePos.position);
            _isOnShoot = false;
        }
        

    }



}
