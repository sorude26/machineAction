using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineParameter : MonoBehaviour
{
    [SerializeField]
    bool _nChangeParameter = false;
    [Tooltip("行動速度")]
    [SerializeField]
    float _actionSpeed = 0.8f;
    [SerializeField]
    Vector3 _actionSpeedPattern = default;
    public float ActionSpeed { get => _actionSpeed; }
    [Tooltip("歩行移動力")]
    [SerializeField]
    float _walkPower = 1.1f;
    [SerializeField]
    Vector3 _walkPowerPattern = default;
    public float WalkPower { get => _walkPower; }
    [Tooltip("歩行最高速度")]
    [SerializeField]
    float _maxWalkSpeed = 12f;
    [SerializeField]
    Vector3 _maxWalkSpeedPattern = default;
    public float MaxWalkSpeed { get => _maxWalkSpeed; }
    [Tooltip("走行移動力")]
    [SerializeField]
    float _runPower = 1.5f;
    [SerializeField]
    Vector3 _runPowerPattern = default;
    public float RunPower { get => _runPower; }
    [Tooltip("走行最高速度")]
    [SerializeField]
    float _maxRunSpeed = 35f;
    [SerializeField]
    Vector3 _maxRunPowerPattern = default;
    public float MaxRunSpeed { get => _maxRunSpeed; }
    [Tooltip("旋回力")]
    [SerializeField]
    float _turnPower = 21f;
    [SerializeField]
    Vector3 _turnPowerPattern = default;
    public float TurnPower { get => _turnPower; }
    [Tooltip("旋回速度")]
    [SerializeField]
    float _turnSpeed = 3f;
    [SerializeField]
    Vector3 _turnSpeedPattern = default;
    public float TurnSpeed { get => _turnSpeed; }
    [Tooltip("ジャンプ力")]
    [SerializeField]
    float _jumpPower = 8f;
    [SerializeField]
    Vector3 _jumpPowerPattern = default;
    public float JumpPower { get => _jumpPower; }
    [Tooltip("着地硬直")]
    [SerializeField]
    float _landingTime = 0.5f;
    [SerializeField]
    Vector3 _landingTimePattern = default;
    public float LandingTime { get => _landingTime; }
    [Tooltip("胴体旋回速度")]
    [SerializeField]
    float _bodyTurnSpeed = 4f;
    [SerializeField]
    Vector3 _bodyTurnSpeedPattern = default;
    public float BodyTurnSpeed { get => _bodyTurnSpeed; }
    [Tooltip("胴体旋回限界")]
    [SerializeField]
    float _bodyTurnRange = 0.4f;
    public float BodyTurnRange { get => _bodyTurnRange; }
    [Tooltip("カメラ旋回限界")]
    [SerializeField]
    float _cameraTurnRange = 50f;
    public float CameraTurnRange { get => _cameraTurnRange; }
    [Tooltip("ロックオン範囲")]
    [SerializeField]
    float _lockOnRange = 100f;
    public float LockOnRange { get => _lockOnRange; }
    [Tooltip("ジェット力")]
    [SerializeField]
    float _jetPower = 3f;
    [SerializeField]
    Vector3 _jetPowerPattern = default;
    public float JetPower { get => _jetPower; }
    [Tooltip("ジェット制御力")]
    [SerializeField]
    float _jetControlPower = 0.8f;
    [SerializeField]
    Vector3 _jetControlPattern = default;
    public float JetControlPower { get => _jetControlPower; }
    [Tooltip("ジェット移動力")]
    [SerializeField]
    float _jetMovePower = 0.8f;
    [SerializeField]
    Vector3 _jetMovePattern = default;
    public float JetMovePower { get => _jetMovePower; }
    [Tooltip("ジェット持続時間")]
    [SerializeField]
    float _jetTime = 30f;
    public float JetTime { get => _jetTime; }
    [Tooltip("ジェット移動速度")]
    [SerializeField]
    float _jetImpulsePower = 40f;
    [SerializeField]
    Vector3 _jetImpulsePattern = default;
    public float JetImpulsePower { get => _jetImpulsePower; }
    [Tooltip("ジェット移動時消費量")]
    [SerializeField]
    float _needPowerJet = 1f;
    [SerializeField]
    Vector3 _needJetPattern = default;
    public float NeedPowerJet { get => _needPowerJet; }
    [Tooltip("ホバー移動力")]
    [SerializeField]
    float _floatSpeed = 30f;
    [SerializeField]
    Vector3 _floatSpeedPattern = default;
    public float FloatSpeed { get => _floatSpeed; }
    [Tooltip("ホバー旋回力")]
    [SerializeField]
    float _floatTurnSpeed = 0.3f;
    public float FloatTurnSpeed { get => _floatTurnSpeed; }
    [Tooltip("ホバー最高速度")]
    [SerializeField]
    float _maxFloatSpeed = 500f;
    [SerializeField]
    Vector3 _maxFloatSpeedPattern = default;
    public float MaxFloatSpeed { get => _maxFloatSpeed; }
    [Tooltip("飛行時必要パワー")]
    [SerializeField]
    float _needPowerFly = 1f;
    public float NeedPowerFly { get => _needPowerFly; }
    [Tooltip("滞空中消費速度")]
    [SerializeField]
    float _flyConsumption = 0.1f;
    public float FlyConsumption { get => _flyConsumption; }
    [Tooltip("エネルギー回復速度")]
    [SerializeField]
    float _powerRecoverySpeed = 5f;
    [SerializeField]
    Vector3 _powerRecoveryPattern = default;
    public float PowerRecoverySpeed { get => _powerRecoverySpeed; }
    int _totalWeight = default;
    int _energy = default;
    int _balance = default;
    public void SetParameter(PartsManager machineParts)
    {
        if (_nChangeParameter)
        {
            return;
        }
        SetWeight(machineParts);
        SetEnergy(machineParts);
        SetBalance(machineParts);
        _bodyTurnRange = machineParts.Body.TurnRange;
        _lockOnRange = machineParts.Head.LockOnRange;
    }
    void SetWeight(PartsManager machineParts)
    {
        _totalWeight = 0;
        foreach (var parts in machineParts.GetAllParts())
        {
            _totalWeight += parts.Weight;
        }
    }
    void SetEnergy(PartsManager machineParts)
    {
        _energy = 0;
        _energy = machineParts.Body.Output;
        _energy += machineParts.Booster.Energy;
    }
    void SetBalance(PartsManager machineParts)
    {
        int lWeight = machineParts.LArm.Weight;
        lWeight += machineParts.LAWeapon.Weight;
        int rWeight = machineParts.RArm.Weight;
        rWeight += machineParts.RAWeapon.Weight;
        _balance =_totalWeight + Mathf.Abs(lWeight - rWeight);
    }
    void SetBoostPower(PartsManager machineParts)
    {
        int boostPower = machineParts.Booster.Propulsion;
    }
}
