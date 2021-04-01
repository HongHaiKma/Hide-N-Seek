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

    private void Awake()
    {
        m_ID = UIID.POPUP_WIN;
        Init();

        GUIManager.Instance.AddClickEvent(btn_Claim, OnClaim);
        GUIManager.Instance.AddClickEvent(btn_X3Reward, OnX3Reward);
    }

    private void OnEnable()
    {
        txt_Level.text = "Level " + (ProfileManager.GetLevel() - 1).ToString();

        BigNumber levelGold = InGameObjectsManager.Instance.m_Map.m_LevelGold;
        txt_GoldLevel.text = "+" + (GameManager.Instance.m_GoldLevel + levelGold).ToString();

        MiniCharacterStudio.Instance.SpawnMiniCharacter("Win");

        SpawnGoldEffect();
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
    }

    public void OnX3Reward()
    {

    }
}
