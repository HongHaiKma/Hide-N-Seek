using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [DefaultExecutionOrder(-5)]
public class MiniCharacterStudio : Singleton<MiniCharacterStudio>
{
    // private static MiniCharacterStudio m_Instance;
    // public static MiniCharacterStudio Instance
    // {
    //     get
    //     {
    //         return m_Instance;
    //     }
    // }

    public GameObject g_Char;

    // private void Awake()
    // {
    //     if (m_Instance != null)
    //     {
    //         DestroyImmediate(gameObject);
    //     }
    //     else
    //     {
    //         m_Instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    // }

    private void OnEnable()
    {
        Helper.DebugLog("MiniCharacterStudio OnEnableeeeeeeeeeeeeeeeeeeee");
        StartListenToEvent();
    }

    private void OnDisable()
    {
        StopListenToEvent();
    }

    private void OnDestroy()
    {
        StopListenToEvent();
    }

    public void StartListenToEvent()
    {
        EventManagerWithParam<int>.AddListener(GameEvent.LOAD_OUTFIT_CHARACTER, SpawnMiniCharacterIdle);
    }

    public void StopListenToEvent()
    {
        EventManagerWithParam<int>.RemoveListener(GameEvent.LOAD_OUTFIT_CHARACTER, SpawnMiniCharacterIdle);
    }

    public void DestroyChar()
    {
        Destroy(g_Char);
        g_Char = null;
    }

    public void SpawnMiniCharacterIdle(int _id)
    {
        Vector3 a = new Vector3(0f, 0f, 0f);

        if (g_Char != null)
        {
            Helper.DebugLog("MiniCharacterStudio destroyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy");
            Destroy(g_Char);
            g_Char = null;
        }

        g_Char = PrefabManager.Instance.SpawnMiniCharacterStudio(a, _id);
        g_Char.transform.SetParent(gameObject.transform);
        g_Char.transform.localPosition = a;
        g_Char.GetComponent<MiniCharacter>().m_Anim.SetTrigger("Idle");

        Helper.DebugLog("SpawnMiniCharacterIdle");
    }

    public void SpawnMiniCharacter(string _anim)
    {
        int _id = ProfileManager.GetSelectedCharacter();

        Vector3 a = new Vector3(0f, 0f, 0f);

        if (g_Char != null)
        {
            Destroy(g_Char);
            g_Char = null;
        }

        g_Char = PrefabManager.Instance.SpawnMiniCharacterStudio(a, _id);
        g_Char.transform.SetParent(gameObject.transform);
        g_Char.transform.localPosition = a;
        g_Char.GetComponent<MiniCharacter>().m_Anim.SetTrigger(_anim);
    }
}
