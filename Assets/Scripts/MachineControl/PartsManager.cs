using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsManager
{
    public PartsHead Head { get; set; }
    public PartsBody Body { get; set; }
    public PartsArm RArm { get; set; }
    public PartsArm LArm { get; set; }
    public PartsLeg Leg { get; set; }
    public PartsBooster Booster { get; set; }
    public WeaponMaster RAWeapon { get; set; }
    public WeaponMaster LAWeapon { get; set; }
    public WeaponMaster BodyWeapon { get; set; }
    public ShoulderWeapon ShoulderWeapon { get; set; }
    public IUnitParts[] GetAllParts()
    {
        IUnitParts[] AllParts = { Head, Body, RArm, LArm, Leg, Booster };
        return AllParts;
    }
}
