using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSpawnPoint : SpawnPoint
{
    public override void Spawn()
    {
        GameObject go = PrefabManager.Instance.SpawnCharacter(tf_Owner.position);
        Character character = go.GetComponent<Character>();
        InGameObjectsManager.Instance.m_Char = character;

        // EventManager.CallEvent(GameEvent.SPAWN_ENEMY);
    }
}