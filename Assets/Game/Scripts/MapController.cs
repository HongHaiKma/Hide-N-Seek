using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<KeyKey> m_Keys;
    public Door m_Door;

    public List<SpawnPoint> m_SpawnPoints;

    public void SetupMap()
    {
        // Instantiate(PrefabManager.Instantiate.);

        SetupKeysAndDoor();

        SpawnEnemiesAndChar();
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

    public void SpawnEnemiesAndChar()
    {
        for (int i = 0; i < m_SpawnPoints.Count; i++)
        {
            m_SpawnPoints[i].Spawn();
        }
    }
}
