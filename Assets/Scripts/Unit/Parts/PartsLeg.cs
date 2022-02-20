using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 脚部パーツ
/// </summary>
public class PartsLeg : UnitPartsMaster<LegData>
{
    /// <summary> 現在の移動力 </summary>
    public int CurrentMovePower { get; private set; }
    /// <summary> 現在の昇降力 </summary>
    public float CurrentLiftingForce { get; private set; }
    /// <summary> 現在の回避力 </summary>
    public int CurrentAvoidance { get; private set; }
    /// <summary> 脚部の種類 </summary>
    public LegType Type { get => _legType; }

    [UnityEngine.Serialization.FormerlySerializedAs("m_legTop")]
    [Tooltip("脚部パーツの頂点")]
    [SerializeField]
    Transform _legTop = default;
    [UnityEngine.Serialization.FormerlySerializedAs("m_lLeg1")]
    [SerializeField]
    Transform _lLeg1 = default;
    [UnityEngine.Serialization.FormerlySerializedAs("m_lLeg2")]
    [SerializeField]
    Transform _lLeg2 = default;
    [UnityEngine.Serialization.FormerlySerializedAs("m_lLeg3")]
    [SerializeField]
    Transform _lLeg3 = default;
    [UnityEngine.Serialization.FormerlySerializedAs("m_rLeg1")]
    [SerializeField]
    Transform _rLeg1 = default;
    [UnityEngine.Serialization.FormerlySerializedAs("m_rLeg2")]
    [SerializeField]
    Transform _rLeg2 = default;
    [UnityEngine.Serialization.FormerlySerializedAs("m_rLeg3")]
    [SerializeField]
    Transform _rLeg3 = default;

    [Tooltip("脚部パーツのアニメーション操作機能")]
    [SerializeField]
    MoveAnimation _moveAnimation = default;
    [SerializeField]
    LegType _legType = default;
    /// <summary> 脚部パーツの頂点 </summary>
    public Transform LegTop { get => _legTop; }
    public Transform LLeg1 { get => _lLeg1; }
    public Transform LLeg2 { get => _lLeg2; }
    public Transform LLeg3 { get => _lLeg3; }
    public Transform RLeg1 { get => _rLeg1; }
    public Transform RLeg2 { get => _rLeg2; }
    public Transform RLeg3 { get => _rLeg3; }
    public MoveAnimation MoveAnimation { get => _moveAnimation; }

    protected override void PartsBreak()
    {
        if (Break) { return; }
        CurrentAvoidance = 0;
        CurrentLiftingForce /= 5;
        CurrentMovePower /= 5;
        Break = true;
    }
    public override void DestoryParts()
    {
        Transform[] allParts = { _lLeg3, _lLeg2, _lLeg1, _rLeg3, _rLeg2, _rLeg1 };
        foreach (var parts in allParts)
        {
            Destroy(parts.gameObject);
        }
        base.DestoryParts();
    }
}
