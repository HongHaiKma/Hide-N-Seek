using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupLevelReward : UICanvas
{
    public int m_CharId;
    public Button btn_Video;
    public Button btn_NoThanks;
    public Text txt_Name;

    private void Awake()
    {
        m_ID = UIID.POPUP_LEVELREWARD;
        Init();

        GUIManager.Instance.AddClickEvent(btn_Video, OnWatchVideoReward);
        GUIManager.Instance.AddClickEvent(btn_NoThanks, OnClose);
    }

    private void OnEnable()
    {
        BlockPanel.Instance.g_BlackPanel.SetActive(false);
        btn_NoThanks.gameObject.SetActive(false);
        List<int> chars = new List<int>();
        chars.Clear();

        Dictionary<int, CharacterDataConfig> configs = GameData.Instance.GetCharacterDataConfig();

        // Helper.DebugLog("Configs Count: " + configs.Count);

        for (int i = 1; i < configs.Count + 1; i++)
        {
            CharacterProfileData data = ProfileManager.GetCharacterProfileData(configs[i].m_Id);

            if (data == null)
            {
                if (configs[i].m_AdsCheck == 0)
                {
                    chars.Add(configs[i].m_Id);
                }
            }
        }

        m_CharId = chars[Random.Range(0, chars.Count)];
        txt_Name.text = configs[m_CharId].m_Name;

        // Helper.DebugLog("Reward char: " + (CharacterType)m_CharId);

        MiniCharacterStudio.Instance.SpawnMiniCharacterIdle(m_CharId);

        StartCoroutine(DisplayNoThanks());
    }

    IEnumerator DisplayNoThanks()
    {
        yield return Yielders.Get(2f);
        btn_NoThanks.gameObject.SetActive(true);
    }

    public void OnWatchVideoReward()
    {
        ProfileManager.UnlockNewCharacter(m_CharId);
        OnClose();
    }

    public override void OnClose()
    {
        base.OnClose();
        MiniCharacterStudio.Instance.DestroyChar();
    }
}
