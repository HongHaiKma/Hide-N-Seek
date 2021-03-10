using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : SpawnPoint
{
    public EnemyKeys m_EnemyKeys;

    public override void Spawn()
    {
        Enemy enemy = PrefabManager.Instance.SpawnEnemyPool(m_EnemyKeys.ToString(), tf_Owner.position).GetComponent<Enemy>();
        InGameObjectsManager.Instance.m_Enemies.Add(enemy);
    }
}
