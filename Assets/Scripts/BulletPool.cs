using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Normal,
    Flame,
    Explosive,
    Grenade,
    Atomic,
}
public class BulletPool : ObjectPoolBase<Shot,BulletType>
{
}
