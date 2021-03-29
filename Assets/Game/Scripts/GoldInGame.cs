using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldInGame : InGameObject
{
    public int m_Goldvalue;

    void Update()
    {
        transform.Rotate(Vector3.up * (450f * Time.deltaTime));
    }

    public void Despawn()
    {
        PrefabManager.Instance.DespawnPool(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        InGameObject obj = other.GetComponent<InGameObject>();

        if (obj != null)
        {
            if (obj.m_ObjectType == ObjectType.CHAR)
            {
                PrefabManager.Instance.DespawnPool(gameObject);
                ProfileManager.AddGold((BigNumber)m_Goldvalue);
                // EventManagerWithParam<int>.CallEvent(GameEvent.CHAR_CLAIM_KEYKEY, m_KeyNo);
                // InGameObjectsManager.Instance.m_Map.SpawnKey(m_KeyNo);
            }
        }
    }
}
