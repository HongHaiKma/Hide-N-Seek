using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupOutfit : UICanvas
{
    private void Awake()
    {
        m_ID = UIID.POPUP_OUTFIT;
        Init();
    }

    private void OnEnable()
    {

    }
}
