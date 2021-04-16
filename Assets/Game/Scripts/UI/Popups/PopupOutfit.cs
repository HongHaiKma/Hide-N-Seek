using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupOutfit : UICanvas
{
    public int m_SelectedCharacter;

    public Text txt_TotalGold;
    public Text txt_CharName;
    public Button btn_Equip;
    public Button btn_Equipped;
    public Button btn_BuyByGold;
    public Button btn_BuyByAds;
    public Button btn_AdsGold;

    public Text txt_BuyByGold;
    public Text txt_AdsNumber;

    public GameObject g_Warning;

    private void Awake()
    {
        m_ID = UIID.POPUP_OUTFIT;
        Init();

        GUIManager.Instance.AddClickEvent(btn_Equip, OnEquip);
        GUIManager.Instance.AddClickEvent(btn_BuyByGold, OnBuyByGold);
        GUIManager.Instance.AddClickEvent(btn_BuyByAds, OnBuyByAds);
        GUIManager.Instance.AddClickEvent(btn_AdsGold, OnAdsGold);

        SetChar(ProfileManager.GetSelectedCharacter());
    }

    private void OnEnable()
    {
        g_Warning.SetActive(false);
        m_SelectedCharacter = ProfileManager.GetSelectedCharacter();
        txt_TotalGold.text = ProfileManager.GetGold();

        SetChar(m_SelectedCharacter);
        MiniCharacterStudio.Instance.SpawnMiniCharacterIdle(m_SelectedCharacter);

        StartListenToEvent();
    }

    private void OnDisable()
    {
        StopListenToEvent();
    }

    public void StartListenToEvent()
    {
        EventManagerWithParam<int>.AddListener(GameEvent.LOAD_OUTFIT_CHARACTER, SetChar);
        EventManager.AddListener(GameEvent.ADS_CHARACTER_LOGIC, OnByBuyAdsLogic);
        EventManager.AddListener(GameEvent.ADS_GOLD_1_LOGIC, OnAdsGoldLogic);
        EventManager.AddListener(GameEvent.ADS_GOLD_1_ANIM, OnAdsGoldAnim);
    }

    public void StopListenToEvent()
    {
        EventManagerWithParam<int>.RemoveListener(GameEvent.LOAD_OUTFIT_CHARACTER, SetChar);
        EventManager.RemoveListener(GameEvent.ADS_CHARACTER_LOGIC, OnByBuyAdsLogic);
        EventManager.RemoveListener(GameEvent.ADS_GOLD_1_LOGIC, OnAdsGoldLogic);
        EventManager.RemoveListener(GameEvent.ADS_GOLD_1_ANIM, OnAdsGoldAnim);
    }

    public void SetChar(int _id)
    {
        g_Warning.SetActive(false);

        CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(_id);

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
        SetOwned(m_SelectedCharacter);
    }

    public void OnBuyByGold() //Remember to Update UICharacterCard when buy succeed
    {
        CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(m_SelectedCharacter);

        if (ProfileManager.IsEnoughGold(config.m_Price))
        // if (ProfileManager.MyProfile.IsEnoughGold(config.m_Price))
        {
            ProfileManager.ConsumeGold(config.m_Price);
            ProfileManager.UnlockNewCharacter(m_SelectedCharacter);
            ProfileManager.SetSelectedCharacter(m_SelectedCharacter);
            SetOwned(m_SelectedCharacter);
            EventManagerWithParam<int>.CallEvent(GameEvent.CLAIM_CHAR, m_SelectedCharacter);
            EventManagerWithParam<int>.CallEvent(GameEvent.EQUIP_CHAR, m_SelectedCharacter);

            AnalysticsManager.LogUnlockCharacter(config.m_Id, config.m_Name);
        }
        else
        {
            StartCoroutine(IEWarning());
        }
    }

    IEnumerator IEWarning()
    {
        g_Warning.SetActive(true);
        yield return Yielders.Get(2f);
        g_Warning.SetActive(false);
    }

    public void OnBuyByAds() //Remember to Update UICharacterCard when buy succeed
    {
        AdsManager.Instance.WatchRewardVideo(RewardType.CHARACTER);
        // OnByBuyAdsLogic();
    }

    public void OnByBuyAdsLogic()
    {
        CharacterProfileData data = ProfileManager.GetCharacterProfileData(m_SelectedCharacter);
        CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(m_SelectedCharacter);

        if (data == null)
        {
            ProfileManager.UnlockNewCharacter(m_SelectedCharacter);
            data = new CharacterProfileData();
            data = ProfileManager.GetCharacterProfileData(m_SelectedCharacter);
            data.ClaimByAds(1);
        }
        else
        {
            data.ClaimByAds(1);
        }

        if (ProfileManager.IsOwned(m_SelectedCharacter))
        {
            ProfileManager.SetSelectedCharacter(m_SelectedCharacter);
            EventManagerWithParam<int>.CallEvent(GameEvent.EQUIP_CHAR, m_SelectedCharacter);

            AnalysticsManager.LogUnlockCharacter(config.m_Id, config.m_Name);
        }

        SetOwned(m_SelectedCharacter);
        EventManagerWithParam<int>.CallEvent(GameEvent.CLAIM_CHAR, m_SelectedCharacter);
    }

    public void OnAdsGold()
    {
        AdsManager.Instance.WatchRewardVideo(RewardType.GOLD_1);
        // OnAdsGoldLogic();
        // OnAdsGoldAnim();
    }

    public void OnAdsGoldLogic()
    {
        ProfileManager.AddGold(100);
        AnalysticsManager.LogGetShopGold1();
    }

    public void OnAdsGoldAnim()
    {
        EventManager.CallEvent(GameEvent.UPDATE_GOLD_TEXT);
        SpawnGoldEffect();
        txt_TotalGold.text = ProfileManager.GetGold();
    }

    public void SpawnGoldEffect()
    {
        for (int i = 0; i < 15; i++)
        {
            GameObject g_EffectGold = PrefabManager.Instance.SpawnEffectPrefabPool(EffectKeys.GoldEffect1.ToString(), btn_AdsGold.transform.position);
            g_EffectGold.transform.SetParent(this.transform);
            g_EffectGold.transform.localScale = new Vector3(1, 1, 1);
            g_EffectGold.transform.position = btn_AdsGold.transform.position;

            Flyer flyer = g_EffectGold.GetComponent<Flyer>();
            IEffectFlyer iEF = g_EffectGold.GetComponent<IEffectFlyer>();

            InGameObjectsManager.Instance.m_IEffectFlyer.Add(iEF);

            flyer.FlyToTargetOneSide(txt_TotalGold.transform.position, () =>
            {

            }, true, -1, 0.3f);
        }
    }

    public override void OnClose()
    {
        base.OnClose();
        MiniCharacterStudio.Instance.DestroyChar();
        InGameObjectsManager.Instance.RemoveEffectFlyer();
    }

    public void SetOwned(int _id)
    {
        CharacterProfileData data = ProfileManager.GetCharacterProfileData(m_SelectedCharacter);
        CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(m_SelectedCharacter);

        bool _checkowned = ProfileManager.IsOwned(_id);
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
                btn_Equipped.gameObject.SetActive(!_adsCheck);
                btn_BuyByAds.gameObject.SetActive(_adsCheck);
                btn_BuyByGold.gameObject.SetActive(!_adsCheck);
                btn_Equip.gameObject.SetActive(!_adsCheck);

                if (data != null)
                {
                    txt_AdsNumber.text = data.m_AdsNumber.ToString() + "/" + config.m_AdsNumber.ToString();
                }
                else
                {
                    txt_AdsNumber.text = "0" + "/" + config.m_AdsNumber.ToString();
                }
            }
            else
            {
                btn_Equipped.gameObject.SetActive(_adsCheck);
                btn_BuyByAds.gameObject.SetActive(_adsCheck);
                btn_BuyByGold.gameObject.SetActive(!_adsCheck);
                btn_Equip.gameObject.SetActive(_adsCheck);

                txt_BuyByGold.text = config.m_Price.ToString();
            }
        }
    }
}
