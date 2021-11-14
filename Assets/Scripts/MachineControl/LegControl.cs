using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegControl : MonoBehaviour
{
    [SerializeField]
    Animator m_animator = default;
    [SerializeField]
    GroundCheck m_groundCheck = default;
    int m_walk = default;
    int m_turn = default;
    bool m_jump = default;
    bool m_isGround = false;
    bool m_landing = default;
    bool m_float = default;
    float m_landingTime = 0.5f;
    float m_landingTimer = 0;
    MachineController m_machine = default;
    public void Set(MachineController controller)
    {
        m_machine = controller;
    } 
    public void SetLandingTime(float time)
    {
        m_landingTime = time;
    }
    public void ChangeSpeed(float speed)
    {
        if (m_animator)
        {
            m_animator.SetFloat("Speed", speed);
        }
    }
    public void WalkStart(int angle)
    {
        if (m_jump)
        {
            return;
        }
        if (m_walk == angle)
        {
            return;
        }
        m_walk = angle;
        if (angle > 0)
        {
            ChangeAnimation("Walk", 0.5f);
        }
        else if(angle < 0)
        {
            ChangeAnimation("WalkBack", 0.5f);
        }
        else
        {
            WalkStop();
        }
    }
    public void WalkStop()
    {
        if (m_jump)
        {
            return;
        }
        if (m_walk == 0 && m_turn == 0)
        {
            return;
        }
        m_walk = 0;
        m_turn = 0;
        ChangeAnimation("Idle", 0.5f);
    }
    public void TurnStartLeft()
    {
        if (m_jump)
        {
            return;
        }
        if (m_turn < 0)
        {
            return;
        }
        m_turn = -1;
        if (m_walk != 0)
        {
            return;
        }
        ChangeAnimation("TurnLeft");
    }
    public void TurnStartRight()
    {
        if (m_jump)
        {
            return;
        }
        if (m_turn > 0)
        {
            return;
        }
        m_turn = 1;
        if (m_walk != 0)
        {
            return;
        }
        ChangeAnimation("TurnRight");
    }
    public void ChangeMode()
    {
        if (m_float)
        {
            m_float = false;
        }
        else
        {
            m_float = true;
        }
        m_jump = false;
        m_walk = 0;
        m_turn = 0;
        m_landing = false;
        m_animator.SetBool("Float", m_float);
    }
    public void StartJump()
    {
        if (m_jump || m_float)
        {
            return;
        }
        m_jump = true;
        m_isGround = false;
        StartCoroutine(JumpFall());
        if (m_walk != 0)
        {
            ChangeAnimation("JunpStart");
            return;
        }
        if (m_turn > 0)
        {
            ChangeAnimation("JunpStartR");
        }
        else if (m_turn < 0)
        {

            ChangeAnimation("JunpStartL");
        }
        else
        {
            ChangeAnimation("JunpStart");
        }
    }
    public void StartJet()
    {
        if (m_landing)
        {
            return;
        }
        if (m_machine.InputAxis == Vector3.zero)
        {
            return;
        }
        if (m_jump)
        {
            if (m_isGround)
            {
                return;
            }
            if (m_machine.InputAxis.y <= 0)
            {
                if (m_machine.InputAxis.x > 0)
                {
                    ChangeAnimation("JetMoveFlyR", 0.01f);
                }
                else if (m_machine.InputAxis.x < 0)
                {
                    ChangeAnimation("JetMoveFlyL", 0.01f);
                }
                else
                {
                    ChangeAnimation("JetMoveFly", 0.01f);
                }
            }
            else
            {
                if (m_machine.InputAxis.x > 0)
                {
                    ChangeAnimation("JetMoveFlyR", 0.01f);
                }
                else if (m_machine.InputAxis.x < 0)
                {
                    ChangeAnimation("JetMoveFlyL", 0.01f);
                }
                else
                {
                    ChangeAnimation("JetMoveFlyB", 0.01f);
                }
            }
        }
        else
        {
            if (m_machine.InputAxis.y < 0)
            {
                ChangeAnimation("JetMoveB", 0.01f);
            }
            else
            {
                if (m_machine.InputAxis.x > 0)
                {
                    ChangeAnimation("JetMoveR", 0.01f);
                }
                else if (m_machine.InputAxis.x < 0)
                {
                    ChangeAnimation("JetMoveL", 0.01f);
                }
                else
                {
                    ChangeAnimation("JetMove", 0.01f);
                }
            }
        }
    }
    IEnumerator JumpFall()
    {
        while (!m_isGround)
        {
            yield return null;
        }
        m_machine?.Landing();
        CameraController.Shake();
        ChangeAnimation("JunpEnd");
    }
    IEnumerator LandingWait()
    {
        m_landingTimer = 0;
        while (m_landingTimer < m_landingTime)
        {
            m_landingTimer += Time.deltaTime;
            yield return null;
        }
        m_animator.Play("LandingEnd");
        m_jump = false;
        m_walk = 0;
        m_turn = 0;
        m_landing = false;
    }

    void Walk()
    {
        m_machine?.Walk(m_walk);
        if (m_turn > 0)
        {
            TurnRight();
        }
        else if (m_turn < 0)
        {
            TurnLeft();
        }
        m_turn = 0;
    }
    void AttackMove()
    {
        m_machine?.Walk(1);
    }
    void AttackMoveStrong()
    {
        m_machine?.Walk(2);
    }
    void Shake()
    {
        CameraController.LightShake();
    }
    void TurnLeft()
    {
        m_machine?.Turn(-1);
    }
    void TurnRight()
    {
        m_machine?.Turn(1);
    }
    void Jump()
    {
        Vector3 dir = transform.forward * m_walk + transform.right * m_turn;
        m_machine?.StartJump((Vector3.up + dir).normalized);
    }
    void Landing()
    {
        if (m_landing || m_float)
        {
            return;
        }
        m_landing = true;
        StartCoroutine(LandingWait());
    }
    void Jet()
    {
        m_machine?.Jet();
    }
    void Stop()
    {
        m_machine?.Stop();
    }
    void Brake()
    {
        m_machine?.Brake();
    }
    void GroundCheck()
    {
        m_isGround = m_groundCheck.IsGrounded();
    }
    void ChangeAnimation(string changeTarget,float changeTime = 0.2f) 
    { 
        m_animator.CrossFadeInFixedTime(changeTarget, changeTime);
    }
    
    public void AttackMoveR()
    {
        if (m_jump || m_float || m_landing)
        {
            return;
        }
        ChangeAnimation("AttackR1");
    }
    public void AttackMoveL()
    {
        if (m_jump || m_float || m_landing)
        {
            return;
        }
        ChangeAnimation("AttackL1");
    }
}
