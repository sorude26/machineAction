using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyControl : MonoBehaviour
{
    [SerializeField]
    TurnControl m_turnControl = default;
    public void LookMove(Vector2 dir)
    {
        Vector3 look = new Vector3(dir.x, 0, dir.y);
        m_turnControl.StartTurn(transform, 0.5f, look);
    }
}
