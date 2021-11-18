using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユニットの武器を含めた全パーツの基底クラス
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class PartsMaster<T> : MonoBehaviour, IParts where T:PartsData
{
    [Tooltip("パーツの基礎ID")]
    [SerializeField] protected int _partsID;
    [Tooltip("パーツのデータ")]
    [SerializeField] protected T _partsData;
    [Tooltip("表示が切り替わるパーツ")]
    [SerializeField] protected GameObject[] _partsObject;
    [Tooltip("パーツのアニメーション")]
    [SerializeField] protected Animator _anime;
    /// <summary> パーツID </summary>
    public int PartsID { get => _partsID; }
    /// <summary> パーツ名 </summary>
    public string PartsName { get => _partsData.PartsName[_partsID]; }
    /// <summary> 重量 </summary>
    public int Weight { get => _partsData.Weight[_partsID]; }
    /// <summary> パーツサイズ </summary>
    public int PartsSize { get => _partsData.PartsSize[_partsID]; }
    /// <summary> 破壊フラグ </summary>
    public bool Break { get; protected set; }
    /// <summary> パーツの固有アニメーション </summary>
    public Animator PartsAnime { get => _anime; }
    public virtual int GetSize() => PartsSize;
    public virtual void DestoryParts() => Destroy(this.gameObject);
}
