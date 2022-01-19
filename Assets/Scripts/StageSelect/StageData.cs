using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : ScriptableObject
{
    [SerializeField]
    private int _stageID = default;
    public int StageID { get => _stageID; }
}
