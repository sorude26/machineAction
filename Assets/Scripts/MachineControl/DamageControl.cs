using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageControl : MonoBehaviour, IDamageApplicable
{
    [SerializeField]
    GameObject _body = default;
    [SerializeField]
    Transform _center = default;
    [SerializeField]
    GaugeControl _gauge = default;
    [SerializeField]
    EffectType _deadEffect = EffectType.ExplosionMachine;
    [SerializeField]
    int _hp = 5;
    [SerializeField]
    bool _target = true;
    public int CurrentHP { get => _hp; }
    public Transform Center { get => _center; }
    private void Start()
    {
        if (_target)
        {
            BattleManager.Instance.AddTarget(this);
        }
        if (_gauge != null)
        {
            _gauge.SetMaxValue(_hp);
        }
    }

    public void AddlyDamage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Dead();
        }
        if (_gauge != null)
        {
            _gauge.CurrentValue = _hp;
        }
    }
    private void Dead()
    {
        if (_center == null)
        {
            _center = transform;
        }
        EffectPool.Get(_deadEffect, _center.position);
        CameraController.Shake();
        if (_body == null)
        {
            gameObject.SetActive(false);
            return;
        }
        _body.SetActive(false);
        if (_target)
        {
            BattleManager.Instance.ReMoveTarget(this);
        }
    }
}
