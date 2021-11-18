using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotWeapon : WeaponMaster
{
    [SerializeField]
    BulletType _bullet = default;
    [SerializeField]
    Transform _muzzle = default;
    [SerializeField]
    ParticleSystem[] _particle = default;
    [SerializeField]
    float _power = 20f;
    [SerializeField]
    int _triggerShotCount = 1;
    [SerializeField]
    float _triggerInterval = 0f;
    float _triggerTimer = 0;
    bool _trigger = false;
    [SerializeField]
    float _shotInterval = 0.2f;
    int _shotCount = 0;
    [SerializeField]
    protected float _diffusivity = 0.01f;
    public bool ShotNow { get; private set; }

    public void Shot()
    {
        if (_particle.Length > 0)
        {
            foreach (var particle in _particle)
            {
                particle.Play();
            }
        }
        if (_diffusivity > 0.09f)
        {
            DiffusionShot();
        }
        Vector3 moveDir = transform.forward;
        moveDir.x += Random.Range(-_diffusivity, _diffusivity);
        moveDir.y += Random.Range(-_diffusivity, _diffusivity);
        moveDir.z += Random.Range(-_diffusivity, _diffusivity);
        var shot = BulletPool.Get(_bullet, _muzzle.position);
        if (shot)
        {
            shot.ShotRb.AddForce(moveDir * _power, ForceMode.Impulse);
        }
        CameraController.HitShake();
    }
    void DiffusionShot()
    {
        for (int i = 1; i < _shotCount; i++)
        {
            Vector3 moveDir = transform.forward;
            moveDir.x += Random.Range(-_diffusivity, _diffusivity);
            moveDir.y += Random.Range(-_diffusivity, _diffusivity);
            moveDir.z += Random.Range(-_diffusivity, _diffusivity);
            var shot = BulletPool.Get(_bullet, _muzzle.position);
            if (shot)
            {
                shot.ShotRb.AddForce(moveDir * _power, ForceMode.Impulse);
            }
        }
        _shotCount = 0;
    }
    public void StartShot()
    {
        if (_triggerTimer > 0)
        {
            return;
        }
        if (_triggerInterval > 0)
        {
            if (!_trigger)
            {
                _trigger = true;
                _triggerTimer = _triggerInterval;
                _shotCount = _triggerShotCount;
                StartCoroutine(TriggerTimer());
            }
        }
        else
        {
            _shotCount = _triggerShotCount;
        }
        if (!ShotNow)
        {
            ShotNow = true;
            StartCoroutine(BulletShot());
        }
    }
    IEnumerator BulletShot()
    {
        float timer = _shotInterval;
        while (_shotCount > 0)
        {
            timer += Time.deltaTime;           
            if (timer >= _shotInterval)
            {
                Shot();
                _shotCount--;
                timer = 0;
            }
            yield return null;
        }
        ShotNow = false;
    }
    IEnumerator TriggerTimer()
    {
        while (_triggerTimer > 0)
        {
            _triggerTimer -= Time.deltaTime;
            yield return null;
        }
        _triggerTimer = 0;
        _trigger = false;
    }

    public override void AttackAction()
    {
        StartShot();
    }
}
