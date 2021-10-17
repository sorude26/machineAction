using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotWeapon : MonoBehaviour
{
    [SerializeField]
    AttackPos m_attackPos = AttackPos.Shot;
    [SerializeField]
    Shot m_bullet = default;
    [SerializeField]
    Transform m_muzzle = default;
    [SerializeField]
    ParticleSystem[] m_particle = default;
    [SerializeField]
    float m_power = 20f;
    [SerializeField]
    int m_triggerShotCount = 1;
    [SerializeField]
    float m_triggerInterval = 0f;
    float m_triggerTimer = 0;
    bool m_trigger = false;
    [SerializeField]
    float m_shotInterval = 0.2f;
    int m_shotCount = 0;
    public bool ShotNow { get; private set; }
    enum AttackPos
    {
        Shot,
        ShotL,
        ShotR,
        Attack,
    }
    private void Start()
    {
        switch (m_attackPos)
        {
            case AttackPos.Shot:
                GameScene.InputManager.Instance.OnFirstInputShot += StartShot;
                break;
            case AttackPos.ShotL:
                GameScene.InputManager.Instance.OnFirstInputShotL += StartShot;
                break;
            case AttackPos.ShotR:
                GameScene.InputManager.Instance.OnFirstInputShotR += StartShot;
                break;
            case AttackPos.Attack:
                GameScene.InputManager.Instance.OnFirstInputAttack += StartShot;
                break;
            default:
                break;
        }
    }
    public void Shot()
    {
        if (m_particle.Length > 0)
        {
            foreach (var particle in m_particle)
            {
                particle.Play();
            }
        }
        var shot = Instantiate(m_bullet);
        shot.transform.position = m_muzzle.position;
        shot.ShotRb.AddForce(transform.forward * m_power, ForceMode.Impulse);
    }
    public void StartShot()
    {
        if (m_triggerTimer > 0)
        {
            return;
        }
        if (m_triggerInterval > 0)
        {
            if (!m_trigger)
            {
                m_trigger = true;
                m_triggerTimer = m_triggerInterval;
                m_shotCount = m_triggerShotCount;
                StartCoroutine(TriggerTimer());
            }
        }
        else
        {
            m_shotCount = m_triggerShotCount;
        }
        if (!ShotNow)
        {
            ShotNow = true;
            StartCoroutine(BulletShot());
        }
    }
    IEnumerator BulletShot()
    {
        float timer = m_shotInterval;
        while (m_shotCount > 0)
        {
            timer += Time.deltaTime;           
            if (timer >= m_shotInterval)
            {
                Shot();
                m_shotCount--;
                timer = 0;
            }
            yield return null;
        }
        ShotNow = false;
    }
    IEnumerator TriggerTimer()
    {
        while (m_triggerTimer > 0)
        {
            m_triggerTimer -= Time.deltaTime;
            yield return null;
        }
        m_triggerTimer = 0;
        m_trigger = false;
    }
}
