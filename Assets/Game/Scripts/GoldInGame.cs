using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GoldInGame : InGameObject
{
    public int m_Goldvalue;
    public Collider col_Owner;

    private void OnEnable()
    {
        col_Owner.enabled = true;
    }

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
                col_Owner.enabled = false;

                Vector3 aaa = new Vector3(tf_Owner.position.x, tf_Owner.position.y + 2f, tf_Owner.position.z);
                EventManagerWithParam<BigNumber>.CallEvent(GameEvent.CLAIM_GOLD_IN_GAME, m_Goldvalue);

                SoundManager.Instance.PlaySoundGetGold(transform.position);

                tf_Owner.DOMove(aaa, 0.15f).OnComplete(() =>
                {
                    PrefabManager.Instance.DespawnPool(gameObject);
                    InGameObjectsManager.Instance.m_GoldInGames.Remove(this);
                });
            }
        }
    }
}
