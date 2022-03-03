using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
