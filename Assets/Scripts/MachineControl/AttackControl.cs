using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControl
{
    string[] _lArmBlade = { "attackSwingLArm", "attackSwingLArm2", "attackSwingLArm3" };
    string[] _rArmBlade = { "attackSwingRArm", "attackSwingRArm2", "attackSwingRArm3" };
    string[] _dArmBlade = { "attackSwingDArm", "attackSwingDArm2", "attackSwingDArm3" };
    string[] _lArmKnuckle = { "attackKnuckleLArm", "attackKnuckleLArm", "attackKnuckleLArm" };
    string[] _rArmKnuckle = { "attackKnuckleRArm", "attackKnuckleRArm", "attackKnuckleRArm" };
    string[] _dArmKnuckle = { "attackKnuckleRArm", "attackKnuckleLArm", "attackKnuckleRArm" };
    string[] _lArmBladeRKnuckle = { "attackSwingLArm", "attackSwingLArm2", "attackKnuckleRArm" };
    string[] _rArmBladeLKnuckle = { "attackSwingRArm", "attackSwingRArm2", "attackKnuckleLArm" };
    /// <summary>
    /// 格闘攻撃のアニメション名を返す
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public string AttackAction(FightingType type,int count)
    {
        switch (type)
        {
            case FightingType.None:
                break;
            case FightingType.LArmBlade:
                return _lArmBlade[count];
            case FightingType.RArmBlade:
                return _rArmBlade[count];
            case FightingType.DArmBlade:
                return _dArmBlade[count];
            case FightingType.LArmKnuckle:
                return _lArmKnuckle[count];
            case FightingType.RArmKnuckle:
                return _rArmKnuckle[count];
            case FightingType.DArmKnuckle:
                return _dArmKnuckle[count];
            case FightingType.LBladeRKnuckle:
                return _lArmBladeRKnuckle[count];
            case FightingType.RBladeLKnuckle:
                return _rArmBladeLKnuckle[count];
            default:
                break;
        }
        return "attack";
    }
    public FightingType GetType(MachineController machine)
    {
        if (machine.LAWeapon.Type == WeaponType.Blade)
        {
            if (machine.RAWeapon.Type == WeaponType.Blade)
            {
                return FightingType.DArmBlade;
            }
            else if (machine.RAWeapon.Type == WeaponType.Knuckle)
            {
                return FightingType.LBladeRKnuckle;
            }
            else
            {
                return FightingType.LArmBlade;
            }
        }
        else if (machine.LAWeapon.Type == WeaponType.Knuckle)
        {
            if (machine.RAWeapon.Type == WeaponType.Blade)
            {
                return FightingType.RBladeLKnuckle;
            }
            else if (machine.RAWeapon.Type == WeaponType.Knuckle)
            {
                return FightingType.DArmKnuckle;
            }
            else
            {
                return FightingType.LArmKnuckle;
            }
        }
        else
        {
            if (machine.RAWeapon.Type == WeaponType.Blade)
            {
                return FightingType.RArmBlade;
            }
            else if (machine.RAWeapon.Type == WeaponType.Knuckle)
            {
                return FightingType.RArmKnuckle;
            }
            else
            {
                return FightingType.None;
            }
        }
    }
}
/// <summary>
/// 格闘攻撃タイプ
/// </summary>
public enum FightingType
{
    None,
    LArmBlade,
    RArmBlade,
    DArmBlade,
    LArmKnuckle,
    RArmKnuckle,
    DArmKnuckle,
    LBladeRKnuckle,
    RBladeLKnuckle,
}
