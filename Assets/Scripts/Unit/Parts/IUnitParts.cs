using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユニットパーツが持つ情報を返す為のインターフェース
/// </summary>
public interface IUnitParts : IParts, IBattleEffect,IDamageApplicable
{
    /// <summary>
    /// パーツの最大耐久値
    /// </summary>
    int MaxPartsHP { get; }
    /// <summary>
    /// パーツの現在耐久値
    /// </summary>
    int CurrentPartsHp { get; }
    /// <summary>
    /// パーツの防御力
    /// </summary>
    int Defense { get; }
    /// <summary>
    /// パーツの色変更
    /// </summary>
    /// <param name="color"></param>
    void PartsColorChange(Color color);
}
