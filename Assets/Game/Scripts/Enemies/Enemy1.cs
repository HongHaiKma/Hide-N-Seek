using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{
    public override void OnIdleExecute()
    {
        if (CanSeePlayer() && !IsThroughWall() && (Mathf.Abs(tf_Owner.position.y - m_Char.tf_Owner.position.y) < 0.5f))
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
        if (CanSeePlayer() && !IsThroughWall() && (Mathf.Abs(tf_Owner.position.y - m_Char.tf_Owner.position.y) < 0.5f))
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

    public override void SetFOV()
    {
        m_CatchColorRange = Color.red;
        m_FOV.defaultColor = Color.red;
        m_FOV.normalColor = Color.red;
        // m_FOV.normalMat.color = Color.red;
        m_FOV.SetFieldOfView(m_RangeAngle + 10f);
    }
}