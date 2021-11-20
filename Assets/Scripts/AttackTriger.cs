using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriger : MonoBehaviour
{
    [SerializeField]
    EffectType _effect = default;
    [SerializeField]
    int _power = 1;
    public int Power { get => _power; }
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
        var effect = EffectPool.Get(_effect, transform.position);
        if (effect)
        {
            effect.Particle.Play();
            if (_effect == EffectType.Explosion || _effect == EffectType.ExplosionMachine)
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
