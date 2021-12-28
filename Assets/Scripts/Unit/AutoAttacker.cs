using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttacker : MonoBehaviour
{
    [SerializeField]
    Transform _target = default;
    [SerializeField]
    Transform _weaponBase = default;
    [SerializeField]
    Transform _targetLock = default;
    [SerializeField]
    WeaponMaster _weapon = default;
    [SerializeField]
    float _aimRange = 0.2f;
    [SerializeField]
    float _aimSpeed = 3.0f;
    [SerializeField]
    float _attackInterval = 1f;
    float _attackTimer = 0;
    Vector3 _beforePos = default;
    Vector3 _beforePos2 = default;
    Quaternion _aimRotaion = Quaternion.Euler(0, 0, 0);
    void Update()
    {
        Vector3 targetDir = DeviationShootingControl.CirclePrediction(_weaponBase.position, _target.position, _beforePos, _beforePos2, _weapon.AttackSpeed() * _aimRange);
        _targetLock.forward = targetDir - _targetLock.position;
        _aimRotaion = _targetLock.localRotation;
        _beforePos2 = _beforePos;
        _beforePos = _target.position;
        _attackTimer += Time.deltaTime;
        if (_attackTimer >= _attackInterval)
        {
            _attackTimer = 0;
            _weapon.AttackAction();
        }
        if (_aimSpeed > 0)
        {
            _weaponBase.localRotation = Quaternion.Lerp(_weaponBase.localRotation, _aimRotaion, _aimSpeed * Time.deltaTime);
        }
        else
        {
            _weaponBase.localRotation = _aimRotaion;
        }
    }
}
