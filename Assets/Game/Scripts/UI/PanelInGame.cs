using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class PanelInGame : MonoBehaviour
{

    [Header("UI Texts")]
    public Text txt_Keys;
    public Text txt_TotalGold;
    public Text txt_GoldLevel;

    [Header("UI Buttons")]
    public Button btn_Play;
    public Button btn_Pause;
    public Button btn_Outfit;

    [Header("UI GameObjects")]
    public GameObject g_Setting;
    public GameObject g_Gold;
    public GameObject g_Shop;
    public GameObject g_NoAds;
    public GameObject g_Play;
    public GameObject g_Joystick;
    public GameObject g_Pause;
    public GameObject ui_Keys;
    public GameObject g_GoldLevel;
    public Text txt_Level;

    private void Awake()
    {
        // Init();
        GUIManager.Instance.AddClickEvent(btn_Play, OnPlay);
        GUIManager.Instance.AddClickEvent(btn_Pause, OnPause);
        GUIManager.Instance.AddClickEvent(btn_Outfit, OnOpenOutfit);
    }

    private void OnEnable()
    {
        SetOutGame();

        StartListenToEvent();
    }

    private void OnDisable()
    {
        StopListenToEvent();
    }

    public void StartListenToEvent()
    {
        EventManager.AddListener(GameEvent.CHAR_SPOTTED, () => g_Joystick.SetActive(false));

        // EventManager.AddListener(GameEvent.CHAR_WIN, ShowGameWinUI);
        EventManager.AddListener(GameEvent.CHAR_WIN, () => g_Joystick.SetActive(false));

        EventManager.AddListener(GameEvent.LEVEL_START, SetIngame);
        EventManager.AddListener(GameEvent.LEVEL_END, SetOutGame);

        // EventManagerWithParam<BigNumber>.AddListener(GameEvent.CLAIM_GOLD_IN_GAME, SetGoldInGame);

        EventManagerWithParam<int>.AddListener(GameEvent.CHAR_CLAIM_KEYKEY, UpdateCurrentKey);
        EventManagerWithParam<bool>.AddListener(GameEvent.LEVEL_PAUSE, PauseLevel);
    }

    public void StopListenToEvent()
    {
        EventManager.RemoveListener(GameEvent.CHAR_SPOTTED, () => g_Joystick.SetActive(false));

        // EventManager.RemoveListener(GameEvent.CHAR_WIN, ShowGameWinUI);
        EventManager.RemoveListener(GameEvent.CHAR_WIN, () => g_Joystick.SetActive(false));

        EventManager.RemoveListener(GameEvent.LEVEL_START, SetIngame);
        EventManager.RemoveListener(GameEvent.LEVEL_END, SetOutGame);

        // EventManagerWithParam<BigNumber>.RemoveListener(GameEvent.CLAIM_GOLD_IN_GAME, SetGoldInGame);

        EventManagerWithParam<int>.RemoveListener(GameEvent.CHAR_CLAIM_KEYKEY, UpdateCurrentKey);
        EventManagerWithParam<bool>.RemoveListener(GameEvent.LEVEL_PAUSE, PauseLevel);
    }

    public void SetIngame()
    {
        g_Setting.SetActive(false);
        g_Gold.SetActive(false);
        g_Shop.SetActive(false);
        g_NoAds.SetActive(false);
        g_Play.SetActive(false);
        g_Play.SetActive(false);
        g_Pause.SetActive(true);

        g_Joystick.SetActive(true);
        ui_Keys.SetActive(true);

        int keys = InGameObjectsManager.Instance.m_Map.m_Keys.Count;
        txt_Keys.text = 0.ToString() + "/" + keys.ToString();

        txt_Level.gameObject.SetActive(false);

        g_GoldLevel.SetActive(true);
        txt_GoldLevel.text = "0";
    }

    public void SetGoldInGame(BigNumber _value)
    {
        txt_GoldLevel.text = GameManager.Instance.m_GoldLevel.ToString();
    }

    public void SetOutGame()
    {
        g_Setting.SetActive(true);
        g_Gold.SetActive(true);
        g_Shop.SetActive(true);
        g_NoAds.SetActive(true);
        g_Play.SetActive(true);
        g_Play.SetActive(true);
        g_Pause.SetActive(false);

        g_Joystick.SetActive(false);
        ui_Keys.SetActive(false);

        txt_TotalGold.text = ProfileManager.GetGold();
        g_GoldLevel.SetActive(false);

        txt_Level.gameObject.SetActive(true);

        // if (GameManager.Instance.m_MapType == MapType.KEY)
        // if (InGameObjectsManager.Instance.m_Map.m_MapType == MapType.KEY)
        // {
        //     if (InGameObjectsManager.Instance.m_Map == null)
        //     {
        //         Helper.DebugLog("map is null");
        //     }
        //     else
        //     {
        //         Helper.DebugLog("map is not nulllllllllllllllllllll");
        //     }
        //     txt_Level.text = "LEVEL" + ProfileManager.GetLevel2();
        //     Helper.DebugLog("Maptype: " + GameManager.Instance.m_MapType);
        // }
        // else if (InGameObjectsManager.Instance.m_Map.m_MapType == MapType.BONUS)
        // {
        //     if (InGameObjectsManager.Instance.m_Map == null)
        //     {
        //         Helper.DebugLog("map is null");
        //     }
        //     else
        //     {
        //         Helper.DebugLog("map is not nulllllllllllllllllllll");
        //     }
        //     txt_Level.text = "BONUS";
        //     Helper.DebugLog("Maptype: " + GameManager.Instance.m_MapType);
        // }
    }

    public void EnableJoystick()
    {
        g_Joystick.SetActive(true);
    }

    public void DisableJoystick()
    {
        g_Joystick.SetActive(false);
    }

    public void ShowKeys()
    {
        ui_Keys.SetActive(true);

        int keys = InGameObjectsManager.Instance.m_Map.m_Keys.Count;
        txt_Keys.text = 0.ToString() + "/" + keys.ToString();
    }

    public void UpdateCurrentKey(int _value)
    {
        RectTransform rect = txt_Keys.GetComponent<RectTransform>();
        int keys = InGameObjectsManager.Instance.m_Map.m_Keys.Count;

        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(rect.DOScale(new Vector3(2f, 2f, 2f), 0.25f));
        Tween tween = rect.DOScale(new Vector3(1f, 1f, 1f), 0.25f).OnComplete
        (
            () => txt_Keys.text = _value.ToString() + "/" + keys.ToString()
        );
        mySequence.Append(tween);
    }

    public void OnPlay()
    {
        EventManager.CallEvent(GameEvent.LEVEL_START);
        GameManager.Instance.m_LevelStart = true;
        CamController.Instance.ZoomOutChar();
        // InGameObjectsManager.Instance.m_Char.m_DisableMove = false;
    }

    public void OnPause()
    {
        PopupCaller.OpenPausePopup();
        Time.timeScale = 0f;
        g_Joystick.SetActive(false);
    }

    public void OnOpenOutfit()
    {
        PopupCaller.OpenOutfitPopup();
    }

    public void PauseLevel(bool _pause)
    {
        g_Joystick.SetActive(!_pause);
    }
}
