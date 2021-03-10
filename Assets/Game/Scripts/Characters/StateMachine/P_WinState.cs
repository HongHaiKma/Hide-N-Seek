using UnityEngine;

public class P_WinState : IState<Character>
{
    private static P_WinState m_Instance;
    private P_WinState()
    {
        if (m_Instance != null)
        {
            return;
        }

        m_Instance = this;
    }
    public static P_WinState Instance
    {
        get
        {
            if (m_Instance == null)
            {
                new P_WinState();
            }

            return m_Instance;
        }
    }

    public void Enter(Character _charState)
    {
        _charState.OnWinEnter();
    }

    public void Execute(Character _charState)
    {
        _charState.OnWinExecute();
    }

    public void Exit(Character _charState)
    {
        _charState.OnWinExit();
    }
}