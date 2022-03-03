using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PartsDataList : ScriptableObject
{
    [SerializeField]
    HeadData _headData = default;
    [SerializeField]
    BodyData _bodyData = default;
    [SerializeField]
    ArmData _armData = default;
    [SerializeField]
    LegData _legData = default;
    [SerializeField]
    BoosterData _boosterData = default;
    public HeadData Head { get => _headData; }
    public BodyData Body { get => _bodyData; }
    public ArmData Arm { get => _armData; }
    public LegData Leg { get => _legData; }
    public BoosterData Booster { get => _boosterData; }
}
