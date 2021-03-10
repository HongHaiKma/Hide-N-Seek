using UnityEngine;

public class E_ChaseState : IState<Enemy>
{
    private static E_ChaseState m_Instance;
    private E_ChaseState()
    {
        if (m_Instance != null)
        {
            return;
        }

        m_Instance = this;
    }
    public static E_ChaseState Instance
    {
        get
        {
            if (m_Instance == null)
            {
                new E_ChaseState();
            }

            return m_Instance;
        }
    }

    public void Enter(Enemy _charState)
    {
        _charState.OnChaseEnter();
    }

    public void Execute(Enemy _charState)
    {
        _charState.OnChaseExecute();
    }

    public void Exit(Enemy _charState)
    {
        _charState.OnChaseExit();
    }
}