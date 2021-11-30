using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 近接攻撃武器
/// </summary>
public class MeleeWeapon : WeaponMaster
{
    [SerializeField] GameObject _blade;
    public override void AttackAction()
    {
        _blade.SetActive(true);
    }
    public override void AttackEnd()
    {
        _blade.SetActive(false);
    }
}
