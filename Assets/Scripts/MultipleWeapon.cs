using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleWeapon : WeaponMaster
{
    [SerializeField]
    ShotWeapon[] _weapons = default;
    public override void AttackAction()
    {
        foreach (var weapon in _weapons)
        {
            weapon.AttackAction();
        }
    }
    public override float AttackSpeed()
    {
        return _weapons[0].AttackSpeed();
    }
}
