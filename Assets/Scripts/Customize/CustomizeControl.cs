using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeControl : MonoBehaviour
{
    [SerializeField]
    UnitBuildData _unitBuildData = default;
    [SerializeField]
    MachineBuildControl _buildControl = default;
    PartsManager _partsManager = default;
    private void Awake()
    {
        _partsManager = new PartsManager();
    }
    public void Build()
    {
        _partsManager.ResetAllParts();
        _buildControl.SetData(_unitBuildData);
        _buildControl.StartSet(_partsManager);
    }
}
