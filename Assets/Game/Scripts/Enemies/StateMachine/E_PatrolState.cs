using UnityEngine;

public class E_PatrolState : IState<Enemy>
{
    private static E_PatrolState m_Instance;
    private E_PatrolState()
    {
        if (m_Instance != null)
        {
            return;
        }

        m_Instance = this;
    }
    public static E_PatrolState Instance
    {
        get
        {
            if (m_Instance == null)
            {
                new E_PatrolState();
            }

            return m_Instance;
        }
    }

    public void Enter(Enemy _charState)
    {
        _charState.OnPatrolEnter();
    }

    public void Execute(Enemy _charState)
    {
        _charState.OnPatrolExecute();
    }

    public void Exit(Enemy _charState)
    {
        _charState.OnPatrolExit();
    }
}