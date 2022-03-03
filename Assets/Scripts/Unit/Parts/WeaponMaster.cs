using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器の基底クラス
/// </summary>
public abstract class WeaponMaster : PartsMaster<WeaponData>
{
    [SerializeField]
    int _weight = 10;
    /// <summary> 武器種 </summary>
    public WeaponType Type { get => _partsData.Type[_partsID]; }
    /// <summary> 所有者の物理 </summary>
    public virtual Rigidbody OwnerRb { get; set; }
    public override int Weight => _weight;
    /// <summary>
    /// 武装の破壊
    /// </summary>
    public virtual void SetBreak() => Break = true;
    /// <summary>
    /// 攻撃開始
    /// </summary>
    public abstract void AttackAction(Vector3 target);
    public virtual void AttackEnd() { }
    public virtual float AttackSpeed() { return 1; }
}
