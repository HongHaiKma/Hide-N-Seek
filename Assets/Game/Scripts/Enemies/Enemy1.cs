using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy1 : Enemy
{
    public override void LoadDataConfig()
    {
        base.LoadDataConfig();
        m_IdleTimeMax = 3f;
    }

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
            if (ProfileManager.GetLevel() > 3)
            {
                ChangeState(E_PatrolState.Instance);
            }
            else
            {
                m_IdleTime = 0f;
                tf_Owner.DORotate(new Vector3(tf_Owner.position.x, Random.Range(-360, 360), tf_Owner.position.z), 1.5f);
            }
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
        else
        {

        }
    }

    public override void SetFOV()
    {
        // m_CatchColorRange = Color.red;
        // m_FOV.defaultColor = Color.red;
        // m_FOV.normalColor = Color.red;
        // m_FOV.normalMat.color = Color.red;
        // m_FOV.SetFieldOfView(m_RangeAngle + 10f);
    }
}