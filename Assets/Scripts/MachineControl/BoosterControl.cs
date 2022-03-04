using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterControl : MonoBehaviour
{
    [SerializeField]
    GaugeControl _gauge = default;
    MachineController _machine = default;
    public float CurrentBoostPower 
    { 
        get => _currentPower; 
        private set
        {
            _currentPower = value;
            if (_gauge)
            {
                _gauge.CurrentValue = _currentPower;
            }
            if (_currentPower <= 0)
            {
                BoostEnd();
            }
        }
    }
    public bool IsBoost { get; private set; }
    float _currentPower = default;
    float _maxBoostPower = 100f;
    bool _set = false;
    public void Set(MachineController controller)
    {
        _machine = controller;
        _maxBoostPower = _machine.Parameter.JetTime;
        if (_gauge)
        {
            _gauge.SetMaxValue(_maxBoostPower);
        }
        BoostEnd();
        _set = true;
    }
    private void Update()
    {
        if (!_set)
        {
            return;
        }
        if (CurrentBoostPower > 0 && _machine.FloatMode)
        {
            CurrentBoostPower -= _machine.Parameter.NeedPowerFly * Time.deltaTime;
        }
        if (CurrentBoostPower <= 0 && _machine.FloatMode)
        {
            _machine.ChangeFloat();
            return;
        }
        if (IsBoost)
        {
            if (CurrentBoostPower > 0)
            {
                CurrentBoostPower -= _machine.Parameter.FlyConsumption * Time.deltaTime;            
            }
            return;
        }
        PowerRecovery();
    }
    public bool BoostCheckJet()
    {
        CurrentBoostPower -= _machine.Parameter.NeedPowerJet;
        if (CurrentBoostPower > 0)
        {
            return true;
        }
        else
        {
            BoostEnd();
        }
        return false;
    }
    public bool BoostCheckFly()
    {
        CurrentBoostPower -= _machine.Parameter.NeedPowerFly * Time.deltaTime;
        if (CurrentBoostPower > 0)
        {
            return true;
        }
        return false;
    }
    void PowerRecovery()
    {
        CurrentBoostPower += _machine.Parameter.PowerRecoverySpeed * Time.deltaTime;
        if (CurrentBoostPower > _maxBoostPower)
        {
            CurrentBoostPower = _maxBoostPower;
        }
    }
    public void Boost()
    {
        foreach (var parts in _machine.MachineParts.GetAllMachineParts())
        {
            if (parts != null)
            {
                parts.StartBooster();
            }
        }
        IsBoost = true;
    }
    public void BoostF()
    {
        foreach (var parts in _machine.MachineParts.GetAllMachineParts())
        {
            if (parts != null)
            {
                parts.StartBoosterF();
            }
        }
        IsBoost = true;
    }
    public void BoostB()
    {
        foreach (var parts in _machine.MachineParts.GetAllMachineParts())
        {
            if (parts != null)
            {
                parts.StartBoosterB();
            }
        }
        IsBoost = true;
    }
    public void BoostL()
    {
        foreach (var parts in _machine.MachineParts.GetAllMachineParts())
        {
            if (parts != null)
            {
                parts.StartBoosterL();
            }
        }
        IsBoost = true;
    }
    public void BoostR()
    {
        foreach (var parts in _machine.MachineParts.GetAllMachineParts())
        {
            if (parts != null)
            {
                parts.StartBoosterR();
            }
        }
        IsBoost = true;
    }
    public void BoostEnd()
    {
        foreach (var parts in _machine.MachineParts.GetAllMachineParts())
        {
            if (parts != null)
            {
                parts.StopBooster();
            }
        }
    }
    public void BoostRecovery()
    {
        IsBoost = false;
    }
}
