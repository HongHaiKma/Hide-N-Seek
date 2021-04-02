using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPanel : Singleton<BlockPanel>
{
    public GameObject g_BlackPanel;
    public Canvas m_Canvas;

    public void SetupBlock(int _order)
    {
        g_BlackPanel.SetActive(true);
        m_Canvas.sortingOrder = _order;
    }

    public void Close()
    {
        g_BlackPanel.SetActive(false);
    }
}
