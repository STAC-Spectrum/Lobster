using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private Image _pausePanel;
    [SerializeField] private Image _;
    [SerializeField] private Button _continueBtn;
    [SerializeField] private Button _keySettingBtn;
    [SerializeField] private Button _audioBtn;
    [SerializeField] private Button _quitBtn;

    private void Awake()
    {
        _continueBtn.onClick.AddListener(GameContinue);
        //_keySettingBtn.onClick.AddListener();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseOn();
        }
    }

    private void GameContinue()
    {
        Time.timeScale = 1.0f;
        _pausePanel.gameObject.SetActive(false);
    }

    private void PauseOn()
    {
        _pausePanel.gameObject.SetActive(true);
        Time.timeScale = 0.1f;
    }

    private void KeySettingOn()
    {
        
    }
}
