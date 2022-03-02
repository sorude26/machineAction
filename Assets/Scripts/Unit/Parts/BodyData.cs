using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 胴体のデータ、全種類のユニットに必ず設定される
/// </summary>
[CreateAssetMenu]
public class BodyData : UnitPartsData
{
    [Tooltip("機体出力")]
    [SerializeField] int[] _output;
    [Tooltip("旋回限界")]
    [SerializeField] int[] _turnRange;
    /// <summary> 機体出力 </summary>
    public int[] Output { get => _output; }
    /// <summary> 移動力 </summary>
    public int[] TurnRange { get => _turnRange; }
}
