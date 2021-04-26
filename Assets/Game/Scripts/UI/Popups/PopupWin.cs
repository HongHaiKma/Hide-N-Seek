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
    public Transform m_GoldEffectTarget;
    public Transform m_GoldEffectStart;

    public Image img_RewardBar;

    private Canvas m_Canvas;

    private bool m_OpenAgain;

    private void Awake()
    {
        m_ID = UIID.POPUP_WIN;
        Init();

        m_Canvas = GetComponent<Canvas>();

        GUIManager.Instance.AddClickEvent(btn_Claim, OnClaim);
        GUIManager.Instance.AddClickEvent(btn_X3Reward, OnX3Reward);
    }

    public override void Setup()
    {
        base.Setup();
        m_OpenAgain = false;
    }

    private void OnEnable()
    {
        if (!m_OpenAgain)
        {
            AdsManager.Instance.m_WatchInter = true;
            GameManager.Instance.m_LoseStreak = 0;
            SoundManager.Instance.PlaySoundWinLong(false);
        }

        btn_X3Reward.gameObject.SetActive(true);

        if (GameManager.Instance.m_MapType == MapType.KEY)
        {
            // txt_Level.text = "LEVEL" + ProfileManager.GetLevel().ToString();
            txt_Level.text = "LEVEL " + (ProfileManager.GetLevel() - 1).ToString();
            Helper.DebugLog("Normal level");
        }
        else if (GameManager.Instance.m_MapType == MapType.BONUS)
        {
            txt_Level.text = "BONUS LEVEL";
            Helper.DebugLog("BONUS level");
        }

        // txt_Level.text = "LEVEL " + (ProfileManager.GetLevel() - 1).ToString();

        txt_GoldLevel.text = "+" + GameManager.Instance.m_GoldLevel.ToString();

        MiniCharacterStudio.Instance.SpawnMiniCharacter("Win");

        // Helper.DebugLog("Popup win OnEnableeeeeeeeeeeeeeeeeeeeee");

        if (!m_OpenAgain)
        {
            m_OpenAgain = true;
            SpawnGoldEffect();
            RewardFill();
        }

        StartListenToEvent();
    }

    private void OnDisable()
    {
        StopListenToEvent();
    }

    public void StartListenToEvent()
    {
        EventManager.AddListener(GameEvent.ADS_GOLD_2_LOGIC, OnX3RewardLogic);
        EventManager.AddListener(GameEvent.ADS_GOLD_2_ANIM, OnX3RewardAnim);
    }

    public void StopListenToEvent()
    {
        EventManager.RemoveListener(GameEvent.ADS_GOLD_2_LOGIC, OnX3RewardLogic);
        EventManager.RemoveListener(GameEvent.ADS_GOLD_2_ANIM, OnX3RewardAnim);
    }

    public void RewardFill()
    {
        StartCoroutine(IERewardFill());
    }

    public IEnumerator IERewardFill()
    {
        img_RewardBar.fillAmount = 0f;

        int levelCheck = ProfileManager.GetLevel();

        float result = 0f;

        if ((levelCheck - 1) <= 6)
        {
            result = (float)(((levelCheck - 1) % 5) / 5f);
        }
        else
        {
            result = (float)(((levelCheck - 2) % 5) / 5f);
        }

        if (result == 0f)
        {
            result = 1f;
            BlockPanel.Instance.SetupBlock(m_Canvas.sortingOrder + 1);
        }

        float time1 = 0f;
        while (time1 < 1f)
        {
            time1 += Time.deltaTime;
            img_RewardBar.fillAmount = Mathf.Lerp(0f, result, (time1) / 1f);

            yield return null;
        }

        img_RewardBar.fillAmount = result;


        int goldChar = ProfileManager.GetTotalGoldChar();
        int goldCharOwned = ProfileManager.GetTotaOwnedlGoldChar();
        if ((goldChar - goldCharOwned) > 0)
        {
            if (result == 1f)
            {
                InGameObjectsManager.Instance.RemoveEffectFlyer();
                PopupCaller.OpenLevelRewardPopup();
            }
        }
        else
        {
            BlockPanel.Instance.Close();
        }
    }

    public void SpawnGoldEffect()
    {
        // InGameObjectsManager.Instance.RemoveEffectFlyer();

        for (int i = 0; i < 15; i++)
        {
            GameObject g_EffectGold = PrefabManager.Instance.SpawnEffectPrefabPool(EffectKeys.GoldEffect1.ToString(), m_GoldEffectStart.position);
            g_EffectGold.transform.SetParent(this.transform);
            g_EffectGold.transform.localScale = new Vector3(1, 1, 1);
            g_EffectGold.transform.position = m_GoldEffectStart.position;

            Flyer flyer = g_EffectGold.GetComponent<Flyer>();
            IEffectFlyer iEF = g_EffectGold.GetComponent<IEffectFlyer>();

            InGameObjectsManager.Instance.m_IEffectFlyer.Add(iEF);

            flyer.FlyToTargetOneSide(m_GoldEffectTarget.position, () =>
            {

            }, true, -1, 0.3f);
        }
    }

    public void OnClaim()
    {
        OnClose();

        InGameObjectsManager.Instance.DestroyAllInGameObjects();
        GameManager.Instance.ChangeToPlayScene(true, () =>
        {
            EventManager.CallEvent(GameEvent.LEVEL_END);

            bool oddLevel = (((ProfileManager.GetLevel() - 1) % 2) == 1 ? true : false);
            bool level = (ProfileManager.GetLevel() - 1) >= 3 ? true : false;

            if (oddLevel && level)
            {
                AdsManager.Instance.WatchInterstitial();
                return;
            }

            int levelMap = (ProfileManager.GetLevel() - 1);
            bool rateUs = (PlayerPrefs.GetInt(ConfigKeys.rateUs) == 1);

            if ((levelMap % 4 == 0) && levelMap <= 16 && levelMap > 1 && rateUs)
            {
                PopupCaller.OpenRateUsPopup();
            }
        });
    }

    public override void OnClose()
    {
        SoundManager.Instance.PlaySoundWinLong(true);
        base.OnClose();
        MiniCharacterStudio.Instance.DestroyChar();
        InGameObjectsManager.Instance.RemoveEffectFlyer();
        BlockPanel.Instance.Close();
    }

    public void OnX3Reward()
    {
        SoundManager.Instance.PlaySoundWinLong(true);
        InGameObjectsManager.Instance.RemoveEffectFlyer();
        AdsManager.Instance.WatchRewardVideo(RewardType.GOLD_2);
        // OnX3RewardLogic();
        // OnX3RewardAnim();
    }

    public void OnX3RewardLogic()
    {
        ProfileManager.AddGold(GameManager.Instance.m_GoldLevel * 2);
        GameManager.Instance.m_GoldLevel += GameManager.Instance.m_GoldLevel * 2;
        txt_GoldLevel.text = "+" + (GameManager.Instance.m_GoldLevel).ToString();
        btn_X3Reward.gameObject.SetActive(false);
        SpawnGoldEffect();
        FadeIn(0.35f);
    }

    public void OnX3RewardAnim()
    {
        // SpawnGoldEffect();
        // GameManager.Instance.m_GoldLevel += GameManager.Instance.m_GoldLevel * 2;
        // txt_GoldLevel.text = "+" + (GameManager.Instance.m_GoldLevel).ToString();
        // btn_X3Reward.gameObject.SetActive(false);

        // FadeIn();
    }

    // public void AnimFadeIn()
    // {
    //     StartCoroutine(IEAnimFadeIn());
    // }

    // IEnumerator IEAnimFadeIn()
    // {
    //     float m_Time = 1.5f;
    //     float m_TimeCount = 0f;
    //     m_TimeCount += Time.deltaTime;
    //     m_CanvasGroup.alpha = m_TimeCount;

    //     yield return Yielders.Get(1f);

    //     m_CanvasGroup.alpha = 1f;
    // }
}
