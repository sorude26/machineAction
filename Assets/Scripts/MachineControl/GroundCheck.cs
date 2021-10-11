using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 設置判定を行う
/// </summary>
public class GroundCheck : MonoBehaviour
{
    [SerializeField]
    Transform[] m_leftCheckPos = default;
    [SerializeField]
    Transform[] m_rightCheckPos = default;
    [SerializeField]
    Vector3 m_checkDir = Vector3.down;
    [SerializeField]
    float m_checkRange = 0.2f;
    public bool IsGrounded()
    {
        int leftCount = default;
        int rightCount = default;
        foreach (var pos in m_leftCheckPos)
        {
            Vector3 start = pos.position;
            Vector3 end = start + m_checkDir * m_checkRange;
            bool left = Physics.Linecast(start, end);
            if (left)
            {
                leftCount++;
                if (leftCount > 1)
                {
                    return true;
                }
            }
        }
        foreach (var pos in m_rightCheckPos)
        {
            Vector3 start = pos.position;
            Vector3 end = start + m_checkDir * m_checkRange;
            bool right = Physics.Linecast(start, end);
            if (right)
            {
                rightCount++;
                if (rightCount > 1)
                {
                    return true;
                }
            }
        }
        if (leftCount > 0 && rightCount > 0)
        {
            return true;
        }
        return false;
    }
}
