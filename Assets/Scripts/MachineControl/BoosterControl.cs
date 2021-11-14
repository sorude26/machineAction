using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterControl : MonoBehaviour
{
    [SerializeField]
    ParticleSystem[] m_boost = default;
    [SerializeField]
    ParticleSystem[] m_boostL = default;
    [SerializeField]
    ParticleSystem[] m_boostR = default;
    bool m_booster = false;
    private void Start()
    {
        foreach (var item in m_boost)
        {
            item.Stop();
        }
    }
    public void Boost()
    {
        foreach (var item in m_boost)
        {
            item.Play();
        }
    }
    public void BoostL()
    {
        foreach (var item in m_boostL)
        {
            item.Play();
        }
    }
    public void BoostR()
    {
        foreach (var item in m_boostR)
        {
            item.Play();
        }
    }
    public void BoostEnd()
    {
        foreach (var item in m_boost)
        {
            item.Stop();
        }
    }
}
