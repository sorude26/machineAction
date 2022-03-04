using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 腕のデータ
/// </summary>
[CreateAssetMenu]
public class ArmData : UnitPartsData
{
    [Tooltip("推進力")]
    [SerializeField]
    int[] _propulsion = default;

    /// <summary> 推進力 </summary>
    public int[] Propulsion { get => _propulsion; }
}
