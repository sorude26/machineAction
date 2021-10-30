using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyControl : MonoBehaviour
{
    [SerializeField]
    float m_maxUpAngle = 20f;
    [SerializeField]
    float m_maxDownAngle = -10f;
    float m_turn = 0;
    float m_upTurn = 0;
    public bool TurnNow { get; private set; }
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
}
