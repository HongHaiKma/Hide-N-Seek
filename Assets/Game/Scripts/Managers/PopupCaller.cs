using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupCaller : Singleton<PopupCaller>
{
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
        EventManager.AddListener(GameEvent.CHAR_WIN, OpenWinPopup);
    }

    public void StopListenToEvent()
    {
        EventManager.RemoveListener(GameEvent.CHAR_WIN, OpenWinPopup);
    }

    public static void OpenWinPopup()
    {
        PopupWin popup = GUIManager.Instance.GetUICanvasByID(UIID.POPUP_WIN) as PopupWin;

        GUIManager.Instance.ShowUIPopup(popup);
    }
}
