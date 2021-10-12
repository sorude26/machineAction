using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MachineController : MonoBehaviour
{
    [SerializeField]
    MachineParameter m_parameter = default;
    [SerializeField]
    MoveControl m_moveControl = default;
    [SerializeField]
    TurnControl m_trunControl = default;
    [SerializeField]
    GroundCheck m_groundCheck = default;
    [SerializeField]
    LegControl m_leg = default;
    Rigidbody m_rb = default;
    private void Start()
    {
        GameScene.InputManager.Instance.OnInputAxisRaw += Move;
        GameScene.InputManager.Instance.OnInputAxisRawExit += MoveEnd;
        GameScene.InputManager.Instance.OnInputJump += Jump;
        m_rb = GetComponent<Rigidbody>();
        m_leg.OnWalk += Walk;
        m_leg.OnTurnLeft += TurnLeft;
        m_leg.OnTurnRight += TurnRight;
        m_leg.OnJump += StartJump;
        m_leg.OnStop += Stop;
    }

    private void Move(float horizonal, float vertical)
    {
        if (m_groundCheck.IsGrounded())
        {
            if (vertical > 0)
            {
                m_leg.WalkStart(1);
            }
            else if (vertical < 0)
            {
                m_leg.WalkStart(-1);
            }
        }
        if (horizonal > 0)
        {
            m_leg.TurnStartRight();
        }
        else if (horizonal < 0)
        {
            m_leg.TurnStartLeft();
        }
    }
    private void MoveEnd()
    {
        m_leg.WalkStop();
        if (m_groundCheck.IsGrounded())
        {
            Stop();
            m_rb.velocity = Vector3.zero;
        }
    }
    public void Walk(int angle)
    {
        if (m_groundCheck.IsGrounded())
        {
            m_rb.angularVelocity = Vector3.zero;
            m_leg.ChangeSpeed(m_parameter.WalkSpeed);
            m_moveControl.MoveWalk(m_rb, transform.forward * angle, m_parameter.WalkPower, m_parameter.MaxWalkSpeed);
        }
    }
    public void Jump()
    {
        Stop();
        m_leg.StartJump();
    }
    public void StartJump(Vector3 dir)
    {
        m_moveControl.Jump(m_rb, dir, m_parameter.JumpPower);
    }
    void TurnLeft()
    {
        m_trunControl.Turn(m_rb, -1, m_parameter.TurnPower, m_parameter.TurnSpeed);
    }
    void TurnRight()
    {
        m_trunControl.Turn(m_rb, 1, m_parameter.TurnPower, m_parameter.TurnSpeed);
    }
    void Stop()
    {
        m_rb.angularVelocity = Vector3.zero;
    }
}
