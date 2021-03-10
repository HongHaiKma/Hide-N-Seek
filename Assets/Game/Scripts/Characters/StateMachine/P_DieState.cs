using UnityEngine;

public class P_DieState : IState<Character>
{
    private static P_DieState m_Instance;
    private P_DieState()
    {
        if (m_Instance != null)
        {
            return;
        }

        m_Instance = this;
    }
    public static P_DieState Instance
    {
        get
        {
            if (m_Instance == null)
            {
                new P_DieState();
            }

            return m_Instance;
        }
    }

    public void Enter(Character _charState)
    {
        _charState.OnDieEnter();
    }

    public void Execute(Character _charState)
    {
        _charState.OnDieExecute();
    }

    public void Exit(Character _charState)
    {
        _charState.OnDieExit();
    }
}