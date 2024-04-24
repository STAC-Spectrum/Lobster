/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private GameObject[] heartContainers;
    private Image[] heartFills;

    [SerializeField] private Transform heartsParent;
    [SerializeField] private GameObject heartContainerPrefab;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
    }


    private void Start()
    {
        // Should I use lists? Maybe :)
        heartContainers = new GameObject[(int)PlayerStats.Instance.MaxTotalHealth];
        heartFills = new Image[(int)PlayerStats.Instance.MaxTotalHealth];

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
        for (int i = 0; i < heartContainers.Length; i++)
        {
            if (i < PlayerStats.Instance.MaxHealth)
            {
                heartContainers[i].SetActive(true);
            }
            else
            {
                heartContainers[i].SetActive(false);
            }
        }
    }

    private void SetFilledHearts()
    {
        for (int i = 0; i < heartFills.Length; i++)
        {
            if (i < PlayerStats.Instance.Health)
            {
                heartFills[i].fillAmount = 1;
            }
            else
            {
                heartFills[i].fillAmount = 0;
            }
        }

        if (PlayerStats.Instance.Health % 1 != 0)
        {
            int lastPos = Mathf.FloorToInt(PlayerStats.Instance.Health);
            heartFills[lastPos].fillAmount = PlayerStats.Instance.Health % 1;
        }
    }

    private void InstantiateHeartContainers()
    {
        for (int i = 0; i < PlayerStats.Instance.MaxTotalHealth; i++)
        {
            GameObject temp = Instantiate(heartContainerPrefab);
            temp.transform.SetParent(heartsParent, false);
            heartContainers[i] = temp;
            heartFills[i] = temp.transform.Find("HeartFill").GetComponent<Image>();
        }
    }
}
