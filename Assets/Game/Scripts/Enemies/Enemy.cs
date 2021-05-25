using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class Enemy : InGameObject
{
    [Header("Components")]
    public NavMeshAgent nav_Agent;
    public StateMachine<Enemy> m_StateMachine;
    // public FieldOfView m_FOV;
    public AIConeDetection m_FOV;

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
    public Color m_CatchColorRange;

    [Header("Test")]
    public float m_PatrolRadius;
    public Transform tf_RayStartPoint;


    private void OnEnable()
    {
        StartListenToEvent();

        LoadDataConfig();

        m_Char = InGameObjectsManager.Instance.m_Char;

        // SetNavStopDist(m_RangeCatch - 1f);
        SetNavUpdatePosition(true);
        SetNavStopDist(0.6f);
        SetNavSpd(m_MoveSpd);

        SetFOV();

        if (m_StateMachine == null)
        {
            m_StateMachine = new StateMachine<Enemy>(this);
        }
        m_StateMachine.Init(E_IdleState.Instance);
    }

    private void OnDisable()
    {
        StopListenToEvent();
    }

    private void OnDestroy()
    {
        StopListenToEvent();
    }

    public void StartListenToEvent()
    {
        EventManager.AddListener(GameEvent.LEVEL_START, DetermineCharacter);
    }

    public void StopListenToEvent()
    {
        EventManager.RemoveListener(GameEvent.LEVEL_START, DetermineCharacter);
    }

    void Update()
    {
        m_StateMachine.ExecuteStateUpdate();

        Vector3 dir = (m_Char.tf_RayStartPoint.position - tf_RayStartPoint.position).normalized;
    }

    public virtual void LoadDataConfig()
    {
        m_IdleTime = 0f;
        m_IdleTimeMax = 2f;

        m_PatrolTime = 0f;
        m_PatrolTimeMax = 2f;

        m_CatchTime = 0f;
        m_CatchTimeMax = 1.2f;

        m_RangeCatch = 2f;
        m_RangeAngle = 70f;
        m_RangeChase = 3.5f;

        m_PatrolRadius = 8f;

        m_TurnSpd = 1f;
        m_MoveSpd = 1.5f;
    }

    public void DetermineCharacter()
    {
        m_Char = InGameObjectsManager.Instance.m_Char;
    }

    public virtual void SetFOV()
    {
        m_CatchColorRange = Color.white;
        // m_FOV.SetFieldOfView(m_RangeAngle + 10f);
    }

    #region NAVMESH

    public void SetDestination(Vector3 _des)
    {
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

    public void SetNavUpdatePosition(bool _value)
    {
        nav_Agent.updatePosition = _value;
        nav_Agent.updateRotation = _value;
    }

    public Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    #endregion

    public bool CanSeePlayer()
    {
        if (Helper.InRange(tf_ViewPoint.position, m_Char.tf_Owner.position, m_RangeCatch + 1.5f))
        {
            Vector3 dirToPlayer = (m_Char.tf_Owner.position - tf_ViewPoint.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(tf_ViewPoint.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < m_RangeAngle / 2f)
            {
                if (!Physics.Linecast(tf_ViewPoint.position, m_Char.tf_Owner.position, viewMask))
                {
                    return true;
                }
            }
        }
        return false;
    }

    // IEnumerator TurnToFace(Vector3 lookTarget)
    // {
    //     Vector3 dirToLookTarget = (lookTarget - tf_Owner.position).normalized;
    //     float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

    //     while (Mathf.Abs(Mathf.DeltaAngle(tf_Owner.eulerAngles.y, targetAngle)) > 0.05f)
    //     {
    //         float angle = Mathf.MoveTowardsAngle(tf_Owner.eulerAngles.y, targetAngle, m_TurnSpd * Time.deltaTime);
    //         tf_Owner.eulerAngles = Vector3.up * angle;
    //         yield return null;
    //     }
    // }

    #region State

    public void ChangeState(IState<Enemy> _state)
    {
        m_StateMachine.ChangeState(_state);
    }

    #region Idle

    public virtual void OnIdleEnter()
    {
        m_IdleTime = 0f;
        m_EnemyState = EnemyState.IDLE;
        anim_Owner.SetTrigger(ConfigKeys.e_Idle);
        // SetDestination(tf_Owner.position);
    }

    public virtual void OnIdleExecute()
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

    public virtual void OnIdleExit()
    {

    }

    #endregion

    #region Patrol

    public virtual void OnPatrolEnter()
    {
        m_PatrolTime = 0f;
        m_EnemyState = EnemyState.PATROL;
        anim_Owner.SetTrigger(ConfigKeys.e_Run);

        Vector3 newPos = RandomNavSphere(tf_Owner.position, m_PatrolRadius, -1);
        SetDestination(newPos);
    }

    public virtual void OnPatrolExecute()
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

    public virtual void OnPatrolExit()
    {

    }


    #endregion

    #region Chase

    public virtual void OnChaseEnter()
    {
        SetDestination(m_Char.tf_Owner.position);
        m_EnemyState = EnemyState.CHASE;
        m_CatchTime = 0f;

        anim_Owner.SetTrigger(ConfigKeys.e_Run);
    }

    public virtual void OnChaseExecute()
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

        // if (m_Char.IsRunning())
        // {
        SetDestination(m_Char.tf_Owner.position);
        //     Debug.Log("Char IsRunning");
        // }

        if (CanSeePlayer() && !IsThroughWall())
        {
            // if (Helper.InRange(tf_Owner.position, m_Char.tf_Owner.position, 1f))
            // {
            //     ChangeState(E_CatchState.Instance);
            // }
            // else
            // {
            m_CatchTime += Time.deltaTime;
            // }
        }
        else
        {
            m_CatchTime -= Time.deltaTime;
        }
        m_CatchTime = Mathf.Clamp(m_CatchTime, 0, m_CatchTimeMax);
        m_FOV.SetNormalColor(Color.Lerp(m_CatchColorRange, Color.red, m_CatchTime / m_CatchTimeMax));

        if (m_CatchTime >= m_CatchTimeMax)
        {
            ChangeState(E_CatchState.Instance);
            return;
        }
    }

    public bool IsThroughWall()
    {
        Vector3 dir = (m_Char.tf_RayStartPoint.position - tf_RayStartPoint.position).normalized;
        RaycastHit[] hits = Physics.RaycastAll(tf_RayStartPoint.position, dir, 5f);

        int hitCount = hits.Length;

        if (hitCount <= 0)
        {
            return false;
        }

        float distance = Helper.CalDistance(tf_Owner.position, m_Char.tf_Owner.position);
        List<GameObject> gOs = new List<GameObject>();
        gOs.Clear();

        for (int i = 0; i < hitCount; i++)
        {
            if (!hits[i].collider.gameObject.CompareTag("Character"))
            {
                if (Helper.InRange(tf_Owner.position, hits[i].point, distance))
                {
                    gOs.Add(hits[i].transform.gameObject);
                }
            }
        }

        if (gOs.Count > 0)
        {
            return true;
        }

        return false;
    }

    public virtual void OnChaseExit()
    {
        m_FOV.SetNormalColor(m_CatchColorRange);
    }

    #endregion

    #region Chase

    public virtual void OnCatchEnter()
    {
        m_EnemyState = EnemyState.CATCH;
        tf_Owner.LookAt(m_Char.tf_Owner.position);
        tf_Owner.DOMove(m_Char.tf_Owner.position, 0.7f).OnComplete(() =>
        {
            // SetDestination(m_Char.tf_Owner.position);
            nav_Agent.Warp(m_Char.tf_Owner.position);
            SetNavUpdatePosition(false);
        });

        anim_Owner.SetTrigger(ConfigKeys.e_Catch);

        if (GameManager.Instance.m_LevelStart)
        {
            Helper.DebugLog("OnCatchEnter");
            EventManager.CallEvent(GameEvent.CHAR_SPOTTED);

            int levelPlay = ProfileManager.GetLevel();
            AnalysticsManager.LogLoseLevel(levelPlay);

            GameManager.Instance.m_LevelStart = false;
        }
    }

    public virtual void OnCatchExecute()
    {
        // SetDestination(m_Char.tf_Owner.position);
    }

    public virtual void OnCatchExit()
    {

    }

    #endregion

    #endregion

    public void Despawn()
    {
        PrefabManager.Instance.DespawnPool(gameObject);
    }
}

public enum EnemyState
{
    IDLE = 0,
    PATROL = 1,
    CHASE = 2,
    CATCH = 3,
}