using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 機体パーツの基底クラス
/// </summary>
/// <typeparam name="T">対応するパーツのデータ</typeparam>
[System.Serializable]
public abstract class UnitPartsMaster<T> : PartsMaster<T>, IUnitParts where T : UnitPartsData
{
    [Tooltip("攻撃命中の表示箇所")]
    [SerializeField]
    protected Transform[] _hitPos;
    [Tooltip(" 耐久値半分以下で表示する煙 ")]
    [SerializeField]
    protected GameObject _damageSmoke;
    [Tooltip("色が変更可能な装甲")]
    [SerializeField]
    protected Renderer[] m_amors;
    [Tooltip("メインブースターのパーティクル")]
    [SerializeField]
    protected ParticleSystem[] _mainBoosterParticles = default;
    [Tooltip("サブブースターのパーティクル")]
    [SerializeField]
    protected ParticleSystem[] _subBoosterParticles = default;
    [Tooltip("前進ブーストのパーティクル")]
    [SerializeField]
    protected ParticleSystem[] _frontBoosterParticles = default;
    [Tooltip("後退ブーストのパーティクル")]
    [SerializeField]
    protected ParticleSystem[] _backBoosterParticles = default;
    [Tooltip("左ブーストのパーティクル")]
    [SerializeField]
    protected ParticleSystem[] _leftBoosterParticles = default;
    [Tooltip("右ブーストのパーティクル")]
    [SerializeField]
    protected ParticleSystem[] _rightBoosterParticles = default;
    [Tooltip("耐久値ゲージ")]
    [SerializeField]
    protected GaugeControl _gauge = default;

    protected Color _startColor = Color.green;
    protected bool _damageColor;
    /// <summary> 現在のパーツ耐久値 </summary>
    protected int _currentPartsHp;

    /// <summary> パーツ耐久値 </summary>
    public int MaxPartsHP { get => _partsData.MaxPartsHp[_partsID]; }
    /// <summary> パーツ装甲値 </summary>
    public int Defense { get => _partsData.Defense[_partsID]; }
    /// <summary> 現在のパーツ耐久値 </summary>
    public int CurrentPartsHp { get => _currentPartsHp; }
    void Start()
    {
        StartSet();
    }
    /// <summary>
    /// パーツの初期化処理
    /// </summary>
    protected virtual void StartSet()
    {
        if (_damageSmoke)
        {
            _damageSmoke.SetActive(false);
        }
        _currentPartsHp = MaxPartsHP;
        if (_gauge != null)
        {
            _gauge.SetMaxValue(MaxPartsHP);
        }
    }
    /// <summary>
    /// パーツの色設定
    /// </summary>
    /// <param name="color"></param>
    public virtual void PartsColorChange(Color color)
    {
        foreach (var renderer in m_amors)
        {
            if (renderer != null)
            {
                renderer.material.color = color;
            }
        }
        _startColor = color;
    }
    /// <summary>
    /// 一時的な色変更
    /// </summary>
    /// <param name="color"></param>
    public virtual void ColorChange(Color color)
    {
        foreach (var renderer in m_amors)
        {
            renderer.material.color = color;
        }
    }

    public virtual void AddlyDamage(int power)
    {
        if (_currentPartsHp <= 0)
        {
            return;
        }
        if (power == 0)
        {
            return;
        }
        int damage = power;
        _currentPartsHp -= damage;
        if (_currentPartsHp < MaxPartsHP / 3)
        {
            if (_damageSmoke)
            {
                _damageSmoke.SetActive(true);
            }
        }
        if (_currentPartsHp <= 0)
        {
            _currentPartsHp = 0;
            Break = true;
            PartsBreak();
        }
        if (_gauge != null)
        {
            _gauge.CurrentValue = _currentPartsHp;
        }
    }
    /// <summary>
    /// ダメージの演出を行う
    /// </summary>
    public virtual void DamageEffect()
    {
        if (!_damageColor)
        {
            _damageColor = true;
            StartCoroutine(DamageColor());
        }
    }
    /// <summary>
    /// パーツ破壊時の処理
    /// </summary>
    protected virtual void PartsBreak()
    {
        foreach (var item in _partsObject)
        {
            item.SetActive(false);
        }
    }
    /// <summary>
    /// 被ダメージ時の色変化
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator DamageColor()
    {
        ColorChange(Color.white);
        yield return new WaitForSeconds(0.05f);
        ColorChange(Color.black);
        yield return new WaitForSeconds(0.05f);
        ColorChange(Color.white);
        yield return new WaitForSeconds(0.05f);
        ColorChange(_startColor);
        _damageColor = false;
    }
    public void SetGauge(GaugeControl gauge)
    {
        _gauge = gauge;
    }
    public void StartBooster()
    {
        foreach (var booster in _mainBoosterParticles)
        {
            booster.Play();
        }
        foreach (var booster in _subBoosterParticles)
        {
            booster.Play();
        }
    }
    public void StartBoosterF()
    {
        foreach (var booster in _frontBoosterParticles)
        {
            booster.Play();
        }
    }
    public void StartBoosterB()
    {
        foreach (var booster in _backBoosterParticles)
        {
            booster.Play();
        }
    }
    public void StartBoosterL()
    {
        foreach (var booster in _leftBoosterParticles)
        {
            booster.Play();
        }
    }
    public void StartBoosterR()
    {
        foreach (var booster in _rightBoosterParticles)
        {
            booster.Play();
        }
    }
    public void StopBooster()
    {
        foreach (var booster in _mainBoosterParticles)
        {
            booster.Stop();
        }
    }
}
