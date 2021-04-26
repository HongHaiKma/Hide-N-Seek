using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTutorial : UICanvas
{
    private void Awake()
    {
        m_ID = UIID.POPUP_TUTORIAL;
        Init();
    }

    public void Setup(TutorialType _tut)
    {
        //Handle HAND IMAGE
    }
}
