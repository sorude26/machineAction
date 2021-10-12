using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineParameter : MonoBehaviour
{
    [Tooltip("歩行移動力")]
    [SerializeField]
    float m_walkPower = 2f;
    public float WalkPower { get => m_walkPower; }
    [Tooltip("歩行速度")]
    [SerializeField]
    float m_walkSpeed = 0.5f;
    public float WalkSpeed { get => m_walkSpeed; }
    [Tooltip("歩行最高速度")]
    [SerializeField]
    float m_maxWalkSpeed = 5f;
    public float MaxWalkSpeed { get => m_maxWalkSpeed; }
    [Tooltip("旋回力")]
    [SerializeField]
    float m_turnPower = 10f;
    public float TurnPower { get => m_turnPower; }
    [Tooltip("旋回速度")]
    [SerializeField]
    float m_turnSpeed = 5f;
    public float TurnSpeed { get => m_turnSpeed; }
    [Tooltip("ジャンプ力")]
    [SerializeField]
    float m_jumpPower = 10f;
    public float JumpPower { get => m_jumpPower; }
}
