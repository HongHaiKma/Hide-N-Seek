using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupLose : UICanvas
{
    public Button btn_Retry;
    public Button btn_Home;

    private void Awake()
    {
        m_ID = UIID.POPUP_LOSE;
        Init();

        GUIManager.Instance.AddClickEvent(btn_Retry, OnRetry);
        GUIManager.Instance.AddClickEvent(btn_Home, OnHome);
    }

    private void OnEnable()
    {
        GameManager.Instance.m_LoseStreak++;
        if (GameManager.Instance.m_LoseStreak >= 2)
        {
            AdsManager.Instance.m_WatchInter = true;
        }
        else
        {
            AdsManager.Instance.m_WatchInter = false;
        }

        MiniCharacterStudio.Instance.SpawnMiniCharacter("Lose");
    }

    public void OnRetry()
    {
        OnClose();
        InGameObjectsManager.Instance.LoadMap();

        int levelPlay = ProfileManager.GetLevel();
        AnalysticsManager.LogPlayLevel(levelPlay);
        AnalysticsManager.LogRetryLevel(levelPlay);

        CamController.Instance.m_Char = InGameObjectsManager.Instance.m_Char;
        // EventManager.CallEvent(GameEvent.LEVEL_END);
        EventManager.CallEvent(GameEvent.LEVEL_START);
        GameManager.Instance.GetPanelInGame().g_Joystick.SetActive(true);
        GameManager.Instance.GetPanelInGame().SetIngame();
        GameManager.Instance.m_LevelStart = true;
        CamController.Instance.m_StartFollow = true;
        SoundManager.Instance.m_BGM.Play();

        bool level = (ProfileManager.GetLevel() - 1) >= 3 ? true : false;
        if (level)
        {
            AdsManager.Instance.WatchInterstitial();
        }
    }

    public void OnHome()
    {
        OnClose();
        InGameObjectsManager.Instance.LoadMap();
        CamController.Instance.m_Char = InGameObjectsManager.Instance.m_Char;
        EventManager.CallEvent(GameEvent.LEVEL_END);

        bool level = (ProfileManager.GetLevel() - 1) >= 3 ? true : false;
        if (level)
        {
            AdsManager.Instance.WatchInterstitial();
        }
    }
}
