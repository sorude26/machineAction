using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Shot : MonoBehaviour
{
    [SerializeField]
    EffectType m_effect = default;
    [SerializeField]
    Rigidbody m_rb = default;
    [SerializeField]
    int m_power = 1;
    public Rigidbody ShotRb { get => m_rb; }
    public int Power { get => m_power; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            ShotHit();
        }
        else
        {
            var target = other.GetComponent<IDamageApplicable>();
            if (target != null)
            {
                target.AddlyDamage(Power);
                ShotHit();
            }
        }
    }
    public void ShotHit()
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
        m_rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
