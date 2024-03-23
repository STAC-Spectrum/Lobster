using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            _instance ??= FindObjectOfType<T>();
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        _instance ??= this as T;
        DontDestroyOnLoad(gameObject);
    }
}
