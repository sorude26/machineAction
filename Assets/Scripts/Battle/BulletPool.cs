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
}
public class BulletPool : ObjectPoolBase<Shot,BulletType>
{
}
