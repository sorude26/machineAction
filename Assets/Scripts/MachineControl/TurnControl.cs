using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 方向転換を制御する
/// </summary>
public class TurnControl : MonoBehaviour
{
    public void Turn(Rigidbody rb, float dir, float speed,float maxSpeed)
    {
        if (rb.angularVelocity.sqrMagnitude < maxSpeed)
        {
            rb.AddTorque(0, dir * speed, 0);
        }
    }
    public void StrongTurn(Rigidbody rb, float dir, float speed, float maxSpeed)
    {
        if (rb.angularVelocity.sqrMagnitude < maxSpeed)
        {
            rb.AddTorque(0, dir * speed, 0,ForceMode.Impulse);
        }
    }
    public void SetTurn(Transform transfom,Vector3 forward)
    {
        transfom.forward = forward;
    }
}
