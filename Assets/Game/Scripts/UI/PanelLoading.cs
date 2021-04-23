using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelLoading : MonoBehaviour
{
    public Image img_LoadingBar;
    public Text txt_VersionCode;

    private void Start()
    {
        txt_VersionCode.text = "v" + Application.version;
    }
}
