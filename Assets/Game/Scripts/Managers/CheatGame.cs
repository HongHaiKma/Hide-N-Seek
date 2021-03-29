using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatGame : MonoBehaviour
{
    public GameObject g_CheatPanel;
    public InputField m_Level;
    public PanelInGame m_PanelInGame;

    public void OpenCheatPanel()
    {
        g_CheatPanel.SetActive(!g_CheatPanel.activeInHierarchy);
    }

    public void JumpLevel()
    {
        int jumpLevel = int.Parse(m_Level.text);
        InGameObjectsManager.Instance.LoadMapCheat(jumpLevel);

        CamController.Instance.m_Char = InGameObjectsManager.Instance.m_Char;
        GameManager.Instance.m_LevelStart = false;
        m_PanelInGame.SetOutGame();
    }
}
