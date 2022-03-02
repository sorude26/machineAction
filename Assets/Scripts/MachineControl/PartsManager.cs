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
    public IUnitParts[] GetAllMachineParts()
    {
        IUnitParts[] allParts = { Head, Body, RArm, LArm, Leg, Booster };
        return allParts;
    }
    public IParts[] GetAllParts()
    {
        IParts[] allParts = { Head, Body, RArm, LArm, Leg, Booster, RAWeapon, LAWeapon, BodyWeapon, ShoulderWeapon };
        return allParts;
    }
    public void ChangeColor(Color color)
    {
        IUnitParts[] allParts = { Head, Body, RArm, LArm, Leg };
        foreach (var parts in allParts)
        {
            parts.PartsColorChange(color);
        }
    }
    public void ResetAllParts()
    {
        foreach (var parts in GetAllParts())
        {
            if (parts != null)
            {
                parts.DestoryParts();
            }
        }
        Head = null;
        Body = null;
        RArm = null;
        LArm = null;
        Leg = null;
        Booster = null;
        RAWeapon = null;
        LAWeapon = null;
        BodyWeapon = null;
        ShoulderWeapon = null;
    }
}
