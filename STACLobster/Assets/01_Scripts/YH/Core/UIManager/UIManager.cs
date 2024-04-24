using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private Image _pausePanel;
    [SerializeField] private Image _keySettingPanel;
    [SerializeField] private Image _soundPanel;
    [SerializeField] private Button _continueBtn;
    [SerializeField] private Button _keySettingBtn;
    [SerializeField] private Button _audioBtn;
    [SerializeField] private Button _quitBtn;

    protected override void Awake()
    {
        base.Awake();
        _continueBtn.onClick.AddListener(GameContinue);
        _keySettingBtn.onClick.AddListener(KeySettingOn);
        _audioBtn.onClick.AddListener(AudioSettingOn);
        _quitBtn.onClick.AddListener(QuitSettingOn);
    }

    

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (_keySettingPanel.gameObject.activeSelf)
            {
                _keySettingPanel.gameObject.SetActive(false);
            }
            else if(_soundPanel.gameObject.activeSelf)
            {
                _soundPanel.gameObject.SetActive(false);
            }
            else if (_pausePanel.gameObject.activeSelf)
            {
                GameContinue();
            }
            else
            {
                PauseOn();
            }
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
        _keySettingPanel.gameObject.SetActive(true);
    }
    private void AudioSettingOn()
    {
        _soundPanel.gameObject.SetActive(true);
    }
    private void QuitSettingOn()
    {
        Application.Quit();
    }
}
