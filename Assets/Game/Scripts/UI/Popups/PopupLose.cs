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
        MiniCharacterStudio.Instance.SpawnMiniCharacter("Lose");
    }

    public void OnRetry()
    {
        OnClose();
        InGameObjectsManager.Instance.LoadMap();
        CamController.Instance.m_Char = InGameObjectsManager.Instance.m_Char;
        // EventManager.CallEvent(GameEvent.LEVEL_END);
        EventManager.CallEvent(GameEvent.LEVEL_START);
        GameManager.Instance.m_LevelStart = true;
    }

    public void OnHome()
    {
        OnClose();
        InGameObjectsManager.Instance.LoadMap();
        CamController.Instance.m_Char = InGameObjectsManager.Instance.m_Char;
        EventManager.CallEvent(GameEvent.LEVEL_END);
    }
}
