using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Popup1 : UICanvas
{
    public Button btn_Popup11;
    public Button btn_Popup12;

    private void Awake()
    {
        m_ID = UIID.POPUP_1;
        Init();

        GUIManager1.Instance.AddClickEvent(btn_Popup11, OpenPopup11);
        GUIManager1.Instance.AddClickEvent(btn_Popup12, OpenPopup12);
    }

    public void OpenPopup11()
    {
        // if (GUIManager1.Instance != null)
        // {
        Popup11 popup = GUIManager1.Instance.GetUICanvasByID(UIID.POPUP_11) as Popup11;

        // popup.Setup();
        GUIManager1.Instance.ShowUIPopup(popup);
        // }
    }

    public void OpenPopup12()
    {
        Helper.DebugLog("OpenPopup12");
    }
}
