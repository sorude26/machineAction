using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 全パーツデータの基底クラス
/// </summary>
public class PartsData : ScriptableObject
{
    [Tooltip("パーツID")]
    [SerializeField] int[] _partsID;
    [Tooltip("パーツ名")]
    [SerializeField] string[] _partsName;
    [Tooltip("重量")]
    [SerializeField] protected int[] _weight;
    [Tooltip("パーツサイズ")]
    [SerializeField] int[] _partsSize;
    /// <summary> パーツID </summary>
    public int[] PartsID { get => _partsID; }
    /// <summary> パーツ名 </summary>
    public string[] PartsName { get => _partsName; }
    /// <summary> 重量 </summary>
    public int[] Weight { get => _weight; }
    /// <summary> パーツサイズ </summary>
    public int[] PartsSize { get => _partsSize; }
}
