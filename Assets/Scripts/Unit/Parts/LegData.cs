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
    [SerializeField] int[] m_movePower;
    [Tooltip("対荷重量")]
    [SerializeField] int[] _loadCapacity;
    [Tooltip("バランス性能")]
    [SerializeField] int[] _landingBalancer;
    /// <summary> 移動力 </summary>
    public int[] MovePower { get => m_movePower; }
    /// <summary> 対荷重量 </summary>
    public int[] LoadCapacity { get => _loadCapacity; }
    /// <summary> バランス性能 </summary>
    public int[] _Balancer { get => _landingBalancer; }
}
