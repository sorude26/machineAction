using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoosterData : UnitPartsData
{
    [Tooltip("推進力")]
    [SerializeField]
    int[] _propulsion = default;
    [Tooltip("エネルギー")]
    [SerializeField]
    int[] _energy = default;
    [Tooltip("持続時間")]
    [SerializeField]
    float[] _duration = default;

    /// <summary> 推進力 </summary>
    public int[] Propulsion { get => _propulsion; }
    /// <summary> エネルギー </summary>
    public int[] Energy { get => _energy; }
    /// <summary> 持続時間 </summary>
    public float[] Duration { get => _duration; }
}
