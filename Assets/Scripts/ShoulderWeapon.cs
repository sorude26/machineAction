using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderWeapon : WeaponMaster
{
    [SerializeField]
    ShotWeapon[] _weapons = default;
    [SerializeField]
    Transform _lShoulder = default;
    [SerializeField]
    Transform _rShoulder = default;
    public Transform LShoulder { get => _lShoulder; }
    public Transform RShoulder { get => _rShoulder; }
    public override void AttackAction()
    {
        foreach (var weapon in _weapons)
        {
            weapon.AttackAction();
        }
    }
}
