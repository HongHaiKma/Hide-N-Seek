using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource m_IngameShootingFx;
    public AudioSource m_BGM;

    public AudioClip m_ButtonClick;
    public AudioClip m_SoundGetGold;
    public AudioClip m_SoundObstacleDynamic;
    public AudioClip m_SoundBuySuccess;
    public AudioClip m_SoundWin;
    public AudioClip m_SoundWinLong;
    public AudioClip m_SoundLose;
    public AudioClip m_ClipBGMInGame;

    bool IsSoundOn
    {
        get
        {
            return GameManager.Instance.GetSoundState() == 1;
        }
    }

    bool IsMusicOn
    {
        get
        {
            return GameManager.Instance.GetMusicState() == 1;
        }
    }

    private void Awake()
    {
        m_BGM.Pause();
    }
    private void Start()
    {
        m_BGM.Pause();
    }

    private void OnEnable()
    {
        OnSoundChange();
        OnMusicChange();
        PlayBGM();


        StartListenToEvent();
    }

    private void OnDisable()
    {
        StopListenToEvent();
    }

    public void StartListenToEvent()
    {
        EventManager.AddListener(GameEvent.SOUND_CHANGE, OnSoundChange);
        EventManager.AddListener(GameEvent.MUSIC_CHANGE, OnMusicChange);
        EventManager.AddListener(GameEvent.CHAR_WIN, OnSoundWin);
        EventManager.AddListener(GameEvent.CHAR_SPOTTED, OnSoundLose);
    }

    public void StopListenToEvent()
    {
        EventManager.RemoveListener(GameEvent.SOUND_CHANGE, OnSoundChange);
        EventManager.RemoveListener(GameEvent.MUSIC_CHANGE, OnMusicChange);
        EventManager.RemoveListener(GameEvent.CHAR_WIN, OnSoundWin);
        EventManager.RemoveListener(GameEvent.CHAR_SPOTTED, OnSoundLose);
    }

    public void OnSoundChange()
    {
        if (IsSoundOn)
        {
            m_IngameShootingFx.volume = 0.3f;
        }
        else
        {
            m_IngameShootingFx.volume = 0f;
        }
    }

    public void OnMusicChange()
    {
        if (IsMusicOn)
        {
            m_BGM.volume = 0.6f;
        }
        else
        {
            m_BGM.volume = 0f;
        }
    }

    public void PlayBGM()
    {
        // m_BGM.clip = m_ClipBGMInGame;
        m_BGM.Play();
    }

    public void PlayButtonClick(Vector3 pos)
    {
        // if (IsSoundOn && !IsLockSound)
        if (IsSoundOn)
        {
            m_IngameShootingFx.PlayOneShot(m_ButtonClick, 1);
        }
    }

    public void PlaySoundGetGold(Vector3 pos)
    {
        // if (IsSoundOn && !IsLockSound)
        if (IsSoundOn)
        {
            m_IngameShootingFx.PlayOneShot(m_SoundGetGold, 5);
        }
    }

    public void PlaySoundObstacleDynamic(Vector3 pos)
    {
        // if (IsSoundOn && !IsLockSound)
        if (IsSoundOn)
        {
            m_IngameShootingFx.PlayOneShot(m_SoundObstacleDynamic, 2);
        }
    }

    public void PlaySoundBuySuccess()
    {
        // if (IsSoundOn && !IsLockSound)
        if (IsSoundOn)
        {
            m_IngameShootingFx.PlayOneShot(m_SoundBuySuccess, 5);
        }
    }

    public void PlaySoundWin()
    {
        // if (IsSoundOn && !IsLockSound)
        if (IsSoundOn)
        {
            m_IngameShootingFx.PlayOneShot(m_SoundWin, 5);
        }
    }

    public void PlaySoundWinLong(bool _stop)
    {
        if (_stop)
        {
            m_IngameShootingFx.Stop();
            return;
        }

        if (IsSoundOn)
        {
            m_IngameShootingFx.PlayOneShot(m_SoundWinLong, 5);
        }
    }

    public void PlaySoundLose()
    {
        // if (IsSoundOn && !IsLockSound)
        if (IsSoundOn)
        {
            m_IngameShootingFx.PlayOneShot(m_SoundLose, 5);
        }
    }


    public void OnSoundWin()
    {
        m_BGM.Pause();
        PlaySoundWin();
    }

    public void OnSoundLose()
    {
        m_BGM.Pause();
        PlaySoundLose();
    }
}
