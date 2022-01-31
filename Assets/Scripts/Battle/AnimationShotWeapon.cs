﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationShotWeapon : ShotWeapon
{
    protected bool _shotNow = false;
    protected override IEnumerator BulletShot()
    {
        _anime.SetFloat("BarrelSpeed", _shotInterval);
        _anime.Play("startShot");
        while (_shotCount > 0 || _shotNow)
        {
            yield return null;
        }
        _anime.Play("endShot");
        ShotNow = false;
    }
    private void AnimationShot()
    {
        if (_shotCount > 0)
        {
            _shotCount--;
            Shot();
        }
    }
    void ShotEnd()
    {
        _shotNow = false;
    }
    void StartShot()
    {
        StartShot(_target);
    }
    void ShotStart()
    {
        if (_shotCount > 0)
        {
            _shotNow = true;
        }
        else
        {
            _shotNow = false;
        }
    }
    public override float AttackSpeed()
    {
        return _power;
    }
}
