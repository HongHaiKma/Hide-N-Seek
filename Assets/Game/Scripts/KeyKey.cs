using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KeyKey : InGameObject
{
    public static int m_KeyNo;

    void Update()
    {
        transform.Rotate(Vector3.up * (450f * Time.deltaTime));
    }

    public void SetActive(bool _value)
    {
        gameObject.SetActive(_value);
    }

    private void OnTriggerEnter(Collider other)
    {
        InGameObject obj = other.GetComponent<InGameObject>();

        if (obj != null)
        {
            if (obj.m_ObjectType == ObjectType.CHAR)
            {
                gameObject.SetActive(false);
                m_KeyNo++;
                EventManagerWithParam<int>.CallEvent(GameEvent.CHAR_CLAIM_KEYKEY, m_KeyNo);

                SoundManager.Instance.PlaySoundGetGold(transform.position);
            }
        }
    }
}
