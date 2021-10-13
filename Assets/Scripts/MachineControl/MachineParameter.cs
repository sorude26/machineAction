using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineParameter : MonoBehaviour
{

    [Tooltip("行動速度")]
    [SerializeField]
    float m_actionSpeed = 0.5f;
    public float ActionSpeed { get => m_actionSpeed; }
    [Tooltip("歩行移動力")]
    [SerializeField]
    float m_walkPower = 1.4f;
    public float WalkPower { get => m_walkPower; }
    [Tooltip("歩行最高速度")]
    [SerializeField]
    float m_maxWalkSpeed = 20f;
    public float MaxWalkSpeed { get => m_maxWalkSpeed; }
    [Tooltip("旋回力")]
    [SerializeField]
    float m_turnPower = 4f;
    public float TurnPower { get => m_turnPower; }
    [Tooltip("旋回速度")]
    [SerializeField]
    float m_turnSpeed = 2f;
    public float TurnSpeed { get => m_turnSpeed; }
    [Tooltip("ジャンプ力")]
    [SerializeField]
    float m_jumpPower = 9f;
    public float JumpPower { get => m_jumpPower; }
    [Tooltip("ジェット力")]
    [SerializeField]
    float m_jetPower = 1f;
    public float JetPower { get => m_jetPower; }
    [Tooltip("ホバー移動力")]
    [SerializeField]
    float m_floatSpeed = 5f;
    public float FloatSpeed { get => m_floatSpeed; }
    [Tooltip("ホバー最高速度")]
    [SerializeField]
    float m_maxFloatSpeed = 20f;
    public float MaxFloatSpeed { get => m_maxFloatSpeed; }
}
