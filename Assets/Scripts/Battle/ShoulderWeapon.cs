using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderWeapon : MultipleWeapon
{
    [SerializeField]
    Transform _lShoulder = default;
    [SerializeField]
    Transform _rShoulder = default;
    public Transform LShoulder { get => _lShoulder; }
    public Transform RShoulder { get => _rShoulder; }
}
