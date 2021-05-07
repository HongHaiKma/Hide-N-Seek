using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupRateUs : UICanvas
{
    public Button btn_Rate;
    public Button btn_NotNow;
    // public Button btn_NoThanks;

    public int star;

    public Button btn_Star1;
    public Button btn_Star2;
    public Button btn_Star3;
    public Button btn_Star4;
    public Button btn_Star5;

    public Image img_Star1;
    public Image img_Star2;
    public Image img_Star3;
    public Image img_Star4;
    public Image img_Star5;

    private void Awake()
    {
        m_ID = UIID.POPUP_RATEUS;
        Init();

        GUIManager.Instance.AddClickEvent(btn_Rate, ShowRate);
        GUIManager.Instance.AddClickEvent(btn_NotNow, NotNow);
        // GUIManager.Instance.AddClickEvent(btn_NoThanks, NoThanks);

        GUIManager.Instance.AddClickEvent(btn_Star1, ClickStar1);
        GUIManager.Instance.AddClickEvent(btn_Star2, ClickStar2);
        GUIManager.Instance.AddClickEvent(btn_Star3, ClickStar3);
        GUIManager.Instance.AddClickEvent(btn_Star4, ClickStar4);
        GUIManager.Instance.AddClickEvent(btn_Star5, ClickStar5);

    }

    private void OnEnable()
    {
        star = 0;
        img_Star1.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_S_Star_Off];
        img_Star2.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_M_Star_Off];
        img_Star3.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_L_Star_Off];
        img_Star4.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_M_Star_Off];
        img_Star5.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_S_Star_Off];
    }

    public void ShowRate()
    {
        if (star > 4)
        {
            PlayerPrefs.SetInt(ConfigKeys.rateUs, 0);
            OnClose();
            Application.OpenURL("market://details?id=" + Application.identifier);
        }
        else
        {
            PlayerPrefs.SetInt(ConfigKeys.rateUs, 0);
            OnClose();
            OpenMail();
        }
    }

    public void NoThanks()
    {
        PlayerPrefs.SetInt(ConfigKeys.rateUs, 0);
        OnClose();
    }

    public void NotNow()
    {
        OnClose();
    }

    public void ClickStar1()
    {
        img_Star1.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_S_Star_On];
        img_Star2.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_M_Star_Off];
        img_Star3.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_L_Star_Off];
        img_Star4.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_M_Star_Off];
        img_Star5.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_S_Star_Off];
        star = 1;
    }

    public void ClickStar2()
    {
        img_Star1.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_S_Star_On];
        img_Star2.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_M_Star_On];
        img_Star3.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_L_Star_Off];
        img_Star4.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_M_Star_Off];
        img_Star5.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_S_Star_Off];
        star = 2;
    }

    public void ClickStar3()
    {
        img_Star1.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_S_Star_On];
        img_Star2.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_M_Star_On];
        img_Star3.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_L_Star_On];
        img_Star4.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_M_Star_Off];
        img_Star5.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_S_Star_Off];
        star = 3;
    }

    public void ClickStar4()
    {
        img_Star1.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_S_Star_On];
        img_Star2.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_M_Star_On];
        img_Star3.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_L_Star_On];
        img_Star4.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_M_Star_On];
        img_Star5.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_S_Star_Off];
        star = 4;
    }

    public void ClickStar5()
    {
        img_Star1.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_S_Star_On];
        img_Star2.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_M_Star_On];
        img_Star3.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_L_Star_On];
        img_Star4.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_M_Star_On];
        img_Star5.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_Rate_S_Star_On];
        star = 5;
    }

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

        string email = "nvphong2312x87@gmail.com";

        string subject = MyEscapeURL("Feedback Hide and Seek 3D: Monster Escape v" + Application.version);

        string body = MyEscapeURL("Please tell us what we can improve in the game.");


        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }

    string MyEscapeURL(string url)
    {
        return WWW.EscapeURL(url).Replace("+", "%20");
    }
}
