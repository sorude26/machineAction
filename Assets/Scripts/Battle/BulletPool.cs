using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Normal,
    Flame,
    Explosive,
    Grenade,
    GrenadeSpark,
    Atomic,
    Fire,
    Laser,
    None,
    Energy,
}
public class BulletPool : ObjectPoolBase<Shot,BulletType>
{
    protected override void GetAction(BulletType type, Vector3 pos)
    {
        switch (type)
        {
            case BulletType.Normal:
                CameraEffectManager.SmallShake(pos);
                break;
            case BulletType.Flame:
                CameraEffectManager.SmallShake(pos);
                break;
            case BulletType.Explosive:
                CameraEffectManager.LightShake(pos);
                break;
            case BulletType.Grenade:
                CameraEffectManager.SmallShake(pos);
                break;
            case BulletType.GrenadeSpark:
                CameraEffectManager.SmallShake(pos);
                break;
            case BulletType.Atomic:
                CameraEffectManager.LightShake(pos);
                break;
            case BulletType.Fire:
                break;
            case BulletType.Laser:
                CameraEffectManager.SmallShake(pos);
                break;
            case BulletType.None:
                break;
            case BulletType.Energy:
                CameraEffectManager.SmallShake(pos);
                break;
            default:
                break;
        }
    }
}
