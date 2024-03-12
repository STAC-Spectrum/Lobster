using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpawnParticle : MonoBehaviour
{
    public VisualEffect visualEffect;
    private Animator animator;

    // Update is called once per frame

    private void Awake()
    {
        animator = GetComponent<Animator>();    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Spawn");
            visualEffect.Play();
        }
    }
}
