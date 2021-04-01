using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupWin : UICanvas
{
    public Button btn_Claim;
    public Button btn_X3Reward;
    public Text txt_Level;
    public Text txt_GoldLevel;

    private void Awake()
    {
        m_ID = UIID.POPUP_WIN;
        Init();

        GUIManager.Instance.AddClickEvent(btn_Claim, OnClaim);
        GUIManager.Instance.AddClickEvent(btn_X3Reward, OnX3Reward);
    }

    private void OnEnable()
    {
        txt_Level.text = "Level " + ProfileManager.GetLevel2();
        txt_GoldLevel.text = "+" + GameManager.Instance.m_GoldLevel.ToString();

        MiniCharacterStudio.Instance.SpawnMiniCharacter("Win");
    }

    public void OnClaim()
    {
        OnClose();
        InGameObjectsManager.Instance.LoadMap();
        CamController.Instance.m_Char = InGameObjectsManager.Instance.m_Char;
        EventManager.CallEvent(GameEvent.LEVEL_END);
    }

    public override void OnClose()
    {
        base.OnClose();
        MiniCharacterStudio.Instance.DestroyChar();
    }

    public void OnX3Reward()
    {

    }
}
