using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Shot : MonoBehaviour
{
    [SerializeField]
    EffectType _hitEffect = default;
    [SerializeField]
    EffectType _endEffect = default;
    [SerializeField]
    Rigidbody _rb = default;
    [SerializeField]
    int _power = 1;
    [SerializeField]
    float _lifeTime = 3f;

    bool _hit = false;
    public int Power { get => _power; }
    public void StartShot(Vector3 dir)
    {
        StartShot(dir, _power, 1f);
    }
    public void StartShot(Vector3 dir, float speed)
    {
        StartShot(dir, _power, speed);
    }
    public void StartShot(Vector3 dir, int power, float speed)
    {
        _hit = false;
        StartCoroutine(ShotMove());
        transform.forward = dir.normalized;
        _power = power;
        _rb.AddForce(dir * speed, ForceMode.Impulse);
    }
    public void ShotHit()
    {
        _hit = true;
        _rb.velocity = Vector3.zero;
        PlayEffect(_hitEffect);
    }
    IEnumerator ShotMove()
    {
        float timer = 0;
        while (timer < _lifeTime && !_hit)
        {
            timer += Time.deltaTime;
            if (timer >= _lifeTime)
            {
                PlayEffect(_endEffect);
            }
            yield return null;
        }
        _rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
        _hit = false;
    }
    void PlayEffect(EffectType effectType)
    {
        var effect = EffectPool.Get(effectType, transform.position);
        if (effect)
        {
            effect.Particle.Play();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_hit)
        {
            return;
        }
        if (other.tag == "Ground")
        {
            ShotHit();
        }
        else
        {
            other.TryGetComponent<IDamageApplicable>(out var target);
            if (target != null)
            {
                target.AddlyDamage(Power);
                ShotHit();
            }
        }
    }
}
