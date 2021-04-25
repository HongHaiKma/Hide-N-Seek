using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using DG.Tweening;

[DefaultExecutionOrder(-94)]
public class GameManager : Singleton<GameManager>
{
    public MapType m_MapType;

    private bool IsChanging;
    public string m_NextScene;
    public bool m_LevelStart = false;
    public bool m_LevelPause = false;

    public BigNumber m_GoldLevel;

    private PanelInGame m_PanelInGame;

    public int m_LoseStreak;

    [Header("CoolDown")]
    private CoolDown m_DailyNoti;
    private string m_DailyNotiContent = "Let's escape and get new character!!!";
    private double m_DailyNotiTimeMax;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        m_LoseStreak = 0;

        NotificationManager.Instance.m_NotiDict.Add(CdType.NOTI_DAILY, m_DailyNotiContent);
        m_DailyNotiTimeMax = 60 * 60 * 24;
        m_DailyNoti = new CoolDown(ConfigKeys.m_DailyNotiTimeout, ConfigKeys.m_DailyNotiRunning, ConfigKeys.m_DailyNotiCdTime, ConfigKeys.m_DailyNotiCdTimeMax, m_DailyNotiTimeMax, CdType.NOTI_DAILY, true);
        m_DailyNoti.Awake();
    }

    private void Update()
    {
        if (m_DailyNoti != null)
        {
            m_DailyNoti.OnUpdate();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            // GUIManager.Instance.g_IngameLoading.GetComponent<Animator>().SetTrigger("LoadingOut");
            Helper.DebugLog("TotalGoldChar: " + ProfileManager.GetTotalGoldChar());
            Helper.DebugLog("TotalGoldChar Owned: " + ProfileManager.GetTotaOwnedlGoldChar());
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (m_DailyNoti != null)
        {
            if (focus)
            {
                m_DailyNoti.Awake();
            }
            else
            {
                m_DailyNoti.Save();
            }
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (m_DailyNoti != null)
        {
            m_DailyNoti.Save();
        }
    }

    private void OnApplicationQuit()
    {
        if (m_DailyNoti != null)
        {
            m_DailyNoti.Save();
        }
    }

    private void OnEnable()
    {
        StartListenToEvent();
    }

    private void OnDisable()
    {
        StopListenToEvent();
    }

    public void StartListenToEvent()
    {
        EventManagerWithParam<bool>.AddListener(GameEvent.LEVEL_PAUSE, PauseLevel);
        EventManager.AddListener(GameEvent.LEVEL_START, ResetGoldLevel);
        EventManager.AddListener(GameEvent.CHAR_WIN, SaveGoldLevel);
        EventManagerWithParam<BigNumber>.AddListener(GameEvent.CLAIM_GOLD_IN_GAME, SetGoldLevel);

        EventManager2.StartListening(ConfigKeys.m_DailyNotiTimeout, SetNotiDailyTimeout);
        EventManager2.StartListening(ConfigKeys.m_DailyNotiRunning, SetNotiDailyRunning);
    }

    public void StopListenToEvent()
    {
        EventManagerWithParam<bool>.RemoveListener(GameEvent.LEVEL_PAUSE, PauseLevel);
        EventManager.RemoveListener(GameEvent.LEVEL_START, ResetGoldLevel);
        EventManager.RemoveListener(GameEvent.CHAR_WIN, SaveGoldLevel);
        EventManagerWithParam<BigNumber>.RemoveListener(GameEvent.CLAIM_GOLD_IN_GAME, SetGoldLevel);

        EventManager2.StopListening(ConfigKeys.m_DailyNotiTimeout, SetNotiDailyTimeout);
        EventManager2.StopListening(ConfigKeys.m_DailyNotiRunning, SetNotiDailyRunning);
    }

    public void SetNotiDailyTimeout()
    {
        m_DailyNoti.BeginCoolDown();
    }

    public void SetNotiDailyRunning()
    {

    }

    public void PauseLevel(bool _pause)
    {
        if (_pause)
        {
            Time.timeScale = 0f;
            m_LevelPause = true;
        }
        else
        {
            Time.timeScale = 1f;
            m_LevelPause = false;
        }
    }

    public void ResetGoldLevel()
    {
        m_GoldLevel = 0;
    }

    public void SaveGoldLevel()
    {
        BigNumber bonusLevelGold = InGameObjectsManager.Instance.m_Map.m_LevelGold;
        m_GoldLevel += bonusLevelGold;
        ProfileManager.AddGold(m_GoldLevel);
    }

    public void SetGoldLevel(BigNumber _value)
    {
        m_GoldLevel += _value;
        // m_PanelInGame.txt_GoldLevel.text = m_GoldLevel.ToString();

        RectTransform rect = m_PanelInGame.txt_GoldLevel.GetComponent<RectTransform>();

        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(rect.DOScale(new Vector3(2f, 2f, 2f), 0.25f));
        Tween tween = rect.DOScale(new Vector3(1f, 1f, 1f), 0.25f).OnPlay
        (
            () => m_PanelInGame.txt_GoldLevel.text = m_GoldLevel.ToString()
        );
        mySequence.Append(tween);
    }

    public void FindPanelInGame()
    {
        m_PanelInGame = FindObjectOfType<PanelInGame>().GetComponent<PanelInGame>();
    }

    public PanelInGame GetPanelInGame()
    {
        return m_PanelInGame;
    }

    public void ChangeToPlayScene(UnityAction _callback = null)
    {
        // Debug.Log("PlayScene");
        ChangeScene("PlayScene", () =>
        {
            if (_callback != null)
            {
                _callback();
                Helper.DebugLog("Callback change scene called!!!!");
            }
        });
        //SpineTextureManager.Instance.LoadBackgroundMaterialByName(1);
    }

    public void ChangeScene(string name, UnityAction _callback = null)
    {
        if (IsChanging) return;
        IsChanging = true;
        m_NextScene = name;
        // Time.timeScale = 1;
        // m_ChangeSceneCallback = _changeSceneCallback;
        // IngameEntityManager.Instance.ClearMap();
        // GUIManager.Instance.ClearAllOpenedPanelList();
        // GUIManager.Instance.ClearAllOpenedPopupList();
        StartCoroutine(OnChangingScene(_callback));
    }

    public IEnumerator OnChangingScene(UnityAction _callback = null)
    {
        GUIManager.Instance.g_IngameLoading.GetComponent<Animator>().SetTrigger("LoadingIn");

        yield return Yielders.Get(0.1f);

        AsyncOperation async = SceneManager.LoadSceneAsync(m_NextScene, LoadSceneMode.Single);

        async.allowSceneActivation = false;
        yield return Yielders.EndOfFrame;
        while (async.progress < 0.9f)
        {
            yield return Yielders.EndOfFrame;
        }
        SimplePool.Release();
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
        yield return Yielders.Get(0.1f);
        async.allowSceneActivation = true;
        IsChanging = false;

        yield return Yielders.Get(0f);
        // GUIManager.Instance.g_IngameLoading.GetComponent<Animator>().SetTrigger("LoadingOut");

        yield return Yielders.EndOfFrame;

        InGameObjectsManager.Instance.LoadMap();
        // GUIManager.Instance.GetGOPanelLoading().SetActive(false);
        CamController.Instance.m_Char = InGameObjectsManager.Instance.m_Char;
        FindPanelInGame();
        GUIManager.Instance.FindPanelLoadingAds();
        GUIManager.Instance.g_IngameLoading.GetComponent<Animator>().SetTrigger("LoadingOut");
        GUIManager.Instance.GetGOPanelLoading().SetActive(false);

        // if (m_LevelStart)
        // {
        //     Helper.DebugLog("Level is startttttttttttttttt");
        //     EventManager.CallEvent(GameEvent.LEVEL_START);
        //     GameManager.Instance.GetPanelInGame().g_Joystick.SetActive(true);
        //     GameManager.Instance.GetPanelInGame().SetIngame();
        //     GameManager.Instance.m_LevelStart = true;
        //     CamController.Instance.m_StartFollow = true;

        //     EventManagerWithParam<bool>.CallEvent(GameEvent.LEVEL_PAUSE, false);
        // }

        _callback();

        QualitySettings.vSyncCount = 0;

        Helper.DebugLog("On Changing Scene completed!!!");
        Helper.DebugLog("Time scale = " + Time.timeScale);
    }

    public void SetSoundState(int value)
    {
        PlayerPrefs.SetInt("Sound", value);
        // EventManager.CallEvent("MusicChange");
        EventManager.CallEvent(GameEvent.SOUND_CHANGE);
    }
    public void SetMusicState(int value)
    {
        PlayerPrefs.SetInt("Music", value);
        // EventManager.CallEvent("MusicChange");
        EventManager.CallEvent(GameEvent.MUSIC_CHANGE);
    }
    public int GetSoundState()
    {
        return PlayerPrefs.GetInt("Sound", 1);
    }
    public int GetMusicState()
    {
        return PlayerPrefs.GetInt("Music", 1);
    }


    public void SetDailyNotiTimeout()
    {

    }

    public void SetDailyNotiRunning()
    {

    }
}
