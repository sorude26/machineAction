using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 脚部データ
/// </summary>
[CreateAssetMenu]
public class LegData : UnitPartsData
{
    [Tooltip("移動力")]
    [SerializeField]
    float[] _movePower;
    [Tooltip("対荷重量")]
    [SerializeField] 
    int[] _loadCapacity;
    [Tooltip("バランス性能")]
    [SerializeField]
    int[] _landingBalancer;
    [Tooltip("運動性能")]
    [SerializeField]
    int[] _exercisePerformance;
    [Tooltip("空中移動力")]
    [SerializeField]
    float[] _floatPower;
    /// <summary> 移動力 </summary>
    public float[] MovePower { get => _movePower; }
    /// <summary> 対荷重量 </summary>
    public int[] LoadCapacity { get => _loadCapacity; }
    /// <summary> バランス性能 </summary>
    public int[] Balancer { get => _landingBalancer; }
    /// <summary> 運動性能 </summary>
    public int[] Exercise { get => _exercisePerformance; }
    /// <summary> 空中移動力 </summary>
    public float[] FloatPower { get => _floatPower; }
}
