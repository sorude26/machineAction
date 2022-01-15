using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユニットパーツが持つ情報を返す為のインターフェース
/// </summary>
public interface IUnitParts : IParts, IDamageApplicable
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
    /// <summary>
    /// ゲージ設定
    /// </summary>
    /// <param name="gauge"></param>
    void SetGauge(GaugeControl gauge);
    /// <summary>
    /// メインブースター起動
    /// </summary>
    void StartBooster();
    /// <summary>
    /// 前進ブースター起動
    /// </summary>
    void StartBoosterF();
    /// <summary>
    /// 後退ブースター起動
    /// </summary>
    void StartBoosterB();
    /// <summary>
    /// 左ブースター起動
    /// </summary>
    void StartBoosterL();
    /// <summary>
    /// 右ブースター起動
    /// </summary>
    void StartBoosterR();
    /// <summary>
    /// ブースター停止
    /// </summary>
    void StopBooster();
}
