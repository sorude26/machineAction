using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParameterGauge : MonoBehaviour
{
    [SerializeField]
    GaugeControl _gauge = default;
    [SerializeField]
    Text _parameterName = default;
    public void SetGauge(string name,float max,float current)
    {
        _parameterName.text = name;
        _gauge.SetMaxValue(max);
        _gauge.CurrentValue = current;
    }
}
