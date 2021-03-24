using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panelingame : MonoBehaviour
{
    public Button btn_Popup1;
    public Button btn_Popup2;
    public Button btn_Popup3;

    private void Awake()
    {
        GUIManager1.Instance.AddClickEvent(btn_Popup1, OnOpenPopup1);
        GUIManager1.Instance.AddClickEvent(btn_Popup2, OnOpenPopup2);
        GUIManager1.Instance.AddClickEvent(btn_Popup3, OnOpenPopup3);

        // if (GUIManager1.Instance == null)
        // {
        //     Helper.DebugLog("GUIManager1 is nullllllllllllllllllllllllllllll");
        // }
    }

    public void OnOpenPopup1()
    {
        // if (GUIManager1.Instance != null)
        // {
        Popup1 popup = GUIManager1.Instance.GetUICanvasByID(UIID.POPUP_1) as Popup1;

        // popup.Setup();
        GUIManager1.Instance.ShowUIPopup(popup);
        // }
    }

    public void OnOpenPopup2()
    {
        Debug.Log("OnOpenPopup22222222222");
    }

    public void OnOpenPopup3()
    {
        Debug.Log("OnOpenPopup33333333333");
    }
}
