using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoSingleton<SoundManager>
{
    

    [Header("Music")]
    [SerializeField] private AudioSource _titleMusic;
    [SerializeField] private AudioSource _gameMusic;
    [SerializeField] private float _fadeTimerInterval = 0.001f;
    private bool _titleMusicPlaying = false;
    private bool _gameMusicPlaying = false;

    [Header("Impact")]
    [SerializeField] private AudioSource _attackImpactSource;
    [SerializeField] private AudioSource _shotImpactSource;

    protected override void Awake()
    {
        base.Awake();
    }

    //오디오 소스 재생

    public void StartTitleMusic()
    {
        if (!_titleMusicPlaying)
        {
            _titleMusic.Play();
            _gameMusic.Stop();
            _titleMusic.volume = 0.5f;
            _gameMusicPlaying = false;
            _titleMusicPlaying = true;
        }
    }
    public void StartGameMusic()
    {
        if (!_gameMusicPlaying)
        {
            _gameMusic.Play();
            _titleMusic.Stop();
            _gameMusic.volume = 0.5f;
            _titleMusicPlaying = false;
            _gameMusicPlaying = true;
        }
    }
    public void GamePaused(bool paused)
    {
        if (paused)
        {
            _gameMusic.Pause();
            _titleMusic.Play();
        }
        if (!paused)
        {
            _gameMusic.UnPause();
            _titleMusic.Stop();
        }
    }

    public void StartShotImpactSource()
    {
        _shotImpactSource.Play();
    }

    public void StartAttackImpactSoruce()
    {
        _attackImpactSource.Play();
    }

    public void StartFadeTitleOut(AudioSource music)
    {
        StartCoroutine(FadeTitleMusic(true,music));
    }

    public void StartFadeMusicIn(AudioSource music)
    {
        StartCoroutine(FadeTitleMusic(false,music));
    }

    private IEnumerator FadeTitleMusic(bool fadeOut, AudioSource music)
    {
        if (fadeOut)
        {
            while (music.volume > 0)
            {
                music.volume -= 0.001f;
                yield return new WaitForSeconds(_fadeTimerInterval);
            }
        }
        else if (!fadeOut)
        {
            while (music.volume < 0.5f)
            {
                music.volume += 0.001f;
                yield return new WaitForSeconds(_fadeTimerInterval);
            }
        }
    }
    //---------------------------------------------------------------------------------

    //볼륨 조정
    [Header("AudioMixer")]
    [SerializeField] private AudioMixer _soundMixer;

    [Header("Slider")]
    [SerializeField] private Slider _totalSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _effectSlider;

    private void Start()
    {
        LoadVolume();
        StartTitleMusic();
    }

    private void SetTotalValue(float volume)
    {
        _soundMixer.SetFloat("Total", volume);
        PlayerPrefs.SetFloat("TotalVolume", volume);
    }
    private void SetMusicValue(float volume)
    {
        _soundMixer.SetFloat("Music", volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    private void SetEffectValue(float volume)
    {
        _soundMixer.SetFloat("Music", volume);
        PlayerPrefs.SetFloat("EffectVolume", volume);
    }

    private void LoadVolume()
    {
        _totalSlider.value = PlayerPrefs.GetFloat("TotalVolume");
        _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        _effectSlider.value = PlayerPrefs.GetFloat("EffectVolume");

        _totalSlider.onValueChanged.AddListener(SetTotalValue);
        _musicSlider.onValueChanged.AddListener(SetMusicValue);
        _effectSlider.onValueChanged.AddListener(SetEffectValue);
    }
}



