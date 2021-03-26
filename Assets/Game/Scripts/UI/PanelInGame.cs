using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class PanelInGame : MonoBehaviour
{
    public GameObject gameLoseUI;
    public GameObject gameWinUI;
    bool gameIsOver;


    public GameObject ui_Keys;
    public Text txt_Keys;
    public Button btn_Play;
    public Text txt_TotalGold;

    [Header("Test")]
    public InputField inputLevel;
    public InputField inputChar;

    [Header("UI GameObjects")]
    public GameObject g_Setting;
    public GameObject g_Gold;
    public GameObject g_Shop;
    public GameObject g_NoAds;
    public GameObject g_Play;
    public GameObject g_Joystick;

    private void Awake()
    {
        // Init();
        GUIManager.Instance.AddClickEvent(btn_Play, OnPlay);
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
        EventManager.AddListener(GameEvent.CHAR_SPOTTED, ShowGameLoseUI);
        EventManager.AddListener(GameEvent.CHAR_SPOTTED, () => g_Joystick.SetActive(false));

        // EventManager.AddListener(GameEvent.CHAR_WIN, ShowGameWinUI);
        EventManager.AddListener(GameEvent.CHAR_WIN, () => g_Joystick.SetActive(false));

        EventManager.AddListener(GameEvent.LEVEL_START, SetIngame);
        EventManager.AddListener(GameEvent.LEVEL_END, SetOutGame);

        EventManagerWithParam<int>.AddListener(GameEvent.CHAR_CLAIM_KEYKEY, UpdateCurrentKey);
    }

    public void StopListenToEvent()
    {
        EventManager.RemoveListener(GameEvent.CHAR_SPOTTED, ShowGameLoseUI);
        EventManager.RemoveListener(GameEvent.CHAR_SPOTTED, () => g_Joystick.SetActive(false));

        // EventManager.RemoveListener(GameEvent.CHAR_WIN, ShowGameWinUI);
        EventManager.RemoveListener(GameEvent.CHAR_WIN, () => g_Joystick.SetActive(false));

        EventManager.RemoveListener(GameEvent.LEVEL_START, SetIngame);
        EventManager.RemoveListener(GameEvent.LEVEL_END, SetOutGame);

        EventManagerWithParam<int>.RemoveListener(GameEvent.CHAR_CLAIM_KEYKEY, UpdateCurrentKey);
    }

    // void Update()
    // {
    //     if (gameIsOver)
    //     {
    //         if (Input.GetKeyDown(KeyCode.Space))
    //         {
    //             SceneManager.LoadScene(0);
    //         }
    //     }
    // }

    public void SetIngame()
    {
        CloseAllPopup();

        g_Setting.SetActive(false);
        g_Gold.SetActive(false);
        g_Shop.SetActive(false);
        g_NoAds.SetActive(false);
        g_Play.SetActive(false);
        g_Play.SetActive(false);

        g_Joystick.SetActive(true);
        ui_Keys.SetActive(true);

        int keys = InGameObjectsManager.Instance.m_Map.m_Keys.Count;
        txt_Keys.text = 0.ToString() + "/" + keys.ToString();
    }

    public void SetOutGame()
    {
        g_Setting.SetActive(true);
        g_Gold.SetActive(true);
        g_Shop.SetActive(true);
        g_NoAds.SetActive(true);
        g_Play.SetActive(true);
        g_Play.SetActive(true);

        g_Joystick.SetActive(false);
        ui_Keys.SetActive(false);

        txt_TotalGold.text = ProfileManager.GetGold();
    }

    public void Restart()
    {
        if (gameIsOver)
        {
            SceneManager.LoadScene(0);
        }
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

    void ShowGameWinUI()
    {
        OnGameOver(gameWinUI);
    }

    void ShowGameLoseUI()
    {
        OnGameOver(gameLoseUI);
    }

    public void CloseAllPopup()
    {
        gameLoseUI.SetActive(false);
        gameWinUI.SetActive(false);
    }

    void OnGameOver(GameObject gameOverUI)
    {
        gameOverUI.SetActive(true);
        gameIsOver = true;
    }
}
