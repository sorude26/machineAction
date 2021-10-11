using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineParameter : MonoBehaviour
{
    [Tooltip("歩行移動力")]
    [SerializeField]
    float m_walkSpeed = 10f;
    public float WalkSpeed { get => m_walkSpeed; }
    [Tooltip("歩行最高速度")]
    [SerializeField]
    float m_maxWalkSpeed = 5f;
    public float MaxWalkSpeed { get => m_walkSpeed; }
    [Tooltip("旋回速度")]
    [SerializeField]
    float m_turnSpeed = 5f;
    public float TurnSpeed { get => m_turnSpeed; }
}
