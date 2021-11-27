using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 機体の構成データ
/// </summary>
[System.Serializable]
public struct UnitBuildData 
{
    public int HeadID;
    public int BodyID;
    public int RArmID;
    public int LArmID;
    public int LegID;
    public int WeaponRArmID;
    public int WeaponLArmID;
    public int ShoulderWeaponID;
    public int BodyWeaponID;
    public int BoosterID;
    public int CoreID;
    public UnitBuildData(int head,int body,int rArm,int lArm,int leg,int weaponRA,int weaponLA)
    {
        HeadID = head;
        BodyID = body;
        RArmID = rArm;
        LArmID = lArm;
        LegID = leg;
        WeaponRArmID = weaponRA;
        WeaponLArmID = weaponLA;
        ShoulderWeaponID = 0;
        BodyWeaponID = 0;
        BoosterID = 0;
        CoreID = 0;
    }
    public UnitBuildData(int head, int body, int rArm, int lArm, int leg, int weaponRA, int weaponLA,int shoulder,int weaponBody,int booster,int core)
    {
        HeadID = head;
        BodyID = body;
        RArmID = rArm;
        LArmID = lArm;
        LegID = leg;
        WeaponRArmID = weaponRA;
        WeaponLArmID = weaponLA;
        ShoulderWeaponID = shoulder;
        BodyWeaponID = weaponBody;
        BoosterID = booster;
        CoreID = core;
    }
}
public enum PartsType
{
    Body,
    Head,
    RArm,
    LArm,
    Leg,
    Weapon,
}
/// <summary>
/// 全パーツを保有するオブジェクト
/// </summary>
[CreateAssetMenu]
public class UnitPartsList : ScriptableObject
{
    [SerializeField] PartsBody[] _bodys;
    [SerializeField] PartsHead[] _heads;
    [SerializeField] PartsArm[] _arms;
    [SerializeField] PartsLeg[] _legs;
    [SerializeField] WeaponMaster[] _weapons;
    [SerializeField] ShoulderWeapon[] _shoulderWeapons;
    public PartsBody GetBody(int id) => _bodys.Where(parts => parts.PartsID == id).FirstOrDefault();
    public PartsHead GetHead(int id) => _heads.Where(parts => parts.PartsID == id).FirstOrDefault();
    public PartsArm GetRArm(int id) => _arms.Where(parts => parts.PartsID == id && parts.Arm == ArmType.Right).FirstOrDefault();
    public PartsArm GetLArm(int id) => _arms.Where(parts => parts.PartsID == id && parts.Arm == ArmType.Left).FirstOrDefault();
    public PartsLeg GetLeg(int id) => _legs.Where(parts => parts.PartsID == id).FirstOrDefault();
    public WeaponMaster GetWeapon(int id) => _weapons.Where(parts => parts.PartsID == id).FirstOrDefault();
    public ShoulderWeapon GetShoulderWeapon(int id) => _shoulderWeapons.Where(parts => parts.PartsID == id).FirstOrDefault();
    public PartsBody[] GetAllBodys() => _bodys;
    public PartsHead[] GetAllHeads() => _heads;
    public PartsArm[] GetAllRArms() => _arms.Where(parts => parts.Arm == ArmType.Right).ToArray();
    public PartsArm[] GetAllLArms() => _arms.Where(parts => parts.Arm == ArmType.Left).ToArray();
    public PartsLeg[] GetAllLegs() => _legs;
    public WeaponMaster[] GetAllWeapons() => _weapons;
    public ShoulderWeapon[] GetAllShoulderWeapons() => _shoulderWeapons;
    public PartsBody[] GetHaveAllBody()
    {
        List<PartsBody> allParts = new List<PartsBody>();
        for (int i = 0; i < _bodys.Length; i++)
        { 
            if (UnitDataMaster.HavePartsDic[PartsType.Body][i] > 0)
            {
                allParts.Add(GetBody(i));
            }
        }
        return allParts.ToArray();
    }
    public PartsHead[] GetHaveAllHead()
    {
        List<PartsHead> allParts = new List<PartsHead>();
        for (int i = 0; i < _heads.Length; i++)
        {
            if (UnitDataMaster.HavePartsDic[PartsType.Head][i] > 0)
            {
                allParts.Add(GetHead(i));
            }
        }
        return allParts.ToArray();
    }
    public PartsArm[] GetHaveAllRArm()
    {
        List<PartsArm> allParts = new List<PartsArm>();
        for (int i = 0; i < GetAllRArms().Length; i++)
        {
            if (UnitDataMaster.HavePartsDic[PartsType.RArm][i] > 0)
            {
                allParts.Add(GetRArm(i));
            }
        }
        return allParts.ToArray();
    }
    public PartsArm[] GetHaveAllLArm()
    {
        List<PartsArm> allParts = new List<PartsArm>();
        for (int i = 0; i < GetAllLArms().Length; i++)
        {
            if (UnitDataMaster.HavePartsDic[PartsType.LArm][i] > 0)
            {
                allParts.Add(GetLArm(i));
            }
        }
        return allParts.ToArray();
    }
    public PartsLeg[] GetHaveAllLeg()
    {
        List<PartsLeg> allParts = new List<PartsLeg>();
        for (int i = 0; i < _legs.Length; i++)
        {
            if (UnitDataMaster.HavePartsDic[PartsType.Leg][i] > 0)
            {
                allParts.Add(GetLeg(i));
            }
        }
        return allParts.ToArray();
    }
    public WeaponMaster[] GetHaveAllWeapon()
    {
        List<WeaponMaster> allParts = new List<WeaponMaster>();
        for (int i = 0; i < _weapons.Length; i++)
        {
            if (UnitDataMaster.HavePartsDic[PartsType.Weapon][i] > 0)
            {
                allParts.Add(GetWeapon(i));
            }
        }
        return allParts.ToArray();
    }
}
