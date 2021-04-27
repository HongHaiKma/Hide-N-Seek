using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTutorial : UICanvas
{
    public RectTransform tf_Content;
    public RectTransform rect_Hand;

    private void Awake()
    {
        m_ID = UIID.POPUP_TUTORIAL;
        Init();
    }

    public void Setup(TutorialType _tutType)
    {
        switch (_tutType)
        {
            case TutorialType.SHOP_BUYBYGOLD:
                SetupTutShopByBuyGold_ClickShopIcon();
                break;
            case TutorialType.SHOP_BUYBYADS:
                SetupTutShopByBuyAds();
                break;
        }
    }

    public void SetupTutShopByBuyGold_ClickShopIcon()
    {
        rect_Hand.localPosition = new Vector3(-304f, -576f, 0f);
        PanelInGame panelInGame = GameManager.Instance.GetPanelInGame();
        RectTransform rect = panelInGame.g_Shop.GetComponent<RectTransform>();
        panelInGame.g_Shop.GetComponent<RectTransform>().SetParent(tf_Content, true);
        panelInGame.g_Shop.GetComponent<RectTransform>().anchoredPosition = new Vector2(-410f, -602f);
        panelInGame.g_Shop.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
    }

    public void SetupTutShopByBuyGold_UnClickShopIcon()
    {
        PanelInGame panelInGame = GameManager.Instance.GetPanelInGame();
        RectTransform rect = panelInGame.g_Shop.GetComponent<RectTransform>();
        panelInGame.g_Shop.GetComponent<RectTransform>().SetParent(panelInGame.tf_Content, true);
        panelInGame.g_Shop.GetComponent<RectTransform>().anchoredPosition = new Vector2(-410f, -602f);
        panelInGame.g_Shop.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
    }

    public void SetupTutShopByBuyGold_ClickCharUI(RectTransform _rect)
    {
        _rect.SetParent(tf_Content, true);
        rect_Hand.localPosition = new Vector3(-231f, -638f, 0f);
    }

    public void SetupTutShopByBuyGold_UnClickCharUI(RectTransform _rect)
    {
        PopupOutfit popup = PopupCaller.GetOutfitPopup();
        RectTransform rectOld = popup.m_UICharacterOutfit.rect_Content;
        _rect.SetParent(rectOld, true);

        SetupTutShopByBuyGold_ClickBuyByGoldUI();
    }

    public void SetupTutShopByBuyGold_ClickBuyByGoldUI()
    {
        RectTransform rect = PopupCaller.GetOutfitPopup().btn_BuyByGold.GetComponent<RectTransform>();
        rect.SetParent(tf_Content);
        rect_Hand.localPosition = new Vector3(173f, 107f, 0f);
    }

    public void SetupTutShopByBuyGold_UnClickBuyByGoldUI(RectTransform _rect)
    {
        RectTransform rect = PopupCaller.GetOutfitPopup().btn_BuyByGold.GetComponent<RectTransform>();
        rect.SetParent(_rect);
        rect_Hand.localPosition = new Vector3(173f, 107f, 0f);
        Helper.DebugLog("Complete tut popup!!!!!!!!");
    }

    public void SetupTutShopByBuyAds()
    {

    }

    // public override void OnClose()
    // {
    //     Helper.DebugLog("Complete tut popup!!!!!!!!");
    //     base.OnClose();
    // }
}
