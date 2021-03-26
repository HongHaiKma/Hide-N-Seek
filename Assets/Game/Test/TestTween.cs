using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TestTween : MonoBehaviour
{
    public Transform tf_Key;
    public Transform tf_Keys;
    public Text aaa;

    public Transform tf_Cube;
    public Transform tf_Cube2;

    public void TestScale()
    {
        // rect_Key.DOScale(new Vector3(2f, 2f, 2f), 1.5f);

        // Sequence mySequence = DOTween.Sequence();
        // mySequence.Append(tf_Key.DOScale(new Vector3(2f, 2f, 2f), 0.25f));
        // mySequence.Append(tf_Key.DOScale(new Vector3(1f, 1f, 1f), 0.25f));

        // tf_Keys.DOKill();
        // tf_Keys.DOMove(tf_Key.position, 3f).OnComplete
        // (
        //     () => aaa.text = "StringAlo"
        // );

        // transform.DOMoveX(3, 2).SetEase(Ease.OutQuad);
        // transform.DOMoveY(3, 2).SetEase(Ease.InQuad);

        // tf_Cube.DOMove(tf_Cube2.position, 3f);

        // Sequence mySequence = DOTween.Sequence();
        // // mySequence.Append(tf_Cube.DOMoveY(-5, 3).SetEase(Ease.InQuad));
        // mySequence.Append(tf_Cube.DOMoveY(tf_Cube2.position.y, 3f).SetEase(Ease.InOutQuart));

        // tf_Cube.DOBlendableMoveBy(new Vector3(3, 0, 0), 2);
        // tf_Cube.DOBlendableMoveBy(new Vector3(0, 3, 0), 1).SetLoops(2, LoopType.Yoyo);

        // tf_Cube.DOMoveX(tf_Cube2.position.x, 2).SetEase(Ease.OutQuad);
        // tf_Cube.DOMoveY(tf_Cube2.position.y, 2).SetEase(Ease.InQuad);
        // tf_Cube.DOMoveZ(tf_Cube2.position.z, 2);

        // tf_Cube.DOMove(tf_Cube2.position, 3f);
    }

    public void ABC()
    {
        Debug.Log("1111111111111111111");
        Destroy(tf_Keys.gameObject);
    }
}
