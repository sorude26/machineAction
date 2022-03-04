using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 頭部データ
/// </summary>
[CreateAssetMenu]
public class HeadData : UnitPartsData
{
    [Tooltip("索敵範囲")]
    [SerializeField] float[] _lockRange;
    [Tooltip("情報処理能力")]
    [SerializeField] float[] _processingPerformance;
    /// <summary> 索敵範囲 </summary>
    public float[] LockRange { get => _lockRange; }
    /// <summary> 情報処理能力 </summary>
    public float[] Performance { get => _processingPerformance; }
}
