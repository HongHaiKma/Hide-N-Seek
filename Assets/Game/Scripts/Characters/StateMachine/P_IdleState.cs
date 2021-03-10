using UnityEngine;

public class P_IdleState : IState<Character>
{
    private static P_IdleState m_Instance;
    private P_IdleState()
    {
        if (m_Instance != null)
        {
            return;
        }

        m_Instance = this;
    }
    public static P_IdleState Instance
    {
        get
        {
            if (m_Instance == null)
            {
                new P_IdleState();
            }

            return m_Instance;
        }
    }

    public void Enter(Character _charState)
    {
        _charState.OnIdleEnter();
    }

    public void Execute(Character _charState)
    {
        _charState.OnIdleExecute();
    }

    public void Exit(Character _charState)
    {
        _charState.OnIdleExit();
    }
}