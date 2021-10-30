using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 方向転換を制御する
/// </summary>
public class TurnControl : MonoBehaviour
{
    public void Turn(Rigidbody rb, int dir, float speed,float maxSpeed)
    {
        if (rb.angularVelocity.sqrMagnitude < maxSpeed)
        {
            rb.AddTorque(0, dir * speed, 0);
        }
    }
}
