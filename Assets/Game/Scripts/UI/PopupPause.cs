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

    private void Awake()
    {
        m_ID = UIID.POPUP_PAUSE;
        Init();

        m_PanelInGame = FindObjectOfType<PanelInGame>().GetComponent<PanelInGame>();

        GUIManager.Instance.AddClickEvent(btn_Home, OnHome);
        GUIManager.Instance.AddClickEvent(btn_Retry, OnRetry);
        GUIManager.Instance.AddClickEvent(btn_Resume, OnResume);
    }

    public void OnRetry()
    {
        OnClose();
        InGameObjectsManager.Instance.LoadMap();
        CamController.Instance.m_Char = InGameObjectsManager.Instance.m_Char;
        // EventManager.CallEvent(GameEvent.LEVEL_END);
        EventManager.CallEvent(GameEvent.LEVEL_START);
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

        // Time.timeScale = 1f;
        // m_PanelInGame
    }

    public void OnResume()
    {
        OnClose();
        EventManagerWithParam<bool>.CallEvent(GameEvent.LEVEL_PAUSE, false);
    }
}
