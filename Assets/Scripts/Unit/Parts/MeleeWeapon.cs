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
    AttackTriger[] _trigers = default;
    [SerializeField]
    float _knockBackPower = 1f;
    [SerializeField]
    float _speedPower = 1f;
    private void Start()
    {
        if (_triger != null)
        {
            _triger.OnHit += HitKnockBack;
        }
        foreach (var triger in _trigers)
        {
            triger.OnDamage += HitStop;
        }
        //SetPower(_partsData.Power[PartsID]);
    }
    public void SetPower(int power)
    {
        foreach (var triger in _trigers)
        {
            triger.SetPower = power;
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
        if (_knockBackPower > 0)
        {
            OwnerRb.AddForce(-OwnerRb.transform.forward * _knockBackPower, ForceMode.Impulse);
        }
    }
    void HitStop()
    {
        GameScene.TimeManager.Instance.HitStop();
        CameraEffectManager.LightShake(transform.position);
    }
    public override float AttackSpeed()
    {
        return _speedPower;
    }
}
