using System.Collections;
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
        _shotCount--;
        Shot();
    }
    void ShotEnd()
    {
        _shotNow = false;
    }
    void ShotStart()
    {
        if (_shotCount > 0)
        {
            _shotNow = true;
        }
    }
    public override float AttackSpeed()
    {
        return _power;
    }
}
