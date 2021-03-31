using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSpawnPoint : SpawnPoint
{
    public override void Spawn()
    {
        // PanelInGame panelInGame = FindObjectOfType<PanelInGame>().GetComponent<PanelInGame>();

        // string charString = panelInGame.inputChar.text;
        // // string charString = InGameObjectsManager.Instance.inputChar.text;
        // int charIndex = 0;

        // if (charString != "")
        // {
        //     charIndex = int.Parse(charString);

        //     if (charIndex > 5)
        //     {
        //         charIndex = PrefabManager.Instance.m_CharPrefabs.Length - 1;
        //     }
        //     else if (charIndex < 1)
        //     {
        //         charIndex = 0;
        //     }
        //     else
        //     {
        //         charIndex--;
        //     }
        // }

        int charId = ProfileManager.GetSelectedCharacter() - 1;

        GameObject go = PrefabManager.Instance.SpawnCharacter(tf_Owner.position, charId);
        Character character = go.GetComponent<Character>();
        InGameObjectsManager.Instance.m_Char = character;
        CamController.Instance.m_Char = character;
    }
}