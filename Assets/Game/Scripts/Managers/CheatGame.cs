using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using GoogleMobileAdsMediationTestSuite.Api;

public class CheatGame : MonoBehaviour
{
    public GameObject g_CheatPanel;
    public InputField m_Level;
    public PanelInGame m_PanelInGame;

    public Button btn_CheckAdsMediation;

    public Button btn_NoAds;

    public Button btn_Mail;

    public InputField m_Gold;

    private void Awake()
    {
        // GUIManager.Instance.AddClickEvent(btn_CheckAdsMediation, AdsManager.Instance.ShowMediationTestSuite);
        // GUIManager.Instance.AddClickEvent(btn_NoAds, HideBanner);
        GUIManager.Instance.AddClickEvent(btn_Mail, OpenMail);
    }

    public void OpenCheatPanel()
    {
        g_CheatPanel.SetActive(!g_CheatPanel.activeInHierarchy);
    }

    public void JumpLevel()
    {
        int jumpLevel = int.Parse(m_Level.text);
        InGameObjectsManager.Instance.LoadMapCheat(jumpLevel);

        CamController.Instance.m_Char = InGameObjectsManager.Instance.m_Char;
        GameManager.Instance.m_LevelStart = false;
        m_PanelInGame.SetOutGame();
    }

    public void AddGold()
    {
        BigNumber gold = new BigNumber(m_Gold.text);
        ProfileManager.SetGold(gold);
        m_PanelInGame.txt_TotalGold.text = ProfileManager.GetGold();
    }

    public void HideBanner()
    {
        PlayerPrefs.SetInt(ConfigKeys.noAds, 1);
        AdsManager.Instance.DestroyBanner();
    }

    // public void ShowMediationTestSuite()
    // {
    //     MediationTestSuite.Show();
    // }

    // public void OpenMail()
    // {
    //     Application.OpenURL("http://unity3d.com/");
    // }

    public void OpenMail()
    {
        //         string email = "skysoftone2018@gmail.com";
        // #if UNITY_EDITOR || UNITY_ANDROID
        //         string subject = MyEscapeURL("Feedback Stickman Warriors-Version " + Application.version);
        // #else
        //         string subject = MyEscapeURL("IOS_Feedback Stickman Warriors-Version " + Application.version);
        // #endif
        //         string body = MyEscapeURL("Please tell us what we can improve in the game.");


        //         Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);

        string email = "skycentralgames@gmail.com";

        string subject = MyEscapeURL("Feedback Hide and Seek 3D: Monster Escape v" + Application.version);

        string body = MyEscapeURL("Please tell us what we can improve in the game.");


        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }

    string MyEscapeURL(string url)
    {
        return WWW.EscapeURL(url).Replace("+", "%20");
    }
}
