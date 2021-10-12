using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 方向転換を制御する
/// </summary>
public class TurnControl : MonoBehaviour
{
    public bool TurnNow { get; private set; }
    float m_turn = 0;
    public void StartTurn(Transform targetTransform, float turnSpeed, Vector3 dir)
    {
        if (TurnNow)
        {
            StopAllCoroutines();
        }
        StartCoroutine(Turn(targetTransform, turnSpeed, dir));
    }
    IEnumerator Turn(Transform tragetTransform,float turnSpeed, Vector3 targetDir)
    {
        TurnNow = true;
        while (true)
        {
            Vector3 newDir = Vector3.RotateTowards(tragetTransform.forward, targetDir, turnSpeed * Time.deltaTime, 0f);
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
    public void Turn(Rigidbody rb, int dir, float speed,float maxSpeed)
    {
        if (rb.angularVelocity.sqrMagnitude < maxSpeed)
        {
            rb.AddTorque(0, dir * speed, 0);
        }
    }
    void Turn(float q)
    {
        m_turn += q;
        if (m_turn > 360)
        {
            m_turn -= 360;
        }
        if (m_turn < -360)
        {
            m_turn += 360;
        }
        Vector3 dir = Quaternion.Euler(0, m_turn, 0) * Vector3.forward;
    }
}
