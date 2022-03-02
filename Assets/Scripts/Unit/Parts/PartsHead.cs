using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 頭部パーツ
/// </summary>
public class PartsHead : UnitPartsMaster<HeadData>
{
    public float LockOnRange { get => _partsData.LockRange[_dataID]; }
    public float Performance { get => _partsData.Performance[_dataID]; }
}
