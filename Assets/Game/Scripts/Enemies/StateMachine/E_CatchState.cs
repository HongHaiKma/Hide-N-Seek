using UnityEngine;

public class E_CatchState : IState<Enemy>
{
    private static E_CatchState m_Instance;
    private E_CatchState()
    {
        if (m_Instance != null)
        {
            return;
        }

        m_Instance = this;
    }
    public static E_CatchState Instance
    {
        get
        {
            if (m_Instance == null)
            {
                new E_CatchState();
            }

            return m_Instance;
        }
    }

    public void Enter(Enemy _charState)
    {
        _charState.OnCatchEnter();
    }

    public void Execute(Enemy _charState)
    {
        _charState.OnCatchExecute();
    }

    public void Exit(Enemy _charState)
    {
        _charState.OnCatchExit();
    }
}