using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleWeapon : WeaponMaster
{
    [SerializeField]
    protected WeaponMaster[] _weapons = default;
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
    public override void AttackAction(Vector3 target)
    {
        foreach (var weapon in _weapons)
        {
            weapon.AttackAction(target);
        }
    }
    public override float AttackSpeed()
    {
        return _weapons[0].AttackSpeed();
    }
    public override void DestoryParts()
    {
        foreach (var weapon in _weapons)
        {
            weapon.DestoryParts();
        }
        base.DestoryParts();
    }
}
