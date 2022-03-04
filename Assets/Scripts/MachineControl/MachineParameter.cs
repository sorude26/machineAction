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
    public float ActionSpeed { get => _actionSpeed; }
    [Tooltip("歩行移動力")]
    [SerializeField]
    float _walkPower = 1.1f;
    public float WalkPower { get => _walkPower; }
    [Tooltip("歩行最高速度")]
    [SerializeField]
    float _maxWalkSpeed = 12f;
    public float MaxWalkSpeed { get => _maxWalkSpeed; }
    [Tooltip("走行移動力")]
    [SerializeField]
    float _runPower = 1.5f;
    public float RunPower { get => _runPower; }
    [Tooltip("走行最高速度")]
    [SerializeField]
    float _maxRunSpeed = 35f;
    public float MaxRunSpeed { get => _maxRunSpeed; }
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
    [Tooltip("ロックオン範囲")]
    [SerializeField]
    float _lockOnRange = 100f;
    public float LockOnRange { get => _lockOnRange; }
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
    [Tooltip("ジェット移動速度")]
    [SerializeField]
    float _jetImpulsePower = 40f;
    public float JetImpulsePower { get => _jetImpulsePower; }
    [Tooltip("ジェット移動時消費量")]
    [SerializeField]
    float _needPowerJet = 1f;
    public float NeedPowerJet { get => _needPowerJet; }
    [Tooltip("ホバー移動力")]
    [SerializeField]
    float _floatSpeed = 30f;
    public float FloatSpeed { get => _floatSpeed; }
    [Tooltip("ホバー最高速度")]
    [SerializeField]
    float _maxFloatSpeed = 500f;
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
    public float PowerRecoverySpeed { get => _powerRecoverySpeed; }
    int _totalWeight = default;
    int _energy = default;
    public void SetParameter(PartsManager machineParts)
    {
        if (_nChangeParameter)
        {
            return;
        }
        SetWeight(machineParts);
        SetEnergy(machineParts);
        SetBalance(machineParts);
        SetBoostPower(machineParts);
        SetMove(machineParts);
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
        _energy = machineParts.Body.Output;
        _energy += machineParts.Booster.Energy;
        if (_energy > _totalWeight)
        {
            if (_energy > _totalWeight * 2)
            {
                _flyConsumption = -0.1f;
                _powerRecoverySpeed = 8f;
            }
            else
            {
                _flyConsumption = 0.1f;
                _powerRecoverySpeed = 5f;
            }
        }
        else
        {
            if (_energy > _totalWeight / 2)
            {
                _flyConsumption = 0.5f;
                _powerRecoverySpeed = 2f;
            }
            else
            {
                _flyConsumption = 1f;
                _powerRecoverySpeed = 0.5f;
            }
        }
    }
    void SetBalance(PartsManager machineParts)
    {
        int lWeight = machineParts.LArm.Weight;
        lWeight += machineParts.LAWeapon.Weight;
        int rWeight = machineParts.RArm.Weight;
        rWeight += machineParts.RAWeapon.Weight;
        int balance = Mathf.Abs(lWeight - rWeight);
        int actionCapacity = machineParts.Leg.LoadCapacity - (_totalWeight + balance);
        if (_energy - _totalWeight < 0)
        {
            actionCapacity -= machineParts.Leg.LoadCapacity;
        }
        if (actionCapacity >= 0)
        {
            if (actionCapacity > _totalWeight)
            {
                _actionSpeed = 1.2f;
                _landingTime = 0.1f;
                _bodyTurnSpeed = 8f;
            }
            else if (actionCapacity > _totalWeight / 2)
            {
                _actionSpeed = 1.1f;
                _landingTime = 0.2f;
                _bodyTurnSpeed = 6f;
            }
            else
            {
                _actionSpeed = 1.0f;
                _landingTime = 0.3f;
                _bodyTurnSpeed = 4f;
            }
            if (balance > machineParts.Leg.Balancer)
            {
                _actionSpeed -= 0.1f;
                _landingTime += 0.2f;
                _bodyTurnSpeed -= 1f;
            }
        }
        else
        {
            if (actionCapacity < -_totalWeight / 2)
            {
                balance += _totalWeight;
            }
            _actionSpeed = 0.8f;
            _landingTime = 0.5f; 
            _bodyTurnSpeed = 3f;
            if (balance > machineParts.Leg.Balancer)
            {
                _actionSpeed -= 0.2f;
                _landingTime += 0.2f;
                _bodyTurnSpeed -= 1f;
            }
        }
        if (balance / 2 > machineParts.Leg.Balancer)
        {
            _actionSpeed -= 0.2f;
            _landingTime += 0.5f;
            _bodyTurnSpeed -= 1f;
        }
        _landingTime *= machineParts.Head.Performance;
        _bodyTurnSpeed += 1 / machineParts.Head.Performance;
    }
    void SetBoostPower(PartsManager machineParts)
    {
        int boostPower = machineParts.Booster.Propulsion - _totalWeight;
        if (_energy < _totalWeight)
        {
            boostPower = 0;
        }
        _jetTime = machineParts.Booster.Duration - (_totalWeight / _energy);
        if (boostPower > 0)
        {
            if (boostPower > _totalWeight)
            {
                _jetPower = 6;
                _needPowerJet = _jetTime * 0.02f;
                _jetImpulsePower = 50;
                _needPowerFly = 0.5f;
            }
            else if (boostPower > _totalWeight / 2)
            {
                _jetPower = 4;
                _needPowerJet = _jetTime * 0.05f;
                _jetImpulsePower = 40;
                _needPowerFly = 1f;
            }
            else
            {
                _jetPower = 1;
                _needPowerJet = _jetTime * 0.2f;
                _jetImpulsePower = 20;
                _needPowerFly = 3f;
            }
        }
        else
        {
            _jetPower = 0.1f;
            _needPowerJet = _jetTime;
            _jetImpulsePower = 10;
            _needPowerFly = 10f;
        }
    }
    void SetMove(PartsManager machineParts)
    {
        _walkPower = machineParts.Leg.MovePower;
        _maxWalkSpeed = _walkPower * 10;
        _runPower = _walkPower * 0.8f;
        _maxRunSpeed = _runPower * 45f;
        _turnPower = _walkPower * 2f;
        _turnSpeed = _turnPower * 2f;
        _jumpPower = machineParts.Leg.Exercise;
        _floatSpeed = machineParts.Leg.FloatPower * _actionSpeed;
    }
}
