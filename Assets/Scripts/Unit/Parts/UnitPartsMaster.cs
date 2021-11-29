using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 機体パーツの基底クラス
/// </summary>
/// <typeparam name="T">対応するパーツのデータ</typeparam>
public abstract class UnitPartsMaster<T> : PartsMaster<T>, IUnitParts where T :UnitPartsData
{
    /// <summary> パーツ耐久値 </summary>
    public int MaxPartsHP { get => _partsData.MaxPartsHp[_partsID]; }
    /// <summary> パーツ装甲値 </summary>
    public int Defense { get => _partsData.Defense[_partsID]; }
    /// <summary> 現在のパーツ耐久値 </summary>
    protected int m_currentPartsHp;
    /// <summary> 現在のパーツ耐久値 </summary>
    public int CurrentPartsHp { get => m_currentPartsHp; }
    /// <summary> 表示用パーツ耐久値 </summary>
    public int ViewCurrentHp { get; protected set; }
    /// <summary> ダメージを受けた回数 </summary>
    protected int m_damageCount = 0;
    /// <summary> 受けたダメージ </summary>
    protected List<int> m_partsDamage;
    [Tooltip("攻撃命中の表示箇所")]
    [SerializeField] protected Transform[] m_hitPos;
    [Tooltip(" 耐久値半分以下で表示する煙 ")]
    [SerializeField] protected GameObject m_damageSmoke;
    [Tooltip("色が変更可能な装甲")]
    [SerializeField] protected Renderer[] m_amors;
    protected Color m_startColor = Color.green;
    protected bool m_damageColor;
    void Start()
    {
        StartSet();
    }
    /// <summary>
    /// パーツの初期化処理
    /// </summary>
    protected virtual void StartSet()
    {
        m_damageSmoke?.SetActive(false);
        m_currentPartsHp = MaxPartsHP;
        ViewCurrentHp = MaxPartsHP;
        m_partsDamage = new List<int>();
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
        m_startColor = color;
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
        if (m_currentPartsHp <= 0)
        {
            return;
        }
        if (power == 0)
        {
            return;
        }
        int damage = power;
        m_currentPartsHp -= damage;
        if (m_currentPartsHp < MaxPartsHP / 3)
        {
            m_damageSmoke.SetActive(true);
        }
        if (m_currentPartsHp <= 0)
        {
            m_currentPartsHp = 0;
            Break = true; 
            PartsBreak();
        }
    }
    /// <summary>
    /// ダメージの演出を行う
    /// </summary>
    public virtual void DamageEffect()
    {
        int damage = m_partsDamage[m_damageCount];
        m_damageCount++;
        if (m_damageCount >= m_partsDamage.Count)
        {
            if (m_currentPartsHp <= 0)
            {
                PartsBreak();
            }
            m_damageCount = 0;
            m_partsDamage.Clear();
        }
        if (damage >= 0)
        {
            int r = Random.Range(0, m_hitPos.Length);
            ViewCurrentHp -= damage;
            if (ViewCurrentHp < MaxPartsHP / 3)
            {
                m_damageSmoke.SetActive(true);
            }
            if (!m_damageColor)
            {
                m_damageColor = true;
                StartCoroutine(DamageColor());
            }
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
        ColorChange(m_startColor);
        m_damageColor = false;
    }
}
