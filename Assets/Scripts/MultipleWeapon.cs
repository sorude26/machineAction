using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleWeapon : WeaponMaster
{
    [SerializeField]
    WeaponMaster[] _weapons = default;
    public override Rigidbody OwnerRb 
    { 
        get => base.OwnerRb;
        set
        { 
            base.OwnerRb = value;
            foreach (var weapon in _weapons)
            {
                weapon.OwnerRb = value;
            }
        }
    }
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
