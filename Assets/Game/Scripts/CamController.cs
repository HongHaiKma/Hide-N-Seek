using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : Singleton<CamController>
{
    public Transform tf_Owner;
    public Rigidbody rb_Owner;
    public Character m_Char;
    public float m_SmoothSpd;
    public Vector3 v3_Offset;

    // private void Update()
    // {
    //     tf_Owner.position = Helper.Follow(m_Char.tf_Owner.position, tf_Owner.position, v3_Offset, m_SmoothSpd);
    // }

    private void OnEnable()
    {
        StartListenToEvent();
    }

    private void OnDisable()
    {
        StopListenToEvent();
    }

    public void StartListenToEvent()
    {
        EventManager.AddListener(GameEvent.DETERMINE_CHAR, DetermineCharacter);
    }

    public void StopListenToEvent()
    {
        EventManager.RemoveListener(GameEvent.DETERMINE_CHAR, DetermineCharacter);
    }

    private void LateUpdate()
    {
        if (m_Char != null)
        {
            tf_Owner.position = Helper.Follow(m_Char.tf_Owner.position, tf_Owner.position, v3_Offset, m_SmoothSpd);
        }
    }

    public void DetermineCharacter()
    {
        m_Char = InGameObjectsManager.Instance.m_Char;
    }
}
