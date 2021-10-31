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
    public event Action<int> OnWalk;
    public event Action OnTurnLeft;
    public event Action OnTurnRight;
    public event Action<Vector3> OnJump;
    public event Action OnStop;
    int m_walk = default;
    int m_turn = default;
    bool m_jump = default;
    bool m_isGround = false;
    bool m_landing = default;
    bool m_float = default;
    float m_landingTime = 0.5f;
    float m_landingTimer = 0; 
    private void Start()
    {
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
        ChangeAnimation("JunpStart");
    }
    IEnumerator JumpFall()
    {
        while (!m_isGround)
        {
            yield return null;
        }
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
        OnWalk?.Invoke(m_walk);
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
    void TurnLeft()
    {
        OnTurnLeft?.Invoke();
    }
    void TurnRight()
    {
        OnTurnRight?.Invoke();
    }
    void Jump()
    {
        Vector3 dir = transform.forward * m_walk + transform.right * m_turn;      
        OnJump?.Invoke((Vector3.up + dir).normalized);
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
    void Stop()
    {
        OnStop?.Invoke();
        attackCount = 0;
        attack = false;
    }
    void GroundCheck()
    {
        m_isGround = m_groundCheck.IsGrounded();
    }
    void ChangeAnimation(string changeTarget,float changeTime = 0.2f)
    {
        m_animator.CrossFadeInFixedTime(changeTarget, changeTime);
    }
    int attackCount = 0;
    bool attack = false;
    public void HandAttackRight()
    {
        if (!m_isGround)
        {
            return;
        }
        if (attackCount == 0)
        {
            attackCount++;
            ChangeAnimation("AttackR");
            return;
        }
        attack = true;
    }
    void Attack()
    {
        if (!m_isGround)
        {
            return;
        }
        if (attack)
        {
            if (attackCount == 1)
            {
                ChangeAnimation("AttackL");
            }
            else if (attackCount == 2)
            {
                ChangeAnimation("AttackR");
            }
            attackCount++;
            attack = false;
        }
    }
}
