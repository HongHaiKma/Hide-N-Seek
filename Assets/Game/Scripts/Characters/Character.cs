using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControlFreak2;

public class Character : InGameObject
{
    [Header("Components")]
    public CharacterController cc_Owner;

    [Header("Characteristics")]
    public StateMachine<Character> m_StateMachine;
    public CharState m_CharState;

    [Header("Input")]
    Vector3 m_MoveInput;
    public float m_MoveSpd = 700;

    [Header("Input")]
    #region Rotate
    public float smoothMoveTime = .1f;
    public float turnSpeed = 8;

    float angle;
    float smoothInputMagnitude;
    float smoothMoveVelocity;
    #endregion

    public bool m_DisableMove;

    [Header("Slope")]
    private float m_SlopeForce;
    private float m_SlopeForceRayLength;

    [Header("Test")]
    public float m_AxisX;
    public float m_AxisZ;
    public Transform tf_RayStartPoint;

    private void OnEnable()
    {
        m_StateMachine = new StateMachine<Character>(this);
        m_StateMachine.Init(P_IdleState.Instance);

        StartListenToEvent();
        // Rigidbody rb;
        // rb.centerOfMass
    }

    private void OnDisable()
    {
        StopListenToEvent();
    }

    public void StartListenToEvent()
    {
        EventManager.AddListener(GameEvent.CHAR_SPOTTED, Disable);
    }

    public void StopListenToEvent()
    {
        EventManager.RemoveListener(GameEvent.CHAR_SPOTTED, Disable);
    }

    void Update()
    {
        m_StateMachine.ExecuteStateUpdate();
    }

    // void FixedUpdate()
    // {
    //     m_StateMachine.ExecuteStateUpdate();
    // }

    public float GetRotateAngle()
    {
        float moveInputMag = m_MoveInput.magnitude;
        smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, moveInputMag, ref smoothMoveVelocity, smoothMoveTime);
        float targetAngle = Mathf.Atan2(m_MoveInput.x, m_MoveInput.z) * Mathf.Rad2Deg;

        return Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * turnSpeed * moveInputMag);
    }

    void OnTriggerEnter(Collider other)
    {
        // if (col.tag == "Finish")
        // {
        //     Disable();

        //     EventManager.CallEvent(GameEvent.CHAR_WIN);
        // }

        InGameObject obj = other.GetComponent<InGameObject>();

        if (obj != null)
        {
            if (obj.m_ObjectType == ObjectType.DOOR)
            {
                Disable();
                ChangeState(P_WinState.Instance);
                EventManager.CallEvent(GameEvent.CHAR_WIN);
                GameManager.Instance.m_LevelStart = false;
            }
        }
    }

    void Disable()
    {
        m_DisableMove = true;
        ChangeState(P_IdleState.Instance);
    }

    #region State

    public void ChangeState(IState<Character> _state)
    {
        m_StateMachine.ChangeState(_state);
    }

    #region IdleState

    public void CheckCanMove()
    {
        if (m_DisableMove)
        {
            ChangeState(P_DieState.Instance);
            return;
        }
    }

    public virtual void OnIdleEnter()
    {
        CheckCanMove();

        if (IsRunning())
        {
            ChangeState(P_RunState.Instance);
            return;
        }

        anim_Owner.SetTrigger(ConfigKeys.p_Idle);
        m_CharState = CharState.IDLE;
    }

    public virtual void OnIdleExecute()
    {
        CheckCanMove();

        if (GameManager.Instance.m_LevelStart)
        {
            m_MoveInput = new Vector3(CF2Input.GetAxis("Mouse X"), 0f, CF2Input.GetAxis("Mouse Y")).normalized;
        }

        if (IsRunning())
        {
            ChangeState(P_RunState.Instance);
        }
    }

    public bool IsRunning()
    {
        return (m_MoveInput.x != 0f || m_MoveInput.z != 0f);
    }

    public virtual void OnIdleExit()
    {

    }

    #endregion

    #region RunState

    public virtual void OnRunEnter()
    {
        CheckCanMove();

        if (!IsRunning() || m_DisableMove)
        {
            ChangeState(P_IdleState.Instance);
            return;
        }

        anim_Owner.SetTrigger(ConfigKeys.p_Run);
        m_CharState = CharState.RUN;
    }

    public float factor;

    public virtual void OnRunExecute()
    {
        CheckCanMove();

        if (GameManager.Instance.m_LevelStart)
        {
            m_MoveInput = new Vector3(CF2Input.GetAxis("Mouse X"), 0f, CF2Input.GetAxis("Mouse Y")).normalized;
        }

        m_AxisX = m_MoveInput.x;
        m_AxisZ = m_MoveInput.z;

        float moveInputMag = m_MoveInput.magnitude;
        smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, moveInputMag, ref smoothMoveVelocity, smoothMoveTime);

        float targetAngle = Mathf.Atan2(m_MoveInput.x, m_MoveInput.z) * Mathf.Rad2Deg;

        angle = GetRotateAngle();

        // if (cc_Owner.isGrounded)
        // {

        // }

        // cc_Owner.Move(m_MoveInput * Time.fixedDeltaTime * 100f * m_MoveSpd);
        cc_Owner.Move(m_MoveInput * Time.deltaTime * 3f * m_MoveSpd);
        tf_Owner.rotation = Quaternion.Euler(Vector3.up * angle);

        // rb_Owner.MoveRotation(Quaternion.Euler(Vector3.up * angle));
        // rb_Owner.velocity = m_MoveInput * Time.fixedDeltaTime * 100f * m_MoveSpd;
        // rb_Owner.velocity += Physics.gravity.normalized * factor;

        // if (m_MoveInput.x == 0f || m_MoveInput.z == 0f)
        if (!IsRunning())
        {
            ChangeState(P_IdleState.Instance);
            return;
        }
    }

    public virtual void OnRunExit()
    {

    }

    #endregion

    #region WinState

    public virtual void OnWinEnter()
    {
        m_CharState = CharState.WIN;
        anim_Owner.SetTrigger(ConfigKeys.p_Win);
    }

    public virtual void OnWinExecute()
    {

    }

    public virtual void OnWinExit()
    {

    }

    #endregion

    #region DieState

    public virtual void OnDieEnter()
    {
        m_CharState = CharState.DIE;
        anim_Owner.SetTrigger(ConfigKeys.p_Die);

        m_MoveInput = Vector3.zero;
        CF2Input.ResetInputAxes();
        // rb_Owner.velocity = Vector3.zero;
    }

    public virtual void OnDieExecute()
    {

    }

    public virtual void OnDieExit()
    {

    }

    #endregion

    #endregion
}

public enum CharState
{
    IDLE = 0,
    RUN = 1,
    DIE = 2,
    WIN = 3
}
