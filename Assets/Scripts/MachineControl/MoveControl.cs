using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 移動を制御する
/// </summary>
public class MoveControl : MonoBehaviour
{
    public void MoveWalk(Rigidbody rb,Vector3 dir,float power,float maxSpeed)
    {
        if (rb.velocity.sqrMagnitude < maxSpeed)
        {
            rb.AddForce(dir * power);
        }
    }
    public void MoveFloat(Rigidbody rb, Vector3 dir, float speed, float maxSpeed)
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (rb.velocity.sqrMagnitude < maxSpeed)
        {
            rb.velocity = dir * speed;
        }
    }
}
