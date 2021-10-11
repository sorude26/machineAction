using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 方向転換を制御する
/// </summary>
public class TurnControl : MonoBehaviour
{
    public bool TurnNow { get; private set; }
    public void StartTurn(float turnSpeed, Vector3 dir)
    {
        if (TurnNow)
        {
            StopAllCoroutines();
        }
        StartCoroutine(Turn(turnSpeed, dir));
    }
    IEnumerator Turn(float turnSpeed, Vector3 targetDir)
    {
        TurnNow = true;
        while (true)
        {
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, turnSpeed * Time.deltaTime, 0f);
            Quaternion turnRotation = Quaternion.LookRotation(newDir);
            if (transform.rotation == turnRotation)
            {
                break;
            }
            transform.rotation = turnRotation;
            yield return null;
        }
        TurnNow = false;
    }
}
