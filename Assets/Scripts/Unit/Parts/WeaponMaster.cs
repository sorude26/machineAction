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
    /// <summary> 所有者の物理 </summary>
    public virtual Rigidbody OwnerRb { get; set; }
    /// <summary>
    /// 武装の破壊
    /// </summary>
    public virtual void SetBreak() => Break = true;
    /// <summary>
    /// 攻撃開始
    /// </summary>
    public abstract void AttackAction();
    public virtual void AttackEnd() { }
    public virtual float AttackSpeed() { return 1; }
}
