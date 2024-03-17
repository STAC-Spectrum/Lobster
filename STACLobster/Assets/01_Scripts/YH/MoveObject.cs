using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveObject : MonoBehaviour
{
    [SerializeField]private Transform endPoint;
    [SerializeField] private float time;

    private enum Mode { Linear,InOutQuad};
    private Mode mode = Mode.Linear;

    private void Start()
    {
        if (mode == Mode.Linear)
            transform.DOMove(endPoint.position, time).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        else
            transform.DOMove(endPoint.position, time).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
    }



}
