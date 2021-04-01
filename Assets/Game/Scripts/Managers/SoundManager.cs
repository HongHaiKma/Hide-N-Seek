using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource m_IngameShootingFx;
    public AudioSource m_BGM;

    public AudioClip m_ButtonClick;
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
    }

    public void StopListenToEvent()
    {
        EventManager.RemoveListener(GameEvent.SOUND_CHANGE, OnSoundChange);
        EventManager.RemoveListener(GameEvent.MUSIC_CHANGE, OnMusicChange);
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
            m_BGM.volume = 0.3f;
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
}
