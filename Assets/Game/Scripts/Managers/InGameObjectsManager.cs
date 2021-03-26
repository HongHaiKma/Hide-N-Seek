using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameObjectsManager : Singleton<InGameObjectsManager>
{
    public Character m_Char;
    public List<Enemy> m_Enemies;
    public MapController m_Map;

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
    }

    public void StopListenToEvent()
    {
        EventManagerWithParam<int>.RemoveListener(GameEvent.CHAR_CLAIM_KEYKEY, SpawnKey);
    }

    public void DestroyMap()
    {
        if (m_Map != null)
        {
            Destroy(m_Map.gameObject);
            Destroy(m_Char.gameObject);
        }
    }

    public void LoadMap()
    {
        RemoveEnemies();

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
    }

    public void LoadMapCheat(int _level)
    {
        RemoveEnemies();

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
        ProfileManager.SetLevel(_level);
        string name = "Maps/Map" + _level.ToString();
        GameObject go = Resources.Load<GameObject>(name);
        GameObject map = Instantiate(go, Vector3.zero, Quaternion.identity);
        MapController mapControl = map.GetComponent<MapController>();
        mapControl.nav_Surface.BuildNavMesh();
        // mapControl.nav_Surface.UpdateNavMesh(mapControl.nav_Surface.navMeshData);
        m_Map = mapControl;

        m_Map.SetupMap();
    }

    public void SpawnChar()
    {
        m_Char.gameObject.SetActive(true);
    }

    public void RemoveEnemies()
    {
        for (int i = 0; i < m_Enemies.Count; i++)
        {
            m_Enemies[i].Despawn();
        }

        m_Enemies.Clear();
    }

    public void SpawnKey(int _value)
    {
        m_Map.SpawnKey(_value);
    }

    public void TestRelease()
    {
        // SimplePool.Release();
        RemoveEnemies();
        SimplePool.Release();
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }
}
