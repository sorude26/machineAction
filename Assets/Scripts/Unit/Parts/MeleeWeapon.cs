using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 近接攻撃武器
/// </summary>
public class MeleeWeapon : WeaponMaster
{
    [SerializeField] 
    GameObject _blade = default;
    [SerializeField] 
    AttackTriger _triger = default;
    [SerializeField]
    float _knockBackPower = 1f;
    private void Start()
    {
        if (_triger != null)
        {
            _triger.HitEvent += HitKnockBack;
        }
    }
    public override void AttackAction()
    {
        _blade.SetActive(true);
    }
    public override void AttackEnd()
    {
        _blade.SetActive(false);
    }
    void HitKnockBack()
    {
        OwnerRb.AddForce(-OwnerRb.transform.forward * _knockBackPower, ForceMode.Impulse);
    }
}
