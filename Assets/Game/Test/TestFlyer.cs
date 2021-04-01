using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFlyer : MonoBehaviour
{
    public Sprite m_Sprite;
    public Flyer m_Flyer;
    public Transform m_Target;

    public void TestFly()
    {
        m_Flyer.FlyToTargetOneSide(m_Target.position, () =>
        {

        }
        , true, -1, 0.3f);
    }
}
