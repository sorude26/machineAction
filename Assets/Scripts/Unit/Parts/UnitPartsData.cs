using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 機体のタイプ
/// </summary>
public enum UnitType
{
    Human,
    Walker,
    Helicopter,
    Tank,
    Giant,
}
/// <summary>
/// 手の種類
/// </summary>
public enum ArmType
{
    Left,
    Right,
}
/// <summary>
/// 脚の種類
/// </summary>
public enum LegType
{
    Normal,
    Crawler,
    Animation,
}
/// <summary>
/// 機体パーツデータの基底クラス
/// </summary>
[System.Serializable]
public class UnitPartsData : PartsData
{
    [Tooltip("パーツ耐久値")]
    [SerializeField] protected int[] _partsHp;
    [Tooltip("パーツ装甲値")]
    [SerializeField] protected int[] _defense;
    /// <summary> パーツ耐久値 </summary>
    public int[] MaxPartsHp { get => _partsHp; }
    /// <summary> パーツ装甲値 </summary>
    public int[] Defense { get => _defense; }
}
