using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotWeapon : WeaponMaster
{
    [SerializeField]
    protected BulletType _bullet = default;
    [SerializeField]
    protected Transform _muzzle = default;
    [SerializeField]
    protected ParticleSystem[] _particle = default;
    [SerializeField]
    protected float _power = 20f;
    [SerializeField]
    protected int _triggerShotCount = 1;
    [SerializeField]
    protected float _triggerInterval = 0.1f;
    [SerializeField]
    protected float _shotInterval = 0.2f;
    [SerializeField]
    protected float _diffusivity = 0.01f;
    [SerializeField]
    protected int _diffusionShot = 0;
    [SerializeField]
    protected float _parabola = 0;
    [SerializeField]
    protected float _angle = 0;

    protected float _triggerTimer = 0;
    protected bool _trigger = false;
    protected int _shotCount = 0;
    protected Vector3 _target = default;
    public bool ShotNow { get; protected set; }

    public virtual void Shot()
    {
        if (_particle.Length > 0)
        {
            foreach (var particle in _particle)
            {
                particle.Play();
            }
        }
        if (_parabola > 0)
        {
            if (_diffusivity > 0)
            {
                Vector3 target = _target;
                target.x += Random.Range(-_diffusivity, _diffusivity);
                target.y += Random.Range(-_diffusivity, _diffusivity);
                target.z += Random.Range(-_diffusivity, _diffusivity);
                ParabolaShot.ShootFixedTime(target, _parabola, _bullet, _muzzle.position);
            }
            else
            {
                ParabolaShot.ShootFixedTime(_target, _parabola, _bullet, _muzzle.position);
            }
            return;
        }
        if (_angle > 0)
        {
            if (_diffusivity > 0)
            {
                Vector3 target = _target;
                target.x += Random.Range(-_diffusivity, _diffusivity);
                target.y += Random.Range(-_diffusivity, _diffusivity);
                target.z += Random.Range(-_diffusivity, _diffusivity);
                if(!ParabolaShot.ShootFixedAngle(target, _angle, _muzzle.position, _bullet))
                {
                    BulletStartShot();
                }
            }
            else
            {
                if (!ParabolaShot.ShootFixedAngle(_target, _angle, _muzzle.position, _bullet))
                {
                    BulletStartShot();
                }
            }
            return;
        }
        if (_diffusionShot > 0)
        {
            DiffusionShot();
        }
        BulletStartShot();
    }
    protected void BulletStartShot()
    {
        Vector3 moveDir = _muzzle.forward.normalized;
        if (_diffusivity > 0)
        {
            moveDir.x += Random.Range(-_diffusivity, _diffusivity);
            moveDir.y += Random.Range(-_diffusivity, _diffusivity);
            moveDir.z += Random.Range(-_diffusivity, _diffusivity);
        }
        var shot = BulletPool.Get(_bullet, _muzzle.position);
        if (shot)
        {
            shot.StartShot(moveDir.normalized, _power);
        }
    }
    protected void DiffusionShot()
    {
        for (int i = 1; i < _diffusionShot; i++)
        {
            Vector3 moveDir = _muzzle.forward.normalized;
            if (_diffusivity > 0)
            {
                moveDir.x += Random.Range(-_diffusivity, _diffusivity);
                moveDir.y += Random.Range(-_diffusivity, _diffusivity);
                moveDir.z += Random.Range(-_diffusivity, _diffusivity);
            }
            var shot = BulletPool.Get(_bullet, _muzzle.position);
            if (shot)
            {
                shot.StartShot(moveDir.normalized, _power);
            }
        }
    }
    public virtual void StartShot(Vector3 target)
    {
        _target = target;
        if (_triggerTimer > 0 || !gameObject.activeInHierarchy)
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
    protected virtual IEnumerator BulletShot()
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
    protected IEnumerator TriggerTimer()
    {
        while (_triggerTimer > 0)
        {
            _triggerTimer -= Time.deltaTime;
            yield return null;
        }
        _triggerTimer = 0;
        _trigger = false;
    }

    public override void AttackAction(Vector3 target)
    {
        StartShot(target);
    }
    public override float AttackSpeed()
    {
        return _power;
    }
}
