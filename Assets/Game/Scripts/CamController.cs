using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CamController : Singleton<CamController>
{
    public Transform tf_Owner;
    public Character m_Char;
    public float m_SmoothSpd;
    public Vector3 v3_Offset;
    public bool m_StartFollow;

    // private void Update()
    // {
    //     tf_Owner.position = Helper.Follow(m_Char.tf_Owner.position, tf_Owner.position, v3_Offset, m_SmoothSpd);
    // }

    private void Awake()
    {
        m_StartFollow = false;
    }

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
        // EventManager.AddListener(GameEvent.GAME_START, DetermineCharacter);
    }

    public void StopListenToEvent()
    {
        // EventManager.RemoveListener(GameEvent.GAME_START, DetermineCharacter);
    }

    private void LateUpdate()
    {
        if (m_Char != null && m_StartFollow)
        {
            tf_Owner.position = Helper.Follow(m_Char.tf_Owner.position, tf_Owner.position, v3_Offset, m_SmoothSpd);
        }
    }

    public void DetermineCharacter()
    {
        m_Char = InGameObjectsManager.Instance.m_Char;
    }

    public void ZoomInChar()
    {
        // // tf_Owner.DOMove(ConfigManager.Instance.v3_CamZoomInChar, 2f);
        // v3_Offset = Vector3.Lerp(v3_Offset, ConfigManager.Instance.v3_CamZoomInChar, Time.deltaTime);

        tf_Owner.DOMove(m_Char.tf_Owner.position + v3_Offset, 1).OnComplete(() =>
        {
            m_StartFollow = true;
            GameManager.Instance.GetPanelInGame().btn_Pause.interactable = true;
            GameManager.Instance.GetPanelInGame().JumpToPlay();
        });
    }

    public void ZoomOutChar()
    {
        // tf_Owner.DOMove(ConfigManager.Instance.v3_CamZoomOutChar, 2f);
        // float a = 0f;
        // float b = 3f;
        // float c = 3f;
        // while (a < b)
        // {
        //     a = Mathf.SmoothDamp(a, a += Time.deltaTime, ref c, 2);
        //     v3_Offset = Vector3.Lerp(v3_Offset, ConfigManager.Instance.v3_CamZoomOutChar, a);
        // }

        m_StartFollow = false;
        Vector3 pos = new Vector3(0f, 0f, 0f);
        tf_Owner.DOMove(m_Char.tf_Owner.position + v3_Offset + new Vector3(0f, -8f, 5f), 1);
    }
}
