using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupRateUs : UICanvas
{
    public Button btn_Rate;

    private void Awake()
    {
        m_ID = UIID.POPUP_RATEUS;
        Init();

        GUIManager.Instance.AddClickEvent(btn_Rate, ShowRate);
    }

    public void ShowRate()
    {
        Application.OpenURL("market://details?id=" + Application.identifier);
    }
}
