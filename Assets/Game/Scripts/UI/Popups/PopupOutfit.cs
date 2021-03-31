using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupOutfit : UICanvas
{
    public int m_SelectedCharacter;

    public Text txt_CharName;
    public Button btn_Equip;
    public Button btn_Equipped;
    public Button btn_BuyByGold;
    public Button btn_BuyByAds;

    public Text txt_BuyByGold;
    public Text txt_AdsNumber;

    private void Awake()
    {
        m_ID = UIID.POPUP_OUTFIT;
        Init();
        SetChar(ProfileManager.GetSelectedCharacter());

        GUIManager.Instance.AddClickEvent(btn_Equip, OnEquip);
        GUIManager.Instance.AddClickEvent(btn_BuyByGold, OnBuyByGold);
        GUIManager.Instance.AddClickEvent(btn_BuyByAds, OnBuyByAds);
    }

    private void OnEnable()
    {
        m_SelectedCharacter = ProfileManager.GetSelectedCharacter();

        StartListenToEvent();
    }

    private void Disable()
    {
        StopListenToEvent();
    }

    public void StartListenToEvent()
    {
        EventManagerWithParam<int>.AddListener(GameEvent.LOAD_OUTFIT_CHARACTER, SetChar);
    }

    public void StopListenToEvent()
    {
        EventManagerWithParam<int>.RemoveListener(GameEvent.LOAD_OUTFIT_CHARACTER, SetChar);
    }

    public void SetChar(int _id)
    {
        CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(_id);
        CharacterProfileData data = ProfileManager.GetCharacterProfileData(_id);

        txt_CharName.text = config.m_Name;
        m_SelectedCharacter = _id;

        SetOwned(_id);

        // if (checkowned)
        // {
        //     btn_Equipped.gameObject.SetActive(checkowned);
        //     btn_BuyByAds.gameObject.SetActive(!checkowned);
        //     btn_BuyByGold.gameObject.SetActive(!checkowned);
        //     btn_Equip.gameObject.SetActive(!checkowned);
        // }
        // else
        // {

        // }
    }

    public void OnEquip()
    {
        EventManagerWithParam<int>.CallEvent(GameEvent.EQUIP_CHAR, m_SelectedCharacter);
    }

    public void OnBuyByGold() //Remember to Update UICharacterCard when buy succeed
    {
        CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(m_SelectedCharacter);

        if (ProfileManager.IsEnoughGold(config.m_Price))
        {
            ProfileManager.ConsumeGold(config.m_Price);
            ProfileManager.UnlockNewCharacter(m_SelectedCharacter);
            ProfileManager.SetSelectedCharacter(m_SelectedCharacter);
            SetOwned(m_SelectedCharacter);
        }
        else
        {

        }
    }

    public void OnBuyByAds() //Remember to Update UICharacterCard when buy succeed
    {
        CharacterProfileData data = ProfileManager.GetCharacterProfileData(m_SelectedCharacter);
        CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(m_SelectedCharacter);

        data.ClaimByAds(1);

        if (data.m_AdsNumber >= config.m_AdsNumber)
        {
            ProfileManager.UnlockNewCharacter(m_SelectedCharacter);
            ProfileManager.SetSelectedCharacter(m_SelectedCharacter);
        }

        SetOwned(m_SelectedCharacter);
    }

    public void SetOwned(int _id)
    {
        CharacterProfileData data = ProfileManager.GetCharacterProfileData(m_SelectedCharacter);
        CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(m_SelectedCharacter);

        bool _checkowned = (ProfileManager.GetCharacterProfileData(_id) != null);
        bool _adsCheck = config.CheckAds();

        bool equipped = (_id == ProfileManager.GetSelectedCharacter());

        if (equipped)
        {
            btn_Equipped.gameObject.SetActive(equipped);
            btn_BuyByAds.gameObject.SetActive(!equipped);
            btn_BuyByGold.gameObject.SetActive(!equipped);
            btn_Equip.gameObject.SetActive(!equipped);
            return;
        }

        if (_checkowned)
        {
            btn_Equipped.gameObject.SetActive(!_checkowned);
            btn_BuyByAds.gameObject.SetActive(!_checkowned);
            btn_BuyByGold.gameObject.SetActive(!_checkowned);
            btn_Equip.gameObject.SetActive(_checkowned);
            // return;
        }
        else
        {
            if (_adsCheck)
            {
                Helper.DebugLog("Boy by Ads!!!!");
                btn_Equipped.gameObject.SetActive(!_adsCheck);
                btn_BuyByAds.gameObject.SetActive(_adsCheck);
                btn_BuyByGold.gameObject.SetActive(!_adsCheck);
                btn_Equip.gameObject.SetActive(!_adsCheck);

                txt_AdsNumber.text = "2" + "/" + config.m_AdsNumber.ToString();
            }
            else
            {
                Helper.DebugLog("Boy by Gold!!!!");
                btn_Equipped.gameObject.SetActive(_adsCheck);
                btn_BuyByAds.gameObject.SetActive(_adsCheck);
                btn_BuyByGold.gameObject.SetActive(!_adsCheck);
                btn_Equip.gameObject.SetActive(_adsCheck);

                txt_BuyByGold.text = config.m_Price.ToString();
            }
        }
    }
}
