using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[DefaultExecutionOrder(-90)]
public class InGameObjectsManager : Singleton<InGameObjectsManager>
{
    public Character m_Char;
    public List<Enemy> m_Enemies;
    public List<GoldInGame> m_GoldInGames;
    public List<IEffectFlyer> m_IEffectFlyer = new List<IEffectFlyer>();
    public MapController m_Map;
    public PanelInGame m_PanelInGame;

    private AsyncOperationHandle<GameObject> m_MapAsync;

    // private void Awake()
    // {
    //     Addressables.InitializeAsync();
    // }

    private void OnEnable()
    {
        // LoadMap();

        StartListenToEvent();
    }

    private void OnDisable()
    {
        StopListenToEvent();
    }

    private void OnDestroy()
    {
        Helper.DebugLog("On destroyyyyyyyyyyyyyyyyyy");
        DestroyMap();
        StopListenToEvent();
    }

    public void StartListenToEvent()
    {
        EventManagerWithParam<int>.AddListener(GameEvent.CHAR_CLAIM_KEYKEY, SpawnKey);
        EventManagerWithParam<int>.AddListener(GameEvent.EQUIP_CHAR, SpawnChar);
    }

    public void StopListenToEvent()
    {
        EventManagerWithParam<int>.RemoveListener(GameEvent.CHAR_CLAIM_KEYKEY, SpawnKey);
        EventManagerWithParam<int>.RemoveListener(GameEvent.EQUIP_CHAR, SpawnChar);
    }

    public void DestroyAllInGameObjects()
    {
        RemoveGoldInGames();
        RemoveEnemies();
        RemoveEffectFlyer();
        DestroyMap();
        DestroyChar();
    }

    public void FindPanelInGame()
    {
        Helper.DebugLog("FindPanelInGame");
        m_PanelInGame = FindObjectOfType<PanelInGame>().GetComponent<PanelInGame>();
    }

    // public void LoadMap()
    // {
    //     StartCoroutine(IELoadMap());
    // }

    public async void LoadMap(UnityAction _callback = null)
    {
        int level = ProfileManager.GetLevel();
        string levelStr = "Map" + ProfileManager.GetLevel2();
        // var goo = m_Maps[level - 1].InstantiateAsync();
        var goo = Addressables.InstantiateAsync(levelStr);

        goo.Completed += (handle) =>
        {
            m_MapAsync = goo;

            MapController mapControl = goo.Result.GetComponent<MapController>();

            m_Map = mapControl;
            m_Map.SetupMap();

            if (m_PanelInGame == null)
            {
                FindPanelInGame();
            }

            if (m_Map.m_MapType == MapType.KEY)
            {
                m_PanelInGame.txt_Level.text = "LEVEL" + level;
                GameManager.Instance.m_MapType = MapType.KEY;
            }
            else if (m_Map.m_MapType == MapType.BONUS)
            {
                m_PanelInGame.txt_Level.text = "BONUS";
                GameManager.Instance.m_MapType = MapType.BONUS;
            }

            CamController.Instance.m_Char = m_Char;
            FindPanelInGame();
            GameManager.Instance.FindPanelInGame();
            GUIManager.Instance.FindPanelLoadingAds();

            GUIManager.Instance.GetGOPanelLoading().SetActive(false);


            if ((ProfileManager.GetLevel() - 1) == 0)
            {
                m_PanelInGame.OnPlay();
                Helper.DebugLog("Load level 1");
            }
        };

        await goo.Task;

        if (_callback != null)
        {
            _callback();
        }
    }

    // public GameObject OnLoadDone(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
    // {
    //     return obj;
    // }

    public void LoadMapCheat(int _level)
    {
        ProfileManager.SetLevel(_level);

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
    }

    public void SpawnChar(int _id)
    {
        if (m_Char != null)
        {
            Destroy(m_Char.gameObject);
            m_Char = null;
        }
        m_Map.SpawnChar();
    }

    public void RemoveEnemies()
    {
        for (int i = 0; i < m_Enemies.Count; i++)
        {
            m_Enemies[i].Despawn();
        }

        m_Enemies.Clear();

        Helper.DebugLog("RemoveEnemies");
    }

    public void RemoveEffectFlyer()
    {
        for (int i = 0; i < m_IEffectFlyer.Count; i++)
        {
            m_IEffectFlyer[i].Despawn();
        }

        m_IEffectFlyer.Clear();
    }

    public void DestroyMap()
    {
        if (m_Map != null)
        {
            Destroy(m_Map.gameObject);
            Addressables.ReleaseInstance(m_MapAsync);
            // Addressables.Release(m_Map.gameObject);
        }
    }

    public void DestroyChar()
    {
        if (m_Char != null)
        {
            Destroy(m_Char.gameObject);
            Helper.DebugLog("Remove Char");
        }
    }

    public void RemoveGoldInGames()
    {
        for (int i = 0; i < m_GoldInGames.Count; i++)
        {
            m_GoldInGames[i].Despawn();
        }

        m_GoldInGames.Clear();
    }


    public void SpawnKey(int _value)
    {
        m_Map.SpawnKey(_value);
    }

    // public void TestRelease()
    // {
    //     // SimplePool.Release();
    //     RemoveEnemies();
    //     SimplePool.Release();
    //     Resources.UnloadUnusedAssets();
    //     System.GC.Collect();
    // }
}
