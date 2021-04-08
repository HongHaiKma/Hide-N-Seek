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

    public void OpenWinPopup()
    {
        StartCoroutine(IEOpenWinPopup());
    }

    IEnumerator IEOpenWinPopup()
    {
        yield return Yielders.Get(2f);

        PopupWin popup = GUIManager.Instance.GetUICanvasByID(UIID.POPUP_WIN) as PopupWin;
        popup.Setup();
        GUIManager.Instance.ShowUIPopup(popup);
    }

    public void OpenLosePopup()
    {
        StartCoroutine(IEOpenLosePopup());
    }

    IEnumerator IEOpenLosePopup()
    {
        yield return Yielders.Get(2f);

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
    }

    public static void OpenLevelRewardPopup()
    {
        PopupLevelReward popup = GUIManager.Instance.GetUICanvasByID(UIID.POPUP_LEVELREWARD) as PopupLevelReward;

        GUIManager.Instance.ShowUIPopup(popup);
    }
}
