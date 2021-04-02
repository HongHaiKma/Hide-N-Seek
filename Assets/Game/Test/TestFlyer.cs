using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestFlyer : MonoBehaviour
{
    public Sprite m_Sprite;
    public Flyer m_Flyer;
    public Transform m_Target;
    public Image img_Bar;

    public void TestFly()
    {
        m_Flyer.FlyToTargetOneSide(m_Target.position, () =>
        {

        }
        , true, -1, 0.3f);
    }

    public void TestFill()
    {
        StartCoroutine(IETestFill());
    }

    public IEnumerator IETestFill()
    {
        img_Bar.fillAmount = 0f;

        float time1 = 0f;
        while (time1 < 1f)
        {
            time1 += Time.deltaTime;
            img_Bar.fillAmount = Mathf.Lerp(0f, 0.9f, (time1) / 1f);

            yield return null;
        }

        img_Bar.fillAmount = 0.9f;
    }
}
