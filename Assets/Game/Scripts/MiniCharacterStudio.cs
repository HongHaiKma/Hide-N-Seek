using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCharacterStudio : Singleton<MiniCharacterStudio>
{
    public GameObject g_Char;

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
