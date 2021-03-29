using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : Singleton<PrefabManager>
{
    public GameObject[] m_CharPrefabs;

    private Dictionary<string, GameObject> m_IngameObjectPrefabDict = new Dictionary<string, GameObject>();
    public GameObject[] m_IngameObjectPrefabs;

    private Dictionary<string, GameObject> m_EnemyPrefabDict = new Dictionary<string, GameObject>();
    public GameObject[] m_EnemyPrefabs;

    private Dictionary<string, GameObject> m_GoldInGamePrefabDict = new Dictionary<string, GameObject>();
    public GameObject[] m_GoldInGamePrefabs;

    private void Awake()
    {
        InitPrefab();
        InitIngamePrefab();
    }

    public void InitPrefab()
    {
        for (int i = 0; i < m_EnemyPrefabs.Length; i++)
        {
            GameObject iPrefab = m_EnemyPrefabs[i];
            if (iPrefab == null) continue;
            string iName = iPrefab.name;
            try
            {
                m_EnemyPrefabDict.Add(iName, iPrefab);
            }
            catch (System.Exception)
            {
                continue;
            }
        }
        for (int i = 0; i < m_GoldInGamePrefabs.Length; i++)
        {
            GameObject iPrefab = m_GoldInGamePrefabs[i];
            if (iPrefab == null) continue;
            string iName = iPrefab.name;
            try
            {
                m_GoldInGamePrefabDict.Add(iName, iPrefab);
            }
            catch (System.Exception)
            {
                continue;
            }
        }
    }

    public void InitIngamePrefab()
    {
        string enemy1 = EnemyKeys.Enemy1.ToString();
        CreatePool(enemy1, GetEnemyPrefabByName(enemy1), 2);
        string enemy2 = EnemyKeys.Enemy2.ToString();
        CreatePool(enemy2, GetEnemyPrefabByName(enemy2), 2);
        string enemy3 = EnemyKeys.Enemy3.ToString();
        CreatePool(enemy3, GetEnemyPrefabByName(enemy3), 2);
        string enemy4 = EnemyKeys.Enemy4.ToString();
        CreatePool(enemy4, GetEnemyPrefabByName(enemy4), 2);
        string enemy5 = EnemyKeys.Enemy5.ToString();
        CreatePool(enemy5, GetEnemyPrefabByName(enemy5), 2);

        string gold1 = GoldInGameKeys.GoldInGame1.ToString();
        CreatePool(gold1, GetGoldInGamePrefabByName(gold1), 5);
        string gold5 = GoldInGameKeys.GoldInGame5.ToString();
        CreatePool(gold5, GetGoldInGamePrefabByName(gold5), 5);
        string gold10 = GoldInGameKeys.GoldInGame10.ToString();
        CreatePool(gold10, GetGoldInGamePrefabByName(gold10), 5);
    }

    public void CreatePool(string name, GameObject prefab, int amount)
    {
        SimplePool.Preload(prefab, amount, name);
    }

    public GameObject SpawnPool(string name, Vector3 pos)
    {
        if (SimplePool.IsHasPool(name))
        {
            GameObject go = SimplePool.Spawn(name, pos, Quaternion.identity);
            return go;
        }
        else
        {
            GameObject prefab = GetPrefabByName(name);
            if (prefab != null)
            {
                SimplePool.Preload(prefab, 1, name);
                GameObject go = SpawnPool(name, pos);
                return go;
            }
        }
        return null;
    }

    public GameObject GetPrefabByName(string name)
    {
        GameObject rPrefab = null;
        if (m_IngameObjectPrefabDict.TryGetValue(name, out rPrefab))
        {
            return rPrefab;
        }
        return null;
    }

    public GameObject GetGoldInGamePrefabByName(string name)
    {
        if (m_GoldInGamePrefabDict.ContainsKey(name))
        {
            return m_GoldInGamePrefabDict[name];
        }
        return null;
    }

    public GameObject SpawnGoldInGamePool(string name, Vector3 pos)
    {
        if (SimplePool.IsHasPool(name))
        {
            GameObject go = SimplePool.Spawn(name, pos, Quaternion.identity);
            return go;
        }
        else
        {
            GameObject prefab = GetGoldInGamePrefabByName(name);
            if (prefab != null)
            {
                SimplePool.Preload(prefab, 1, name);
                GameObject go = SpawnPool(name, pos);
                return go;
            }
        }
        return null;
    }

    public GameObject GetEnemyPrefabByName(string name)
    {
        if (m_EnemyPrefabDict.ContainsKey(name))
        {
            return m_EnemyPrefabDict[name];
        }
        return null;
    }

    public GameObject SpawnEnemyPool(string name, Vector3 pos)
    {
        if (SimplePool.IsHasPool(name))
        {
            GameObject go = SimplePool.Spawn(name, pos, Quaternion.identity);
            return go;
        }
        else
        {
            GameObject prefab = GetEnemyPrefabByName(name);
            if (prefab != null)
            {
                SimplePool.Preload(prefab, 1, name);
                GameObject go = SpawnPool(name, pos);
                return go;
            }
        }
        return null;
    }

    public GameObject SpawnCharacter(Vector3 _pos, int _index)
    {
        return Instantiate(m_CharPrefabs[_index], _pos, Quaternion.identity);
    }

    public void DespawnPool(GameObject go)
    {
        SimplePool.Despawn(go);
    }
}
