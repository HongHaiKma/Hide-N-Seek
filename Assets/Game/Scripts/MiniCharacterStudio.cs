using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCharacterStudio : Singleton<MiniCharacterStudio>
{
    public GameObject g_Char;

    private void OnEnable()
    {
        StartListenToEvent();
    }

    private void Disable()
    {
        StopListenToEvent();
    }

    public void StartListenToEvent()
    {
        EventManagerWithParam<int>.AddListener(GameEvent.LOAD_OUTFIT_CHARACTER, SetChar);
    }

    public void StopListenToEvent()
    {
        EventManagerWithParam<int>.RemoveListener(GameEvent.LOAD_OUTFIT_CHARACTER, SetChar);
    }

    public void SetChar(int _id)
    {
        Vector3 a = new Vector3(0f, 0f, 0f);
        if (g_Char == null)
        {
            g_Char = PrefabManager.Instance.SpawnMiniCharacterStudio(a, _id);
            g_Char.transform.SetParent(gameObject.transform);
            g_Char.transform.localPosition = a;
        }
        else
        {
            Destroy(g_Char);
            g_Char = null;
            g_Char = PrefabManager.Instance.SpawnMiniCharacterStudio(a, _id);
            g_Char.transform.SetParent(gameObject.transform);
            g_Char.transform.localPosition = a;
        }
    }
}
