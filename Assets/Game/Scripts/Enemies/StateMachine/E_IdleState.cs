using UnityEngine;

public class E_IdleState : IState<Enemy>
{
    private static E_IdleState m_Instance;
    private E_IdleState()
    {
        if (m_Instance != null)
        {
            return;
        }

        m_Instance = this;
    }
    public static E_IdleState Instance
    {
        get
        {
            if (m_Instance == null)
            {
                new E_IdleState();
            }

            return m_Instance;
        }
    }

    public void Enter(Enemy _charState)
    {
        _charState.OnIdleEnter();
    }

    public void Execute(Enemy _charState)
    {
        _charState.OnIdleExecute();
    }

    public void Exit(Enemy _charState)
    {
        _charState.OnIdleExit();
    }
}