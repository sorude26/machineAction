using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyControl : MonoBehaviour
{
    [SerializeField]
    Animator m_animator = default;
    [SerializeField]
    GroundCheck m_groundCheck = default;
    [SerializeField]
    float m_maxUpAngle = 20f;
    [SerializeField]
    float m_maxDownAngle = -10f;
    float m_turn = 0;
    float m_upTurn = 0;
    public bool TurnNow { get; private set; }
    private void Start()
    {
        GameScene.InputManager.Instance.OnFirstInputAttack += HandAttackRight;
    }
    public void ChangeSpeed(float speed)
    {
        if (m_animator)
        {
            m_animator.SetFloat("Speed", speed);
        }
    }
    public void HandAttackLeft()
    {
    }
    int attackCount = 0;
    bool attack = false;
    public void HandAttackRight()
    {
        if (attackCount == 0)
        {
            attackCount++;
            if (m_groundCheck.IsGrounded())
            {
                ChangeAnimation("attackSwingRArm");
            }
            else
            {
                ChangeAnimation("attackSwingRArm3");
            }
            return;
        }
        attack = true;
    }
    void Attack()
    {
        if (attack)
        {
            if (attackCount == 1)
            {
                ChangeAnimation("attackSwingRArm2");
            }
            else if (attackCount == 2)
            {
                ChangeAnimation("attackSwingRArm3");
            }
            attackCount++;
            attack = false;
        }
    }
    void AttackEnd()
    {
        attackCount = 0;
        attack = false;
    }
    public void LookMove(Vector2 dir,float turnSpeed)
    {
        if (TurnNow)
        {
            return;
        }
        if (dir.x > 0.3f)
        {
            Turn(turnSpeed);
        }
        else if (dir.x < -0.3f)
        {
            Turn(-turnSpeed);
        }
        else
        {
            if (dir.y > 0.7f)
            {
                UpTurn(turnSpeed);
            }
            else if (dir.y < -0.7f)
            {
                UpTurn(-turnSpeed);
            }
        }
    }
    void Turn(float q)
    {
        if (TurnNow)
        {
            return;
        }
        m_turn += q;
        if (m_turn > 360)
        {
            m_turn -= 360;
        }
        if (m_turn < -360)
        {
            m_turn += 360;
        }
        Vector3 dir = Quaternion.Euler(m_upTurn, m_turn, 0) * Vector3.forward;
        TurnNow = true;
        StartCoroutine(Turn(transform, 1f, dir));
    }
    void UpTurn(float q)
    {
        if (TurnNow)
        {
            return;
        }
        if (q > 0)
        {
            if (m_upTurn <= m_maxUpAngle)
            {
                m_upTurn += q;
                if (m_upTurn > m_maxUpAngle)
                {
                    m_upTurn = m_maxUpAngle;
                }
            }
        }
        else
        {
            if (m_upTurn >= m_maxDownAngle)
            {
                m_upTurn += q;
                if (m_upTurn > m_maxDownAngle)
                {
                    m_upTurn = m_maxDownAngle;
                }
            }
        }
        Vector3 dir = Quaternion.Euler(m_upTurn, m_turn, 0) * Vector3.forward;
        TurnNow = true;
        StartCoroutine(Turn(transform, 0.8f, dir));
    }
    IEnumerator Turn(Transform tragetTransform, float turnSpeed, Vector3 targetDir)
    {
        while (true)
        {
            Vector3 newDir = Vector3.RotateTowards(tragetTransform.forward, targetDir, turnSpeed * Time.deltaTime, turnSpeed * Time.deltaTime);
            Quaternion turnRotation = Quaternion.LookRotation(newDir);
            if (tragetTransform.rotation == turnRotation)
            {
                break;
            }
            tragetTransform.rotation = turnRotation;
            yield return null;
        }
        TurnNow = false;
    }
    void ChangeAnimation(string changeTarget, float changeTime = 0.2f)
    {
        m_animator.CrossFadeInFixedTime(changeTarget, changeTime);
    }
}
