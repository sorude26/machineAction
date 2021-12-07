using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterData : UnitPartsData
{
    [Tooltip("推進力")]
    [SerializeField] int[] _propulsion;

    /// <summary> 推進力 </summary>
    public int[] Propulsion { get => _propulsion; }
}
