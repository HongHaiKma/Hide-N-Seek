using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestTween : MonoBehaviour
{
    public Transform tf_Key;
    public Transform tf_Keys;

    public void TestScale()
    {
        // rect_Key.DOScale(new Vector3(2f, 2f, 2f), 1.5f);

        // Sequence mySequence = DOTween.Sequence();
        // mySequence.Append(tf_Key.DOScale(new Vector3(2f, 2f, 2f), 0.25f));
        // mySequence.Append(tf_Key.DOScale(new Vector3(1f, 1f, 1f), 0.25f));

        // chay thu cai nay di
        tf_Keys.DOKill();
        tf_Keys.DOMove(tf_Key.position, 3f).OnComplete
        (
            () => ABC()
        );

    }

    public void ABC()
    {
        Debug.Log("1111111111111111111");
        Destroy(tf_Keys.gameObject);
    }
}
