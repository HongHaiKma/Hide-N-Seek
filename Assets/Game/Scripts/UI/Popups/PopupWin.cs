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
        btn_X3Reward.gameObject.SetActive(true);

        if (GameManager.Instance.m_MapType == MapType.KEY)
        {
            txt_Level.text = "LEVEL" + ProfileManager.GetLevel().ToString();
        }
        else if (GameManager.Instance.m_MapType == MapType.BONUS)
        {
            txt_Level.text = "BONUS LEVEL";
        }

        txt_Level.text = "Level " + (ProfileManager.GetLevel() - 1).ToString();

        txt_GoldLevel.text = "+" + GameManager.Instance.m_GoldLevel.ToString();

        MiniCharacterStudio.Instance.SpawnMiniCharacter("Win");

        Helper.DebugLog("Popup win OnEnableeeeeeeeeeeeeeeeeeeeee");

        if (!m_OpenAgain)
        {
            m_OpenAgain = true;
            SpawnGoldEffect();
            RewardFill();
        }
    }

    public void RewardFill()
    {
        StartCoroutine(IERewardFill());
    }

    public IEnumerator IERewardFill()
    {
        img_RewardBar.fillAmount = 0f;
        float result = (float)(((ProfileManager.GetLevel() - 1) % 5) / 5f);

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

        if (result == 1f)
        {
            InGameObjectsManager.Instance.RemoveEffectFlyer();
            PopupCaller.OpenLevelRewardPopup();
        }
    }

    public void SpawnGoldEffect()
    {
        for (int i = 0; i < 15; i++)
        {
            GameObject g_EffectGold = PrefabManager.Instance.SpawnEffectPrefabPool(EffectKeys.GoldEffect1.ToString(), m_GoldEffectStart.position);
            g_EffectGold.transform.SetParent(this.transform);
            g_EffectGold.transform.localScale = new Vector3(1, 1, 1);
            g_EffectGold.transform.position = m_GoldEffectStart.position;

            Flyer flyer = g_EffectGold.GetComponent<Flyer>();
            IEffectFlyer iEF = g_EffectGold.GetComponent<IEffectFlyer>();

            InGameObjectsManager.Instance.m_IEffectFlyer.Add(iEF);

            if (flyer == null)
            {
                Helper.DebugLog("Gold effect is nullllllllllllllllllllllllllllll");
            }

            flyer.FlyToTargetOneSide(m_GoldEffectTarget.position, () =>
            {

            }, true, -1, 0.3f);
        }
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
        InGameObjectsManager.Instance.RemoveEffectFlyer();
        BlockPanel.Instance.Close();
    }

    public void OnX3Reward()
    {
        btn_X3Reward.gameObject.SetActive(false);
        SpawnGoldEffect();
        ProfileManager.AddGold(GameManager.Instance.m_GoldLevel * 2f);
    }
}
