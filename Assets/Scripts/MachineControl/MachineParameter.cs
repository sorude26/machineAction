﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineParameter : MonoBehaviour
{

    [Tooltip("行動速度")]
    [SerializeField]
    float _actionSpeed = 0.8f;
    public float ActionSpeed { get => _actionSpeed; }
    [Tooltip("歩行移動力")]
    [SerializeField]
    float _walkPower = 1.1f;
    public float WalkPower { get => _walkPower; }
    [Tooltip("歩行最高速度")]
    [SerializeField]
    float _maxWalkSpeed = 12f;
    public float MaxWalkSpeed { get => _maxWalkSpeed; }
    [Tooltip("旋回力")]
    [SerializeField]
    float _turnPower = 21f;
    public float TurnPower { get => _turnPower; }
    [Tooltip("旋回速度")]
    [SerializeField]
    float _turnSpeed = 3f;
    public float TurnSpeed { get => _turnSpeed; }
    [Tooltip("ジャンプ力")]
    [SerializeField]
    float _jumpPower = 8f;
    public float JumpPower { get => _jumpPower; }
    [Tooltip("着地硬直")]
    [SerializeField]
    float _landingTime = 0.5f;
    public float LandingTime { get => _landingTime; }
    [Tooltip("胴体旋回速度")]
    [SerializeField]
    float _bodyTurnSpeed = 4f;
    public float BodyTurnSpeed { get => _bodyTurnSpeed; }
    [Tooltip("胴体旋回限界")]
    [SerializeField]
    float _bodyTurnRange = 0.4f;
    public float BodyTurnRange { get => _bodyTurnRange; }
    [Tooltip("カメラ旋回限界")]
    [SerializeField]
    float _cameraTurnRange = 50f;
    public float CameraTurnRange { get => _cameraTurnRange; }
    [Tooltip("ジェット力")]
    [SerializeField]
    float _jetPower = 3f;
    public float JetPower { get => _jetPower; }
    [Tooltip("ジェット制御力")]
    [SerializeField]
    float _jetControlPower = 0.8f;
    public float JetControlPower { get => _jetControlPower; }
    [Tooltip("ジェット移動力")]
    [SerializeField]
    float _jetMovePower = 0.8f;
    public float JetMovePower { get => _jetMovePower; }
    [Tooltip("ジェット持続時間")]
    [SerializeField]
    float _jetTime = 30f;
    public float JetTime { get => _jetTime; }
    [Tooltip("ホバー移動力")]
    [SerializeField]
    float _floatSpeed = 30f;
    public float FloatSpeed { get => _floatSpeed; }
    [Tooltip("ホバー最高速度")]
    [SerializeField]
    float _maxFloatSpeed = 500f;
    public float MaxFloatSpeed { get => _maxFloatSpeed; }
}
