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
    public Rigidbody ShotRb { get => m_rb; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            var effect = EffectPool.Get(m_effect, transform.position);
            if (effect)
            {
                effect.Particle.Play();
            }
            m_rb.velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}
