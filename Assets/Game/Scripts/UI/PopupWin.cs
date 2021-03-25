using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupWin : UICanvas
{
    public Button btn_Claim;
    public Button btn_X3Reward;
    public Text txt_Level;

    private void Awake()
    {
        m_ID = UIID.POPUP_WIN;
        Init();

        Helper.DebugLog("PopupWin Awakeeeeeeeeeeeee");

        GUIManager.Instance.AddClickEvent(btn_Claim, OnClaim);
        GUIManager.Instance.AddClickEvent(btn_X3Reward, OnX3Reward);
    }

    public void OnClaim()
    {
        OnClose();
        InGameObjectsManager.Instance.LoadMap();
    }

    public void OnX3Reward()
    {

    }
}
