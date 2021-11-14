using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageControl : MonoBehaviour, IDamageApplicable
{
    [SerializeField]
    GameObject m_body = default;
    [SerializeField]
    Transform m_center = default;
    [SerializeField]
    int m_hp = 5;
    public int CurrentHP { get => m_hp; }

    public void AddlyDamage(int damage)
    {
        m_hp -= damage;
        if (m_hp <= 0)
        {
            Dead();
        }
    }
    void Dead()
    {
        if (m_center == null)
        {
            m_center = transform;
        }
        EffectPool.Get(EffectType.ExplosionMachine, m_center.position);
        CameraController.Shake();
        m_body.SetActive(false);
    }
}
