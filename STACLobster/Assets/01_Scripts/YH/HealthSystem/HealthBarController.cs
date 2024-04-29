﻿/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private GameObject[] _heartContainers;
    private Image[] _heartFills;
    private Material[] _heartMatrial;

    [SerializeField] private Transform heartsParent;
    [SerializeField] private GameObject heartContainerPrefab;
    [SerializeField] private Shader _shader;

    private float currentHealth;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
    }


    private void Start()
    {
        // Should I use lists? Maybe :)
        _heartContainers = new GameObject[(int)PlayerStats.Instance.MaxTotalHealth];
        _heartFills = new Image[(int)PlayerStats.Instance.MaxTotalHealth];
        

        PlayerStats.Instance.onHealthChangedCallback += UpdateHeartsHUD;
        InstantiateHeartContainers();
        UpdateHeartsHUD();
    }

    private void Update()
    {
        Test();
    }

    public void UpdateHeartsHUD()
    {
        SetHeartContainers();
        SetFilledHearts();
    }

    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            PlayerStats.Instance.TakeDamage(1);
        if (Input.GetKeyDown(KeyCode.W))
            PlayerStats.Instance.Heal(1);
        if (Input.GetKeyDown(KeyCode.E))
            PlayerStats.Instance.AddHealth();
    }

    private void SetHeartContainers()
    {
        for (int i = 0; i < _heartContainers.Length; i++)
        {
            if (i < PlayerStats.Instance.MaxHealth)
            { 
                _heartContainers[i].SetActive(true);
            }
            else
            {
                _heartContainers[i].SetActive(false);
            }
        }
    }

    private void SetFilledHearts()
    {
        for (int i = 0; i < _heartFills.Length; i++)
        {
            if (i == PlayerStats.Instance.Health)
            {
                if (PlayerStats.Instance.Health - currentHealth == -1)
                {
                    StartCoroutine(HeartUnFill(_heartFills[i]));
                }
                else if (PlayerStats.Instance.Health - currentHealth == 1)
                {
                    StartCoroutine(HeartFill(_heartFills[i - 1]));
                }
            }
        }
    }
    private IEnumerator HeartFill(Image i)
    {
        float currentTime = 0;
        
        while (currentTime <= 1f)
        {
            currentTime += Time.deltaTime;
            float currentFill = Mathf.Lerp(1, 0, currentTime);
            i.material.SetFloat("_StepValue", currentFill);
            yield return null;
        }
        currentHealth++;
    }
    private IEnumerator HeartUnFill(Image i)
    {
        float currentTime = 0;

        while (currentTime <= 1f)
        {
            currentTime += Time.deltaTime;
            float currentFill = Mathf.Lerp(0, 1, currentTime);
            i.material.SetFloat("_StepValue", currentFill);
            yield return null;
        }
        currentHealth--;
    }

    private void InstantiateHeartContainers()
    {
        for (int i = 0; i < PlayerStats.Instance.MaxTotalHealth; i++)
        {
            GameObject temp = Instantiate(heartContainerPrefab);
            temp.transform.SetParent(heartsParent, false);
            _heartContainers[i] = temp;
            _heartFills[i] = temp.transform.Find("HP").GetComponent<Image>();
            _heartFills[i].material = new Material(_shader);
            _heartFills[i].material.SetFloat("_StepValue", 0);
        }
    }
}
