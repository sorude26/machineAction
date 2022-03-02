using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 頭部データ
/// </summary>
[CreateAssetMenu]
public class HeadData : UnitPartsData
{
    [Tooltip("命中精度")]
    [SerializeField] int[] _lockRange;
    [Tooltip("情報処理能力")]
    [SerializeField] int[] _processingPerformance;
    /// <summary> 命中精度 </summary>
    public int[] LockRange { get => _lockRange; }
    /// <summary> 情報処理能力 </summary>
    public int[] Performance { get => _processingPerformance; }
}
