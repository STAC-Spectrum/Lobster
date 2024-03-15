using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    #region Component

    public AgentMovement movementCompo { get; protected set; }

    #endregion

    protected void Awake()
    {
        movementCompo = GetComponent<AgentMovement>();
    }

    public Coroutine StartDelayCallback(float time, Action callback)
    {
        return StartCoroutine(DelayCoroutine(time, callback));
    }

    IEnumerator DelayCoroutine(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }
}
