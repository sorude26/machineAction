using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 設置判定を行う
/// </summary>
public class GroundCheck : MonoBehaviour
{
    [SerializeField]
    Transform[] _leftCheckPos = default;
    [SerializeField]
    Transform[] _rightCheckPos = default;
    [SerializeField]
    Vector3 _checkDir = Vector3.down;
    [SerializeField]
    float _checkRange = 0.2f;
    public bool IsGrounded()
    {
        int leftCount = default;
        int rightCount = default;
        foreach (var pos in _leftCheckPos)
        {
            Vector3 start = pos.position;
            Vector3 end = start + _checkDir * _checkRange;
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
        foreach (var pos in _rightCheckPos)
        {
            Vector3 start = pos.position;
            Vector3 end = start + _checkDir * _checkRange;
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
