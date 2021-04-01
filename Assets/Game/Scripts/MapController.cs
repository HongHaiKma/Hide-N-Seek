using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapController : MonoBehaviour
{
    public MapType m_MapType;
    public List<KeyKey> m_Keys;
    public Door m_Door;
    public BigNumber m_LevelGold;

    public List<SpawnPoint> m_SpawnPoints;

    public NavMeshSurface nav_Surface;

    private void OnEnable()
    {
        StartListenToEvent();
    }

    private void OnDisable()
    {
        StopListenToEvent();
    }

    public void StartListenToEvent()
    {
        EventManager.AddListener(GameEvent.LEVEL_START, SpawnEnemies);
    }

    public void StopListenToEvent()
    {
        EventManager.RemoveListener(GameEvent.LEVEL_START, SpawnEnemies);
    }

    public void SetupMap()
    {
        // Instantiate(PrefabManager.Instantiate.);

        SetupKeysAndDoor();

        SpawnChar();
    }

    public void SpawnKey(int _value)
    {
        float amount = m_Keys.Count;
        if (_value >= amount)
        {
            m_Door.SetActive(true);
            return;
        }
        m_Keys[_value].SetActive(true);
    }

    public void SetupKeysAndDoor()
    {
        for (int i = 0; i < m_Keys.Count; i++)
        {
            m_Keys[i].SetActive(false);
        }
        m_Door.SetActive(false);

        KeyKey.m_KeyNo = 0;
        SpawnKey(KeyKey.m_KeyNo);
    }

    public void SpawnChar()
    {
        m_SpawnPoints[0].Spawn();
    }

    public void SpawnEnemies()
    {
        for (int i = 1; i < m_SpawnPoints.Count; i++)
        {
            m_SpawnPoints[i].Spawn();
        }
    }
}

public enum MapType
{
    KEY,
    BONUS
}