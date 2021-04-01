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

    private void Awake()
    {
        Application.targetFrameRate = 60;
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
    }

    public void StopListenToEvent()
    {
        EventManagerWithParam<bool>.RemoveListener(GameEvent.LEVEL_PAUSE, PauseLevel);
        EventManager.RemoveListener(GameEvent.LEVEL_START, ResetGoldLevel);
        EventManager.RemoveListener(GameEvent.CHAR_WIN, SaveGoldLevel);
        EventManagerWithParam<BigNumber>.RemoveListener(GameEvent.CLAIM_GOLD_IN_GAME, SetGoldLevel);
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
        Helper.DebugLog("Claim gold level: " + m_GoldLevel);
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

    public void ChangeToStartMenu()
    {
        // Debug.Log("PlayScene");
        ChangeScene("PlayScene", () =>
        {

            // InGameObjectsManager.Instance.LoadMap();
        });
        //SpineTextureManager.Instance.LoadBackgroundMaterialByName(1);
    }

    public void ChangeScene(string name, UnityAction _changeSceneCallback = null)
    {
        if (IsChanging) return;
        IsChanging = true;
        m_NextScene = name;
        // Time.timeScale = 1;
        // m_ChangeSceneCallback = _changeSceneCallback;
        // IngameEntityManager.Instance.ClearMap();
        // GUIManager.Instance.ClearAllOpenedPanelList();
        // GUIManager.Instance.ClearAllOpenedPopupList();
        StartCoroutine(OnChangingScene());
    }

    IEnumerator OnChangingScene()
    {
        // Debug.Log("Start Change " + m_NextScene);
        // SoundManager.Instance.PauseBGM();
        // if (m_CurrentSceneName.Contains("MainMenu"))
        // {
        // }
        // if (m_CurrentSceneName.Contains("Level"))
        // {
        // }
        // EventManager.TriggerEvent("StopFire");
        // Time.timeScale = 1;
        // if (!m_NextScene.Contains("StartMenu"))
        // {
        //     PopupLoading pl = GUIManager.Instance.GetUICanvasByID(UIID.POPUP_LOADING) as PopupLoading;
        //     if (pl != null)
        //     {
        //         pl.Setup();
        //         GUIManager.Instance.ShowUIPopup(pl);
        //         yield return Yielders.Get(1);
        //     }
        // }
        AsyncOperation async = SceneManager.LoadSceneAsync(m_NextScene, LoadSceneMode.Single);

        async.allowSceneActivation = false;
        yield return Yielders.EndOfFrame;
        // if (m_CurrentSceneName.Contains("Level"))
        // {
        //     yield return Yielders.Get(0.5f);
        // }
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


        if (m_NextScene.Contains("PlayScene"))
        {
            // SoundManager.Instance.PlayBGM(BGMType.INGAME);
            Helper.DebugLog("PlayerScene");
            // InGameObjectsManager.Instance.LoadMap();
        }

        yield return Yielders.Get(0.1f);
        yield return Yielders.EndOfFrame;

        InGameObjectsManager.Instance.LoadMap();
        CamController.Instance.m_Char = InGameObjectsManager.Instance.m_Char;
        FindPanelInGame();
        // m_MapType = InGameObjectsManager.Instance.m_Map.m_MapType;
        // else
        // {
        //     SoundManager.Instance.PlayBGM(BGMType.MENU);
        // }
    }
}
