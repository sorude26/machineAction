using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 頭部パーツ
/// </summary>
public class PartsHead : UnitPartsMaster<HeadData>
{
    /// <summary> 命中精度 </summary>
    public int HitAccuracy { get => _partsData.HitAccuracy[_partsID]; }
    /// <summary> 回避力 </summary>
    public int Avoidance { get => _partsData.Avoidance[_partsID]; }    
}
