using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpawnParticle : MonoBehaviour
{
    public VisualEffect visualEffect;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            visualEffect.Play();
        }
    }
}
