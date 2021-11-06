﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 全パーツデータの基底クラス
/// </summary>
public class PartsData : ScriptableObject
{
    [Tooltip("パーツID")]
    [SerializeField] int m_partsID;
    [Tooltip("パーツ名")]
    [SerializeField] string[] m_partsName;
    [Tooltip("重量")]
    [SerializeField] protected int[] m_weight;
    [Tooltip("パーツサイズ")]
    [SerializeField] int[] m_partsSize;
    /// <summary> パーツID </summary>
    public int PartsID { get => m_partsID; }
    /// <summary> パーツ名 </summary>
    public string[] PartsName { get => m_partsName; }
    /// <summary> 重量 </summary>
    public int[] Weight { get => m_weight; }
    /// <summary> パーツサイズ </summary>
    public int[] PartsSize { get => m_partsSize; }
}
