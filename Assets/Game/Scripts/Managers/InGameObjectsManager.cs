using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-90)]
public class InGameObjectsManager : Singleton<InGameObjectsManager>
{
    public Character m_Char;
    public List<Enemy> m_Enemies;
    public List<GoldInGame> m_GoldInGames;
    public MapController m_Map;
    public PanelInGame m_PanelInGame;

    private void OnEnable()
    {
        // LoadMap();

        StartListenToEvent();
    }

    private void OnDisable()
    {
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

    public void DestroyMap()
    {
        if (m_Map != null)
        {
            Destroy(m_Map.gameObject);
            Destroy(m_Char.gameObject);
        }
    }

    public void FindPanelInGame()
    {
        m_PanelInGame = FindObjectOfType<PanelInGame>().GetComponent<PanelInGame>();
    }

    public void LoadMap()
    {
        RemoveEnemies();
        RemoveGoldInGames();

        // SimplePool.Release();
        // Resources.UnloadUnusedAssets();
        // System.GC.Collect();

        if (m_Map != null)
        {
            // m_Map.nav_Surface.RemoveData();
            // m_Map.nav_Surface.gameObject.SetActive(false);
            Destroy(m_Char.gameObject);
            m_Map.nav_Surface.gameObject.SetActive(false);
            Destroy(m_Map.gameObject);
        }
        int level = ProfileManager.GetLevel();
        string name = "Maps/Map" + level.ToString();
        GameObject go = Resources.Load<GameObject>(name);
        GameObject map = Instantiate(go, Vector3.zero, Quaternion.identity);
        MapController mapControl = map.GetComponent<MapController>();
        mapControl.nav_Surface.BuildNavMesh();
        // mapControl.nav_Surface.UpdateNavMesh(mapControl.nav_Surface.navMeshData);
        m_Map = mapControl;
        m_Map.SetupMap();

        if (m_PanelInGame == null)
        {
            FindPanelInGame();
        }

        if (m_Map.m_MapType == MapType.KEY)
        {
            m_PanelInGame.txt_Level.text = "LEVEL" + level;
        }
        else if (m_Map.m_MapType == MapType.BONUS)
        {
            m_PanelInGame.txt_Level.text = "BONUS";
        }
    }

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
