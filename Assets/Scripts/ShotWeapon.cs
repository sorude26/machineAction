﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotWeapon : WeaponMaster
{
    [SerializeField]
    protected BulletType _bullet = default;
    [SerializeField]
    protected Transform _muzzle = default;
    [SerializeField]
    protected ParticleSystem[] _particle = default;
    [SerializeField]
    protected float _power = 20f;
    [SerializeField]
    protected int _triggerShotCount = 1;
    [SerializeField]
    protected float _triggerInterval = 0.1f;
    [SerializeField]
    protected float _shotInterval = 0.2f;
    [SerializeField]
    protected float _diffusivity = 0.01f;
    [SerializeField]
    protected int _diffusionShot = 0;

    protected float _triggerTimer = 0;
    protected bool _trigger = false;
    protected int _shotCount = 0;
    public bool ShotNow { get; protected set; }

    public virtual void Shot()
    {
        if (_particle.Length > 0)
        {
            foreach (var particle in _particle)
            {
                particle.Play();
            }
        }
        if (_diffusionShot > 0)
        {
            DiffusionShot();
        }
        Vector3 moveDir = _muzzle.forward.normalized;
        moveDir.x += Random.Range(-_diffusivity, _diffusivity);
        moveDir.y += Random.Range(-_diffusivity, _diffusivity);
        moveDir.z += Random.Range(-_diffusivity, _diffusivity);
        var shot = BulletPool.Get(_bullet, _muzzle.position);
        if (shot)
        {
            shot.StartShot(moveDir.normalized, _power);
        }
        CameraController.HitShake();
    }
    protected void DiffusionShot()
    {
        for (int i = 1; i < _diffusionShot; i++)
        {
            Vector3 moveDir = _muzzle.forward.normalized;
            moveDir.x += Random.Range(-_diffusivity, _diffusivity);
            moveDir.y += Random.Range(-_diffusivity, _diffusivity);
            moveDir.z += Random.Range(-_diffusivity, _diffusivity);
            var shot = BulletPool.Get(_bullet, _muzzle.position);
            if (shot)
            {
                shot.StartShot(moveDir.normalized, _power);
            }
        }
    }
    public virtual void StartShot()
    {
        if (_triggerTimer > 0 || !gameObject.activeInHierarchy)
        {
            return;
        }
        if (_triggerInterval > 0)
        {
            if (!_trigger)
            {
                _trigger = true;
                _triggerTimer = _triggerInterval;
                _shotCount = _triggerShotCount;
                StartCoroutine(TriggerTimer());
            }
        }
        else
        {
            _shotCount = _triggerShotCount;
        }
        if (!ShotNow)
        {
            ShotNow = true;
            StartCoroutine(BulletShot());
        }
    }
    protected virtual IEnumerator BulletShot()
    {
        float timer = _shotInterval;
        while (_shotCount > 0)
        {
            timer += Time.deltaTime;           
            if (timer >= _shotInterval)
            {
                Shot();
                _shotCount--;
                timer = 0;
            }
            yield return null;
        }
        ShotNow = false;
    }
    protected IEnumerator TriggerTimer()
    {
        while (_triggerTimer > 0)
        {
            _triggerTimer -= Time.deltaTime;
            yield return null;
        }
        _triggerTimer = 0;
        _trigger = false;
    }

    public override void AttackAction()
    {
        StartShot();
    }
    public override float AttackSpeed()
    {
        return _power;
    }
}
