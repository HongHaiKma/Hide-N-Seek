using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[DefaultExecutionOrder(-90)]
public class InGameObjectsManager : Singleton<InGameObjectsManager>
{
    // public AssetReference m_Maps;
    public AssetReference[] m_Maps;
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

    public IEnumerator LoadMap()
    {
        // if (!AdsManager.Instance.m_BannerLoaded)
        // {
        //     AdsManager.Instance.LoadBanner();
        // }

        // if (!AdsManager.Instance.interstitial.IsLoaded())
        // {
        //     AdsManager.Instance.RequestInter();
        // }
        // Resources.UnloadUnusedAssets();
        // System.GC.Collect();
        // yield return Yielders.Get(0.1f);

        // StartCoroutine(GUIManager.Instance.m_PanelLoading.StartLoading());

        Helper.DebugLog("Load map ingame object manager!!!!");

        // RemoveEnemies();
        // RemoveGoldInGames();

        Helper.DebugLog("Destroy all Enemies!!!");

        // SimplePool.Release();
        // Resources.UnloadUnusedAssets();
        // System.GC.Collect();

        // if (m_Map != null)
        // {
        //     Destroy(m_Char.gameObject);
        //     m_Map.nav_Surface.gameObject.SetActive(false);
        //     Destroy(m_Map.gameObject);
        //     Helper.DebugLog("Destroy mapppppppppppppp");
        // }
        int level = ProfileManager.GetLevel();
        // string name = "Maps/Map" + level.ToString();
        // string name2 = "Map" + level.ToString();
        // GameObject go = Resources.Load<GameObject>(name);

        // AsyncOperationHandle<GameObject> loadOp = Addressables.LoadAssetAsync<GameObject>(key);

        var goo = m_Maps[level - 1].LoadAssetAsync<GameObject>();
        // var goo = Addressables.InstantiateAsync<GameObject>(m_Maps[level - 1]);

        m_MapAsync = goo;

        // yield return goo;
        yield return Yielders.Get(1f);

        GameObject go = Instantiate(goo.Result);

        // GameObject go = new GameObject();
        // var go = SyncAddressables.Instantiate(name2);
        // Addressables.LoadAssetAsync<GameObject>(name2).Completed += OnLoadDone;

        // AsyncOperationHandle<GameObject> textureHandle = Addressables.LoadAssetAsync<GameObject>(name2);

        // var obj = Addressables.InstantiateAsync("Map2");
        // if (obj.IsDone)
        // {
        //     Helper.DebugLog("Addresables is doneeeeeeeeeeeeee");
        //     go = obj.Result as GameObject;
        // }

        // GameObject map = Instantiate(go, Vector3.zero, Quaternion.identity);
        // MapController mapControl = map.GetComponent<MapController>();
        MapController mapControl = go.GetComponent<MapController>();
        // MapController mapControl = go.GetComponent<MapController>();
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

        // GUIManager.Instance.m_PanelLoading.gameObject.SetActive(false);
        GUIManager.Instance.GetGOPanelLoading().SetActive(false);

        // GUIManager.Instance.AddClickEvent(m_PanelInGame.btn_BuyNoAds, Purchaser.Instance.BuyNoAds);

        if ((ProfileManager.GetLevel() - 1) == 0)
        {
            m_PanelInGame.OnPlay();
            Helper.DebugLog("Load level 1");
        }

        // GUIManager.Instance.g_IngameLoading.GetComponent<Animator>().SetTrigger("LoadingOut");
    }

    // public GameObject OnLoadDone(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
    // {
    //     return obj;
    // }

    public void LoadMapCheat(int _level)
    {
        RemoveEnemies();
        RemoveGoldInGames();

        // SimplePool.Release();
        // Resources.UnloadUnusedAssets();
        // System.GC.Collect();

        if (m_Map != null)
        {
            Destroy(m_Char.gameObject);
            m_Map.nav_Surface.gameObject.SetActive(false);
            Destroy(m_Map.gameObject);
            Addressables.Release(m_Map.gameObject);
        }
        ProfileManager.SetLevel(_level);
        string name = "Maps/Map" + _level.ToString();
        GameObject go = Resources.Load<GameObject>(name);
        GameObject map = Instantiate(go, Vector3.zero, Quaternion.identity);
        MapController mapControl = map.GetComponent<MapController>();
        mapControl.nav_Surface.BuildNavMesh();
        // mapControl.nav_Surface.UpdateNavMesh(mapControl.nav_Surface.navMeshData);
        m_Map = mapControl;
        m_Map.SetupMap();

        // if (m_Map.m_MapType == MapType.KEY)
        // {
        //     GameManager.Instance.GetPanelInGame().txt_Level.text = "LEVEL" + ProfileManager.GetLevel2();
        //     // Helper.DebugLog("Maptype: " + GameManager.Instance.m_MapType);
        // }
        // else if (m_Map.m_MapType == MapType.BONUS)
        // {
        //     GameManager.Instance.GetPanelInGame().txt_Level.text = "BONUS";
        //     // Helper.DebugLog("Maptype: " + GameManager.Instance.m_MapType);
        // }
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
            Addressables.Release(m_MapAsync);
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
