using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    ShotHit,
    Bom,
    Explosion,
    ExplosionMachine,
    AtomicBom,
    Fire,
    Spark,
    BomSpark,
    HeavyExplosion,
    Stealth,
    Energy,
}
public class EffectPool : ObjectPoolBase<EffectControl,EffectType>
{
    protected override void GetAction(EffectType type, Vector3 pos)
    {
        switch (type)
        {
            case EffectType.ShotHit:
                break;
            case EffectType.Bom:
                CameraEffectManager.LightShake(pos);
                break;
            case EffectType.Explosion:
                CameraEffectManager.ExplosionShake(pos, 1f);
                break;
            case EffectType.ExplosionMachine:
                CameraEffectManager.ExplosionShake(pos, 2f);
                break;
            case EffectType.AtomicBom:
                CameraEffectManager.ExplosionShake(pos, 8f);
                break;
            case EffectType.Fire:
                break;
            case EffectType.Spark:
                break;
            case EffectType.BomSpark:
                CameraEffectManager.LightShake(pos);
                break;
            case EffectType.HeavyExplosion:
                CameraEffectManager.ExplosionShake(pos, 1f);
                break;
            case EffectType.Stealth:
                break;
            case EffectType.Energy:
                CameraEffectManager.Shake(pos);
                break;
            default:
                break;
        }
    }
}

