using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriger : MonoBehaviour
{
    [SerializeField]
    EffectType m_effect = default;
    [SerializeField]
    int m_power = 1;
    public int Power { get => m_power; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            AttackHit();
        }
        else
        {
            var target = other.GetComponent<IDamageApplicable>();
            if (target != null)
            {
                target.AddlyDamage(Power);
                AttackHit();
            }
        }
    }
    public void AttackHit()
    {
        var effect = EffectPool.Get(m_effect, transform.position);
        if (effect)
        {
            effect.Particle.Play();
            if (m_effect == EffectType.Explosion || m_effect == EffectType.ExplosionMachine)
            {
                CameraController.Shake();
            }
            else
            {
                CameraController.HitShake();
            }
        }
    }
}
