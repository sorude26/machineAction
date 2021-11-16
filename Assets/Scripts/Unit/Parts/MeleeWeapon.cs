using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 近接攻撃武器
/// </summary>
public class MeleeWeapon : WeaponMaster
{
    [SerializeField] GameObject m_blade;
    public override void AttackAction()
    {
        m_blade?.SetActive(true);
    }
    public override void AttackEnd()
    {
        m_blade?.SetActive(false);
    }
}
