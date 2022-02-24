using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeControl : MonoBehaviour
{
    [SerializeField]
    protected Image _gauge = default;
    protected float _maxValue = 100;
    protected float _currentValue = 100;
    public float CurrentValue { 
        get => _currentValue;
        set
        {
            _currentValue = value;
            UpdateGauge();
        }
    }
    public virtual void SetMaxValue(float maxValue)
    {
        _maxValue = maxValue;
        CurrentValue = maxValue; 
    }
    protected virtual void SetValue(int value)
    {
        CurrentValue = value;
    }
    protected void UpdateGauge()
    {
        if (_gauge == null)
        {
            gameObject.SetActive(false);
            return;
        }
        _gauge.fillAmount = CurrentValue / _maxValue;
    }
}
