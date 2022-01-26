using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriger : MonoBehaviour
{
    [SerializeField]
    EffectType _effect = EffectType.ShotHit;
    [SerializeField]
    int _power = 1;
    public event Action OnHit = default;
    public event Action OnDamage = default;
    public int SetPower { set => _power = value; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            AttackHit();
        }
        else
        {
            other.TryGetComponent<IDamageApplicable>(out var target);
            if (target != null)
            {
                target.AddlyDamage(_power);
                OnDamage?.Invoke();
                AttackHit();
            }
        }
    }
    public void AttackHit()
    {
        var effect = EffectPool.Get(_effect, transform.position);
        if (effect)
        {
            effect.Particle.Play();
            if (_effect == EffectType.Explosion || _effect == EffectType.ExplosionMachine)
            {
                CameraEffectManager.Shake(transform.position);
            }
        }
        OnHit?.Invoke();
    }
}
