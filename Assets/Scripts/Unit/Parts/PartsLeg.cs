using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
/// <summary>
/// 脚部パーツ
/// </summary>
public class PartsLeg : UnitPartsMaster<LegData>
{
    /// <summary> 移動力 </summary>
    public float MovePower { get => _partsData.MovePower[_dataID]; }
    /// <summary> 対荷重量 </summary>
    public int LoadCapacity { get => _partsData.LoadCapacity[_dataID]; }
    /// <summary> バランス性能 </summary>
    public int Balancer { get => _partsData.Balancer[_dataID]; }
    /// <summary> 運動性能 </summary>
    public int Exercise { get => _partsData.Exercise[_dataID]; }
    /// <summary> 空中移動力 </summary>
    public float FloatPower { get => _partsData.FloatPower[_dataID]; }
    /// <summary> 脚部の種類 </summary>
    public LegType Type { get => _legType; }

    [FormerlySerializedAs("m_legTop")]
    [Tooltip("脚部パーツの頂点")]
    [SerializeField]
    Transform _legTop = default;
    [SerializeField]
    Transform m_lLeg1 = default;
    [SerializeField]
    Transform m_lLeg2 = default;
    [SerializeField]
    Transform m_lLeg3 = default;
    [SerializeField]
    Transform m_rLeg1 = default;
    [SerializeField]
    Transform m_rLeg2 = default;
    [SerializeField]
    Transform m_rLeg3 = default;

    [Tooltip("脚部パーツのアニメーション操作機能")]
    [SerializeField]
    MoveAnimation _moveAnimation = default;
    [SerializeField]
    LegType _legType = default;
    /// <summary> 脚部パーツの頂点 </summary>
    public Transform LegTop { get => _legTop; }
    public Transform LLeg1 { get => m_lLeg1; }
    public Transform LLeg2 { get => m_lLeg2; }
    public Transform LLeg3 { get => m_lLeg3; }
    public Transform RLeg1 { get => m_rLeg1; }
    public Transform RLeg2 { get => m_rLeg2; }
    public Transform RLeg3 { get => m_rLeg3; }
    public MoveAnimation MoveAnimation { get => _moveAnimation; }

    public override void DestoryParts()
    {
        Transform[] allParts = { m_lLeg3, m_lLeg2, m_lLeg1, m_rLeg3, m_rLeg2, m_rLeg1 };
        foreach (var parts in allParts)
        {
            Destroy(parts.gameObject);
        }
        base.DestoryParts();
    }
}
