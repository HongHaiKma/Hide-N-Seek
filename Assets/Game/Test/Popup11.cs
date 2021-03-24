using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Popup11 : UICanvas
{
    public Button btn_Popup11;
    public Button btn_Popup12;

    private void Awake()
    {
        m_ID = UIID.POPUP_11;
        Init();
    }
}
