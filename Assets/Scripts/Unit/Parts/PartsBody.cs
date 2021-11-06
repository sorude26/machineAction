﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 胴体パーツ
/// </summary>
public class PartsBody : UnitPartsMaster<BodyData>
{
    /// <summary> 機体出力 </summary>
    public int UnitOutput { get => m_partsData.UnitOutput[m_partsID]; }
    /// <summary> 昇降力 </summary>
    public float LiftingForce { get => m_partsData.LiftingForce[m_partsID]; }
    /// <summary> 移動力 </summary>
    public int MovePower { get => m_partsData.MovePower[m_partsID]; }
    /// <summary> 命中精度 </summary>
    public int HitAccuracy { get => m_partsData.HitAccuracy[m_partsID]; }
    /// <summary> 機体タイプ </summary>
    public UnitType BodyPartsType { get => m_partsData.BodyPartsType[m_partsID]; }
    [Tooltip("頭部パーツ接続部")]
    [SerializeField] Transform m_headParts;
    [Tooltip("左手パーツ接続部")]
    [SerializeField] Transform m_lArmParts;
    [Tooltip("右手パーツ接続部")]
    [SerializeField] Transform m_rArmParts;
    [Tooltip("内蔵武器")]
    [SerializeField] WeaponMaster m_weapon;
    [Tooltip("肩武器")]
    [SerializeField] WeaponMaster m_weaponShoulder;
    [SerializeField] Transform m_bodyPos;
    /// <summary> 頭部パーツ接続部 </summary>
    public Transform HeadPos { get => m_headParts; }
    /// <summary> 左手パーツ接続部 </summary>
    public Transform LArmPos { get => m_lArmParts; }
    /// <summary> 右手パーツ接続部 </summary>
    public Transform RArmPos { get => m_rArmParts; }
    public Transform BodyPos { get => m_bodyPos; }
    /// <summary> 内蔵武器 </summary>
    public WeaponMaster BodyWeapon { get => m_weapon; }
    /// <summary> 肩装備武器 </summary>
    public WeaponMaster ShoulderWeapon { get => m_weaponShoulder; }

    protected override void PartsBreak()
    {
        Break = true;
    }   
    /// <summary>
    /// 機体の回避力と出力の合計値を返す
    /// </summary>
    /// <returns></returns>
    public int GetAvoidance() => m_partsData.Avoidance[m_partsID] + UnitOutput;
    /// <summary>
    /// 武装込みのサイズを返す
    /// </summary>
    /// <returns></returns>
    public override int GetSize()
    {
        int size = PartsSize;
        if (m_weapon)
        {
            size += m_weapon.PartsSize;
        }
        if (m_weaponShoulder)
        {
            size += m_weaponShoulder.PartsSize;
        }
        return size;
    }
    void DestroyEnd()
    {
        gameObject.SetActive(false);
    }
}
