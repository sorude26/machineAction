using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationShotWeapon : ShotWeapon
{
    protected override IEnumerator BulletShot()
    {
        _anime.SetFloat("BarrelSpeed", _shotInterval);
        _anime.Play("startShot");
        while (_shotCount > 0)
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
    public override float AttackSpeed()
    {
        return _power;
    }
}
