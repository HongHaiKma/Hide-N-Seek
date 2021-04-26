using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class PopupPause : UICanvas
{
    public Button btn_Home;
    public Button btn_Retry;
    public Button btn_Resume;
    private PanelInGame m_PanelInGame;
    private Canvas m_Canvas;

    private void Awake()
    {
        m_ID = UIID.POPUP_PAUSE;
        Init();

        m_Canvas = GetComponent<Canvas>();

        m_PanelInGame = FindObjectOfType<PanelInGame>().GetComponent<PanelInGame>();

        // Game

        GUIManager.Instance.AddClickEvent(btn_Home, OnHome);
        GUIManager.Instance.AddClickEvent(btn_Retry, OnRetry);
        GUIManager.Instance.AddClickEvent(btn_Resume, OnResume);
    }

    private void OnEnable()
    {
        BlockPanel.Instance.SetupBlock(m_Canvas.sortingOrder - 1);
    }

    public void OnRetry()
    {
        OnClose();


        int levelPlay = ProfileManager.GetLevel();
        AnalysticsManager.LogPlayLevel(levelPlay);
        AnalysticsManager.LogRetryLevel(levelPlay);

        // EventManager.CallEvent(GameEvent.LEVEL_START);
        // GameManager.Instance.GetPanelInGame().g_Joystick.SetActive(true);
        // GameManager.Instance.GetPanelInGame().SetIngame();
        // GameManager.Instance.m_LevelStart = true;
        // CamController.Instance.m_StartFollow = true;

        // EventManagerWithParam<bool>.CallEvent(GameEvent.LEVEL_PAUSE, false);

        Time.timeScale = 1;
        InGameObjectsManager.Instance.DestroyAllInGameObjects();
        GameManager.Instance.ChangeToPlayScene(true, () =>
        {
            GameManager.Instance.m_LevelStart = true;

            EventManager.CallEvent(GameEvent.LEVEL_START);
            GameManager.Instance.GetPanelInGame().g_Joystick.SetActive(true);
            GameManager.Instance.GetPanelInGame().SetIngame();
            GameManager.Instance.m_LevelStart = true;
            CamController.Instance.m_StartFollow = true;

            EventManagerWithParam<bool>.CallEvent(GameEvent.LEVEL_PAUSE, false);
        });

        // GameManager.Instance.m_LevelStart = true;

        // EventManager.CallEvent(GameEvent.LEVEL_START);
        // GameManager.Instance.GetPanelInGame().g_Joystick.SetActive(true);
        // GameManager.Instance.GetPanelInGame().SetIngame();
        // GameManager.Instance.m_LevelStart = true;
        // CamController.Instance.m_StartFollow = true;

        // EventManagerWithParam<bool>.CallEvent(GameEvent.LEVEL_PAUSE, false);

        // StartCoroutine(IEOnRetry());

        // InGameObjectsManager.Instance.DestroyAllInGameObjects();
        // InGameObjectsManager.Instance.LoadMap();

        // LoadMapManager();
    }

    public void OnHome()
    {
        // GUIManager.Instance.g_IngameLoading.SetActive(false);
        // GUIManager.Instance.g_IngameLoading.SetActive(true);

        // OnClose();
        // EventManager.CallEvent(GameEvent.LEVEL_END);
        // GameManager.Instance.m_LevelStart = false;
        // EventManagerWithParam<bool>.CallEvent(GameEvent.LEVEL_PAUSE, false);
        // GameManager.Instance.GetPanelInGame().g_Joystick.SetActive(false);
        // SoundManager.Instance.m_BGM.Pause();

        // InGameObjectsManager.Instance.LoadMap();
        // CamController.Instance.m_Char = InGameObjectsManager.Instance.m_Char;

        // StartCoroutine(LoadMap());
        // LoadMap();

        // InGameObjectsManager.Instance.DestroyAllInGameObjects();

        Time.timeScale = 1;
        InGameObjectsManager.Instance.DestroyAllInGameObjects();
        GameManager.Instance.ChangeToPlayScene(true, () =>
         {
             OnClose();
             EventManager.CallEvent(GameEvent.LEVEL_END);
             GameManager.Instance.m_LevelStart = false;
             EventManagerWithParam<bool>.CallEvent(GameEvent.LEVEL_PAUSE, false);
             GameManager.Instance.GetPanelInGame().g_Joystick.SetActive(false);
             SoundManager.Instance.m_BGM.Pause();
         });

        // var tasks = new List<Task>();
        // tasks.Add(Task.Run(() =>
        // {
        //     // Task1();
        //     // Task2();
        //     Helper.DebugLog("Task 1 complete");
        //     Helper.DebugLog("Task 2 complete");
        // }));

        // Task t = Task.WhenAll(tasks.ToArray());
        // try
        // {
        //     await t;
        // }
        // catch { }
    }

    async Task LoadMap()
    {
        // await Task.Delay(1);

        // InGameObjectsManager.Instance.LoadMap();

        // await Task.Delay(1);
        // CamController.Instance.m_Char = InGameObjectsManager.Instance.m_Char;
        // GUIManager.Instance.g_IngameLoading.GetComponent<Animator>().SetTrigger("LoadingOut");
        // InGameObjectsManager.Instance.RemoveEnemies();
        var tasks = new List<Task>();
        tasks.Add(Task.Run(() =>
        {
            InGameObjectsManager.Instance.LoadMap();
            CamController.Instance.m_Char = InGameObjectsManager.Instance.m_Char;

        }));

        Task t = Task.WhenAll(tasks);
        try
        {
            await t;
        }
        catch { }

        GUIManager.Instance.g_IngameLoading.GetComponent<Animator>().SetTrigger("LoadingOut");
    }

    public void OnResume()
    {
        OnClose();
        EventManagerWithParam<bool>.CallEvent(GameEvent.LEVEL_PAUSE, false);
    }

    public override void OnClose()
    {
        base.OnClose();
        BlockPanel.Instance.Close();
    }
}
