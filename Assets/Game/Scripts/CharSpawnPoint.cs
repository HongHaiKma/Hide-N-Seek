using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSpawnPoint : SpawnPoint
{
    public override void Spawn()
    {
        string charString = InGameObjectsManager.Instance.inputChar.text;
        int charIndex = 0;

        if (charString != "")
        {
            charIndex = int.Parse(InGameObjectsManager.Instance.inputChar.text);

            if (charIndex > 5)
            {
                charIndex = PrefabManager.Instance.m_CharPrefabs.Length - 1;
            }
            else if (charIndex < 1)
            {
                charIndex = 0;
            }
            else
            {
                charIndex--;
            }
        }

        GameObject go = PrefabManager.Instance.SpawnCharacter(tf_Owner.position, charIndex);
        Character character = go.GetComponent<Character>();
        InGameObjectsManager.Instance.m_Char = character;

        // EventManager.CallEvent(GameEvent.SPAWN_ENEMY);
    }
}