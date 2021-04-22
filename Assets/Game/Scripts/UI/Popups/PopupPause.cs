using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupPause : UICanvas
{
    public Button btn_Home;
    public Button btn_Retry;
    public Button btn_Resume;
    private PanelInGame m_PanelInGame;
    private Canvas m_Canvas;

    private void Awake()
    {
        m_ID = UIID.POPUP_PAUSE;
        Init();

        m_Canvas = GetComponent<Canvas>();

        m_PanelInGame = FindObjectOfType<PanelInGame>().GetComponent<PanelInGame>();

        // Game

        GUIManager.Instance.AddClickEvent(btn_Home, OnHome);
        GUIManager.Instance.AddClickEvent(btn_Retry, OnRetry);
        GUIManager.Instance.AddClickEvent(btn_Resume, OnResume);
    }

    private void OnEnable()
    {
        BlockPanel.Instance.SetupBlock(m_Canvas.sortingOrder - 1);
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
        GameManager.Instance.m_LevelStart = true;

        EventManagerWithParam<bool>.CallEvent(GameEvent.LEVEL_PAUSE, false);

        // Time.timeScale = 1f;
        // g_Joystick.SetActive(false);
    }

    public void OnHome()
    {
        OnClose();
        InGameObjectsManager.Instance.LoadMap();
        CamController.Instance.m_Char = InGameObjectsManager.Instance.m_Char;
        EventManager.CallEvent(GameEvent.LEVEL_END);

        EventManagerWithParam<bool>.CallEvent(GameEvent.LEVEL_PAUSE, false);
        GameManager.Instance.GetPanelInGame().g_Joystick.SetActive(false);
        SoundManager.Instance.m_BGM.Pause();

        // Time.timeScale = 1f;
        // m_PanelInGame
    }

    public void OnResume()
    {
        OnClose();
        EventManagerWithParam<bool>.CallEvent(GameEvent.LEVEL_PAUSE, false);
    }

    public override void OnClose()
    {
        base.OnClose();
        BlockPanel.Instance.Close();
    }
}
