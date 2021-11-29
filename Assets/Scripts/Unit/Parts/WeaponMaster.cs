using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器の基底クラス
/// </summary>
public abstract class WeaponMaster : PartsMaster<WeaponData>
{
    /// <summary> 武器攻撃力 </summary>
    public int Power { get => _partsData.Power[_partsID]; }
    /// <summary> 総攻撃回数 </summary>
    public int MaxAttackNumber { get => _partsData.MaxAttackNumber[_partsID]; }
    /// <summary> 最大攻撃力 </summary>
    public int MaxPower { get => _partsData.Power[_partsID] * _partsData.Power[_partsID] + _partsData.Power[_partsID] * _partsData.MaxAttackNumber[_partsID]; }
    /// <summary> 命中精度 </summary>
    public int HitAccuracy { get => _partsData.HitAccuracy[_partsID]; }
    /// <summary> 最大射程 </summary>
    public int Range { get => _partsData.Range[_partsID]; }
    /// <summary> 最低射程 </summary>
    public int MinRange { get => _partsData.MinRange[_partsID]; }
    /// <summary> 武器種 </summary>
    public WeaponType Type { get => _partsData.Type[_partsID]; }
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
    public abstract void AttackAction();
    public virtual void AttackEnd() { }
    public virtual float AttackSpeed() { return 0; }
}
