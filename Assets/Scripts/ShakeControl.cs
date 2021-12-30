using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeControl : MonoBehaviour
{
    [SerializeField]
    float m_startShakeRange = 0.5f;
    float m_shakeRange = 0.5f;

    float m_timer = 0;
    bool m_shake = default;
    Vector3 m_startPos = default;
    private void Start()
    {
        m_startPos = transform.localPosition;
    }
    public void StartShake(float time)
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        if (m_timer < time)
        {
            m_timer = time;
        }
        if (m_shakeRange < m_startShakeRange)
        {
            m_shakeRange = m_startShakeRange;
        }
        if (!m_shake)
        {
            m_shake = true;
            StartCoroutine(Shake());
        }
    }
    public void StartShake(float time, float power)
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        if (m_timer < time)
        {
            m_timer = time;
        }
        if (m_shakeRange < power)
        {
            m_shakeRange = power;
        }
        if (!m_shake)
        {
            m_shake = true;
            m_shakeRange = power;
            m_timer = time;
            StartCoroutine(Shake());
        }
    }

    private IEnumerator Shake()
    {
        while (m_timer > 0 && m_shakeRange > 0)
        {
            m_timer -= Time.deltaTime;
            Vector3 v = m_startPos;
            v.x = Random.Range(-m_shakeRange, m_shakeRange);
            v.y = Random.Range(-m_shakeRange, m_shakeRange);
            m_shakeRange -= Time.deltaTime * 0.5f;
            transform.localPosition = v;
            yield return null;
        }
        transform.localPosition = m_startPos; 
        m_timer = 0;
        m_shakeRange = 0;
        m_shake = false;
    }
}
