using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    ShotHit,
    Bom,
    Explosion,
}
public class EffectPool : ObjectPoolBase<EffectControl,EffectType>
{
    
}

