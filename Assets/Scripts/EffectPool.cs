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
}
public class EffectPool : ObjectPoolBase<EffectControl,EffectType>
{
    
}

