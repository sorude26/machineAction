﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器の基底クラス
/// </summary>
public abstract class WeaponMaster : PartsMaster<WeaponData>
{
    /// <summary> 武器攻撃力 </summary>
    public int Power { get => m_partsData.Power[m_partsID]; }
    /// <summary> 総攻撃回数 </summary>
    public int MaxAttackNumber { get => m_partsData.MaxAttackNumber[m_partsID]; }
    /// <summary> 最大攻撃力 </summary>
    public int MaxPower { get => m_partsData.Power[m_partsID] * m_partsData.Power[m_partsID] + m_partsData.Power[m_partsID] * m_partsData.MaxAttackNumber[m_partsID]; }
    /// <summary> 命中精度 </summary>
    public int HitAccuracy { get => m_partsData.HitAccuracy[m_partsID]; }
    /// <summary> 最大射程 </summary>
    public int Range { get => m_partsData.Range[m_partsID]; }
    /// <summary> 最低射程 </summary>
    public int MinRange { get => m_partsData.MinRange[m_partsID]; }
    /// <summary> 最大対応高低差 </summary>
    public float VerticalRange { get => m_partsData.VerticalRange[m_partsID]; }
    /// <summary> 武器種 </summary>
    public WeaponType Type { get => m_partsData.Type[m_partsID]; }
    /// <summary> 武装部位 </summary>
    public WeaponPosition WeaponPos { get; private set; }
    /// <summary> 攻撃種類のデリゲート </summary>
    protected Action<WeaponType,int> m_attackMode;
    /// <summary> 攻撃種類のイベント </summary>
    public event Action<WeaponType, int> OnAttackMode { add => m_attackMode += value; remove => m_attackMode -= value; }
    /// <summary> 攻撃開始時のデリゲート </summary>
    protected Action m_attackStart;
    /// <summary> 攻撃開始時のイベント </summary>
    public event Action OnAttackStart { add => m_attackStart += value; remove => m_attackStart -= value; }
    /// <summary> 攻撃のデリゲート </summary>
    protected Action m_attack;
    /// <summary> 攻撃のイベント </summary>
    public event Action OnAttack { add => m_attack += value; remove => m_attack -= value; }
    /// <summary> 攻撃終了時のデリゲート </summary>
    protected Action m_attackEnd;
    /// <summary> 攻撃終了時のイベント </summary>
    public event Action OnAttackEnd { add => m_attackEnd += value; remove => m_attackEnd -= value; }
    
    /// <summary>
    /// 武装部位を設定する
    /// </summary>
    /// <param name="position"></param>
    public void SetWeaponPosition(WeaponPosition position) => WeaponPos = position;
    /// <summary>
    /// 武装の破壊
    /// </summary>
    public virtual void SetBreak() => Break = true;
    /// <summary>
    /// 攻撃開始
    /// </summary>
    public abstract void AttackStart();
}
