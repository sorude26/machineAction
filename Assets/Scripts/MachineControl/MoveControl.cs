using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 移動を制御する
/// </summary>
public class MoveControl : MonoBehaviour
{
    const float FloatDelay = 0.999f;
    public void MoveWalk(Rigidbody rb,Vector3 dir,float power,float maxSpeed)
    {
        if (rb.velocity.sqrMagnitude < maxSpeed)
        {
            rb.AddForce(dir * power,ForceMode.Impulse);
        }
    }
    public void MoveFloat(Rigidbody rb, Vector3 dir, float speed, float maxSpeed)
    {
        Vector3 current = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.velocity = current * FloatDelay;
        if (rb.velocity.sqrMagnitude < maxSpeed)
        {
            rb.velocity = dir * speed;
        }
    }
    public void Jump(Rigidbody rb, Vector3 dir, float power)
    {
        rb.AddForce(dir * power, ForceMode.Impulse);
    }
    public void Jet(Rigidbody rb, Vector3 dir, float power,float maxSpeed)
    {
        if (rb.velocity.sqrMagnitude < maxSpeed)
        {
            rb.AddForce(dir * power);
        }
    }
    public void Jet(Rigidbody rb, Vector3 dir, float power)
    {
        rb.AddForce(dir * power);
    }
}
