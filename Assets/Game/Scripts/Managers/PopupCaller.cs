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
        EventManager.AddListener(GameEvent.CHAR_SPOTTED, OpenLosePopup);
        // EventManager.AddListener(GameEvent.CHAR_SPOTTED, OpenPausePopup);
    }

    public void StopListenToEvent()
    {
        EventManager.RemoveListener(GameEvent.CHAR_WIN, OpenWinPopup);
        EventManager.RemoveListener(GameEvent.CHAR_SPOTTED, OpenLosePopup);
        // EventManager.RemoveListener(GameEvent.CHAR_SPOTTED, OpenPausePopup);
    }

    public static void OpenWinPopup()
    {
        PopupWin popup = GUIManager.Instance.GetUICanvasByID(UIID.POPUP_WIN) as PopupWin;

        GUIManager.Instance.ShowUIPopup(popup);
    }

    public static void OpenLosePopup()
    {
        PopupLose popup = GUIManager.Instance.GetUICanvasByID(UIID.POPUP_LOSE) as PopupLose;

        GUIManager.Instance.ShowUIPopup(popup);
    }

    public static void OpenPausePopup()
    {
        PopupPause popup = GUIManager.Instance.GetUICanvasByID(UIID.POPUP_PAUSE) as PopupPause;

        GUIManager.Instance.ShowUIPopup(popup);
    }

    public static void OpenOutfitPopup()
    {
        PopupOutfit popup = GUIManager.Instance.GetUICanvasByID(UIID.POPUP_OUTFIT) as PopupOutfit;

        GUIManager.Instance.ShowUIPopup(popup);
        Helper.DebugLog("PopupCaller OnOpenOutfit");
    }
}
