using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectControl : MonoBehaviour
{
    [SerializeField]
    ParticleSystem m_particleSystem = default;
    public ParticleSystem Particle { get => m_particleSystem; }
}
