using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterView : MonoBehaviour
{
    [SerializeField]
    ParameterGauge _gaugePrefab = default;
    [SerializeField]
    RectTransform _base = default;
    const int _gaugeNum = 12;
    ParameterGauge[] _gauges = default;
    private void Awake()
    {
        if (_base == null)
        {
            _base = GetComponent<RectTransform>();
        }
        _gauges = new ParameterGauge[_gaugeNum];
        for (int i = 0; i < _gaugeNum; i++)
        {
            _gauges[i] = Instantiate(_gaugePrefab, _base);
        }
    }
    public void SetParameter(MachineParameter parameter)
    {
        _gauges[0].SetGauge("運動性能", 1.5f, parameter.ActionSpeed);
        _gauges[1].SetGauge("索敵範囲", 450f, parameter.LockOnRange);
        _gauges[2].SetGauge("歩行最高速度", 70f, parameter.MaxWalkSpeed);
        _gauges[3].SetGauge("走行最高速度", 200f, parameter.MaxRunSpeed);
        _gauges[4].SetGauge("滞空移動速度", 40f, parameter.FloatSpeed);
        _gauges[5].SetGauge("移動旋回性能", 40f, parameter.TurnSpeed);
        _gauges[6].SetGauge("跳躍性能", 30f, parameter.JumpPower);
        _gauges[7].SetGauge("反応速度", 9f, parameter.BodyTurnSpeed);
        _gauges[8].SetGauge("推進力", 8f, parameter.JetPower);
        _gauges[9].SetGauge("持続時間", 30f, parameter.JetTime);
        _gauges[10].SetGauge("高速移動速度", 80f, parameter.JetImpulsePower);
        _gauges[11].SetGauge("ジェネレータ回復性能", 10f, parameter.PowerRecoverySpeed);
    }
}
