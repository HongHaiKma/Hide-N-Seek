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
        // GoldInGame goldInGame = PrefabManager.Instance.SpawnGoldInGamePool(m_GoldInGameKeys.ToString(), tf_Owner.position).GetComponent<GoldInGame>();
        GameObject goldInGame = PrefabManager.Instance.SpawnGoldInGamePool(m_GoldInGameKeys.ToString(), tf_Owner.position);
        goldInGame.transform.localPosition = new Vector3(tf_Owner.position.x, 0.5f, tf_Owner.position.z);
        // goldInGame.transform.localScale = new Vector3(0.075f, 0.075f, 0.075f);
        goldInGame.transform.rotation = new Quaternion(90f, 0f, 0f, 0f);
    }
}
