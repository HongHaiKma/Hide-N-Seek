using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldInGameSpawnPoint : SpawnPoint
{
    public GoldInGameKeys m_GoldInGameKeys;

    private void Awake()
    {
        Spawn();
    }

    public override void Spawn()
    {
        GameObject goldInGame = PrefabManager.Instance.SpawnGoldInGamePool(m_GoldInGameKeys.ToString(), tf_Owner.position);
        GoldInGame goldInGame2 = goldInGame.GetComponent<GoldInGame>();

        if (goldInGame2 == null)
        {
            Helper.DebugLog("Nullllllllllllllllllllllllllllllllllllll");
        }

        InGameObjectsManager.Instance.m_GoldInGames.Add(goldInGame2);
        goldInGame.transform.SetParent(transform.transform);
        goldInGame.transform.localPosition = new Vector3(0f, 2f, 0f);
        goldInGame.transform.rotation = new Quaternion(90f, 0f, 0f, 0f);
    }
}
