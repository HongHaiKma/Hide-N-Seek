using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameObjectsManager : Singleton<InGameObjectsManager>
{
    public Character m_Char;
    public List<Enemy> m_Enemies;
    public MapController m_Map;

    public void DestroyMap()
    {
        if (m_Map != null)
        {
            Destroy(m_Map.gameObject);
            Destroy(m_Char.gameObject);
        }
    }

    public void TestLoadMap3()
    {
        // EventManager.CallEvent(GameEvent.DESPAWN_ENEMY);

        RemoveEnemies();

        // SimplePool.Release();
        // Resources.UnloadUnusedAssets();
        // System.GC.Collect();

        if (m_Map != null)
        {
            Destroy(m_Map.gameObject);
            Destroy(m_Char.gameObject);
        }

        string name = "Maps/Level3";
        GameObject go = Resources.Load<GameObject>(name);
        GameObject map = Instantiate(go, Vector3.zero, Quaternion.identity);
        MapController mapControl = map.GetComponent<MapController>();
        m_Map = mapControl;
        m_Map.SetupMap();

        EventManager.CallEvent(GameEvent.DETERMINE_CHAR);
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

    public void TestRelease()
    {
        // SimplePool.Release();
        RemoveEnemies();
        SimplePool.Release();
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }
}
