using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : InGameObject
{
    [Header("Components")]
    public NavMeshAgent nav_Agent;
    public StateMachine<Enemy> m_StateMachine;


    [Header("Lighting")]
    public Light m_CatchLight;
    public Light m_PointLight;


    [Header("Characteristics")]
    public EnemyState m_EnemyState;
    public float m_MoveSpd;
    public float m_TurnSpd;


    [Header("Timer")]
    public float m_CatchTime;
    public float m_CatchTimeMax;

    public float m_IdleTime;
    public float m_IdleTimeMax;

    public float m_PatrolTime;
    public float m_PatrolTimeMax;



    [Header("View")]
    public Transform tf_ViewPoint;
    public float m_RangeCatch;
    public LayerMask viewMask;
    public float m_RangeAngle;
    public float m_RangeChase;


    public Character m_Char;
    Color m_CatchColorRange;

    [Header("Test")]
    public float m_PatrolRadius;


    private void OnEnable()
    {
        StartListenToEvent();

        LoadDataConfig();

        m_Char = InGameObjectsManager.Instance.m_Char;

        SetCatchLightAngle(m_RangeAngle);
        SetCatchLightRange(m_RangeCatch + 1f);
        SetNavStopDist(m_RangeCatch - 1f);
        SetNavSpd(m_MoveSpd);
        m_CatchColorRange = Color.white;

        m_StateMachine = new StateMachine<Enemy>(this);
        m_StateMachine.Init(E_IdleState.Instance);
    }

    private void OnDisable()
    {
        StopListenToEvent();
    }

    public void StartListenToEvent()
    {
        EventManager.AddListener(GameEvent.DETERMINE_CHAR, DetermineCharacter);
    }

    public void StopListenToEvent()
    {
        EventManager.RemoveListener(GameEvent.DETERMINE_CHAR, DetermineCharacter);
    }

    void Update()
    {
        m_StateMachine.ExecuteStateUpdate();
    }

    private void LoadDataConfig()
    {
        m_IdleTime = 0f;
        m_IdleTimeMax = 2f;

        m_PatrolTime = 0f;
        m_PatrolTimeMax = 2f;

        m_CatchTime = 0f;
        m_CatchTimeMax = 1.2f;

        m_RangeCatch = 2f;
        m_RangeAngle = 90f;
        m_RangeChase = 6f;

        m_PatrolRadius = 8f;

        m_TurnSpd = 1f;
        m_MoveSpd = 1.5f;
    }

    public void DetermineCharacter()
    {
        m_Char = InGameObjectsManager.Instance.m_Char;
    }

    public void SetDestination(Vector3 _des)
    {
        // nav_Agent.isStopped = false;
        nav_Agent.SetDestination(_des);
    }

    public void SetNavStopDist(float _value)
    {
        nav_Agent.stoppingDistance = _value;
    }

    public void SetNavSpd(float _value)
    {
        nav_Agent.speed = _value;
    }

    public void SetCatchLightAngle(float _value)
    {
        m_CatchLight.spotAngle = _value;
    }

    public void SetCatchLightRange(float _value)
    {
        m_CatchLight.range = _value;
    }

    public Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    public bool CanSeePlayer()
    {
        if (Helper.InRange(tf_ViewPoint.position, m_Char.tf_Owner.position, m_RangeCatch + 1.5f))
        {
            Vector3 dirToPlayer = (m_Char.tf_Owner.position - tf_ViewPoint.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(tf_ViewPoint.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < m_RangeAngle / 2.4f)
            {
                if (!Physics.Linecast(tf_ViewPoint.position, m_Char.tf_Owner.position, viewMask))
                {
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator TurnToFace(Vector3 lookTarget)
    {
        Vector3 dirToLookTarget = (lookTarget - tf_Owner.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(tf_Owner.eulerAngles.y, targetAngle)) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(tf_Owner.eulerAngles.y, targetAngle, m_TurnSpd * Time.deltaTime);
            tf_Owner.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }

    #region State

    public void ChangeState(IState<Enemy> _state)
    {
        m_StateMachine.ChangeState(_state);
    }

    #region Idle

    public void OnIdleEnter()
    {
        m_IdleTime = 0f;
        m_EnemyState = EnemyState.IDLE;
        anim_Owner.SetTrigger(ConfigKeys.e_Idle);
        // SetDestination(tf_Owner.position);
    }

    public void OnIdleExecute()
    {
        if (m_Char.m_CharState == CharState.WIN || m_Char.m_CharState == CharState.DIE)
        {
            SetDestination(tf_Owner.position);
            return;
        }

        if (Helper.InRange(tf_Owner.position, m_Char.tf_Owner.position, m_RangeChase)) //CHASING
        {
            ChangeState(E_ChaseState.Instance);
            return;
        }

        SetDestination(tf_Owner.position);

        m_IdleTime += Time.deltaTime;

        if (m_IdleTime > m_IdleTimeMax)
        {
            ChangeState(E_PatrolState.Instance);
        }
    }

    public void OnIdleExit()
    {

    }

    #endregion

    #region Patrol

    public void OnPatrolEnter()
    {
        m_PatrolTime = 0f;
        m_EnemyState = EnemyState.PATROL;
        anim_Owner.SetTrigger(ConfigKeys.e_Run);

        Vector3 newPos = RandomNavSphere(tf_Owner.position, m_PatrolRadius, -1);
        SetDestination(newPos);

        // StartCoroutine(Patrol);

        // StartCoroutine(TurnToFace());

        Debug.Log("OnPatrolEnter");
    }

    public void OnPatrolExecute()
    {
        if (Helper.InRange(tf_Owner.position, m_Char.tf_Owner.position, m_RangeChase)) //CHASING
        {
            ChangeState(E_ChaseState.Instance);
            return;
        }

        m_PatrolTime += Time.deltaTime;

        if (m_PatrolTime > m_PatrolTimeMax)
        {
            // nav_Agent.isStopped = true;
            ChangeState(E_IdleState.Instance);
        }
    }

    public void OnPatrolExit()
    {

    }


    #endregion

    #region Chase

    public void OnChaseEnter()
    {
        SetDestination(m_Char.tf_Owner.position);
        m_EnemyState = EnemyState.CHASE;
        m_CatchTime = 0f;

        anim_Owner.SetTrigger(ConfigKeys.e_Run);

        Debug.Log("OnChaseEnter");
    }

    public void OnChaseExecute()
    {
        if (m_Char.m_CharState == CharState.WIN || m_Char.m_CharState == CharState.DIE)
        {
            SetDestination(tf_Owner.position);
            return;
        }

        if (!Helper.InRange(tf_Owner.position, m_Char.tf_Owner.position, m_RangeChase)) //CHASING
        {
            ChangeState(E_IdleState.Instance);
            return;
        }

        if (m_Char.IsRunning())
        {
            SetDestination(m_Char.tf_Owner.position);
        }

        if (CanSeePlayer())
        {
            m_CatchTime += Time.deltaTime;
        }
        else
        {
            m_CatchTime -= Time.deltaTime;
        }
        m_CatchTime = Mathf.Clamp(m_CatchTime, 0, m_CatchTimeMax);
        m_CatchLight.color = Color.Lerp(m_CatchColorRange, Color.red, m_CatchTime / m_CatchTimeMax);
        m_PointLight.color = Color.Lerp(m_CatchColorRange, Color.red, m_CatchTime / m_CatchTimeMax);

        if (m_CatchTime >= m_CatchTimeMax)
        {
            ChangeState(E_CatchState.Instance);
            return;
        }
    }
    public void OnChaseExit()
    {

    }

    #endregion

    #region Chase

    public void OnCatchEnter()
    {
        EventManager.CallEvent(GameEvent.CHAR_SPOTTED);
    }

    public void OnCatchExecute()
    {

    }
    public void OnCatchExit()
    {

    }

    #endregion

    #endregion

    public void Despawn()
    {
        PrefabManager.Instance.DespawnPool(gameObject);
        Debug.Log("DESPAWNNNNNNNNNNNNNNNNNNNNNNNNN");
    }
}

public enum EnemyState
{
    IDLE = 0,
    PATROL = 1,
    CHASE = 2,
    CATCH = 3,
}