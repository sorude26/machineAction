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
    float _currentPower = default;
    float _maxBoostPower = 100f;
    float _needPowerJet = 700f;
    float _needPowerFly = 1f;
    float _needPower = 0.1f;
    float _powerRecoverySpeed = 5f;
    bool _boost = false;
    public void Set(MachineController controller)
    {
        _machine = controller;
        _maxBoostPower = _machine.Parameter.JetTime;
        if (_gauge)
        {
            _gauge.SetMaxValue(_maxBoostPower);
        }
        BoostEnd();
    }
    private void Update()
    {
        if (_boost)
        {
            if (CurrentBoostPower > 0)
            {
                CurrentBoostPower -= _needPower * Time.deltaTime;
            }
            return;
        }
        PowerRecovery();
    }
    public bool BoostCheckJet()
    {
        CurrentBoostPower -= _needPowerJet * Time.deltaTime;
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
        CurrentBoostPower -= _needPowerFly * Time.deltaTime;
        if (CurrentBoostPower > 0)
        {
            return true;
        }
        return false;
    }
    void PowerRecovery()
    {
        CurrentBoostPower += _powerRecoverySpeed * Time.deltaTime;
        if (CurrentBoostPower > _maxBoostPower)
        {
            CurrentBoostPower = _maxBoostPower;
        }
    }
    public void Boost()
    {
        foreach (var parts in _machine.MachineParts.GetAllParts())
        {
            if (parts != null)
            {
                parts.StartBooster();
            }
        }
        _boost = true;
    }
    public void BoostF()
    {
        foreach (var parts in _machine.MachineParts.GetAllParts())
        {
            if (parts != null)
            {
                parts.StartBoosterF();
            }
        }
        _boost = true;
    }
    public void BoostB()
    {
        foreach (var parts in _machine.MachineParts.GetAllParts())
        {
            if (parts != null)
            {
                parts.StartBoosterB();
            }
        }
        _boost = true;
    }
    public void BoostL()
    {
        foreach (var parts in _machine.MachineParts.GetAllParts())
        {
            if (parts != null)
            {
                parts.StartBoosterL();
            }
        }
        _boost = true;
    }
    public void BoostR()
    {
        foreach (var parts in _machine.MachineParts.GetAllParts())
        {
            if (parts != null)
            {
                parts.StartBoosterR();
            }
        }
        _boost = true;
    }
    public void BoostEnd()
    {
        foreach (var parts in _machine.MachineParts.GetAllParts())
        {
            if (parts != null)
            {
                parts.StopBooster();
            }
        }
    }
    public void BoostRecovery()
    {
        _boost = false;
    }
}
