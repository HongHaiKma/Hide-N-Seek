using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJoint : MonoBehaviour
{
    private float m_CollideCdMax = 1f;
    private float m_CollideCd = 0f;

    private void OnEnable()
    {
        m_CollideCd = 1f;
    }

    private void Update()
    {
        if (m_CollideCd < m_CollideCdMax)
        {
            m_CollideCd += Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Obstacle Dynamic"))
        {
            if (m_CollideCd >= m_CollideCdMax)
            {
                Helper.DebugLog("Obstacle Dynamic: " + other.transform.CompareTag("Obstacle Dynamic"));
                SoundManager.Instance.PlaySoundObstacleDynamic(other.transform.position);
                m_CollideCd = 0f;
                return;
            }
        }
    }
}
