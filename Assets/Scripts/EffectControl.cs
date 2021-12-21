using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectControl : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _particleSystem = default;
    public ParticleSystem Particle { get => _particleSystem; }
}
