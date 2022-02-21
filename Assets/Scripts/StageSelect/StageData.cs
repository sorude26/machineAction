using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StageData : ScriptableObject
{
    [SerializeField]
    int _stageType = default;
    [SerializeField]
    int _enemy = default;
    [SerializeField]
    int _battleType = default;
    [SerializeField]
    int _stageObject = default;
    public int StageType { get=> _stageType; }
    public int Enemy { get => _enemy; }
    public int Wintype { get => _battleType; } 
    public int Objects { get => _stageObject; }
}
