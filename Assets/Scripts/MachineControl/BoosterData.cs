using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoosterData : UnitPartsData
{
    [Tooltip("推進力")]
    [SerializeField]
    int[] _propulsion;
    [Tooltip("エネルギー")]
    [SerializeField]
    int[] _energy;

    /// <summary> 推進力 </summary>
    public int[] Propulsion { get => _propulsion; }
    /// <summary> エネルギー </summary>
    public int[] Energy { get => _energy; }
}
