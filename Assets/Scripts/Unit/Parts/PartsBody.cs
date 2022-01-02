using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 胴体パーツ
/// </summary>
public class PartsBody : UnitPartsMaster<BodyData>
{
    [Tooltip("頭部パーツ接続部")]
    [SerializeField]
    Transform _headParts = default;
    [Tooltip("左手パーツ接続部")]
    [SerializeField]
    Transform _lArmParts = default;
    [Tooltip("右手パーツ接続部")]
    [SerializeField] 
    Transform _rArmParts = default;
    [Tooltip("バックパックパーツ接続部")]
    [SerializeField]
    Transform _backPartsPos = default;
    [Tooltip("内蔵武器")]
    [SerializeField]
    WeaponMaster _weapon = default;
    [Tooltip("肩武器")]
    [SerializeField]
    WeaponMaster _weaponShoulder =default;
    [Tooltip("破壊時エフェクト")]
    [SerializeField]
    GameObject _breakSpark = default;
    /// <summary> 胴体破壊時のイベント </summary>
    public event Action OnBodyBreak;
    /// <summary> 機体出力 </summary>
    public int UnitOutput { get => _partsData.UnitOutput[_partsID]; }
    /// <summary> 昇降力 </summary>
    public float LiftingForce { get => _partsData.LiftingForce[_partsID]; }
    /// <summary> 移動力 </summary>
    public int MovePower { get => _partsData.MovePower[_partsID]; }
    /// <summary> 命中精度 </summary>
    public int HitAccuracy { get => _partsData.HitAccuracy[_partsID]; }
    /// <summary> 機体タイプ </summary>
    public UnitType BodyPartsType { get => _partsData.BodyPartsType[_partsID]; }   
    /// <summary> 頭部パーツ接続部 </summary>
    public Transform HeadPos { get => _headParts; }
    /// <summary> 左手パーツ接続部 </summary>
    public Transform LArmPos { get => _lArmParts; }
    /// <summary> 右手パーツ接続部 </summary>
    public Transform RArmPos { get => _rArmParts; }
    public Transform BackPos { get => _backPartsPos; }
    /// <summary> 内蔵武器 </summary>
    public WeaponMaster BodyWeapon { get => _weapon; }
    /// <summary> 肩装備武器 </summary>
    public WeaponMaster ShoulderWeapon { get => _weaponShoulder; }

    protected override void PartsBreak()
    {        
        Break = true;
        EffectPool.Get(EffectType.ExplosionMachine, transform.position);
        OnBodyBreak?.Invoke();
        if (_breakSpark != null)
        {
            _breakSpark.SetActive(true);
        }
    }   
    /// <summary>
    /// 機体の回避力と出力の合計値を返す
    /// </summary>
    /// <returns></returns>
    public int GetAvoidance() => _partsData.Avoidance[_partsID] + UnitOutput;
    /// <summary>
    /// 武装込みのサイズを返す
    /// </summary>
    /// <returns></returns>
    public override int GetSize()
    {
        int size = PartsSize;
        if (_weapon)
        {
            size += _weapon.PartsSize;
        }
        if (_weaponShoulder)
        {
            size += _weaponShoulder.PartsSize;
        }
        return size;
    }
    void DestroyEnd()
    {
        gameObject.SetActive(false);
    }
}
