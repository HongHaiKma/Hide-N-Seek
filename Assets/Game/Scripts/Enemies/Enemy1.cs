using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{
    public override void OnIdleExecute()
    {
        if (CanSeePlayer() && !IsThroughWall())
        {
            ChangeState(E_CatchState.Instance);
            return;
        }

        if (m_Char.m_CharState == CharState.WIN || m_Char.m_CharState == CharState.DIE)
        {
            SetDestination(tf_Owner.position);
            return;
        }

        SetDestination(tf_Owner.position);

        m_IdleTime += Time.deltaTime;

        if (m_IdleTime > m_IdleTimeMax)
        {
            ChangeState(E_PatrolState.Instance);
        }
    }

    public override void OnPatrolExecute()
    {
        if (CanSeePlayer() && !IsThroughWall())
        {
            ChangeState(E_CatchState.Instance);
            return;
        }

        m_PatrolTime += Time.deltaTime;

        if (m_PatrolTime > m_PatrolTimeMax)
        {
            // nav_Agent.isStopped = true;
            ChangeState(E_IdleState.Instance);
        }
    }


}
