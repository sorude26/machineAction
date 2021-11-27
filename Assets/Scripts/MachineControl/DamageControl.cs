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
    int _hp = 5;
    public int CurrentHP { get => _hp; }
    public Transform Center { get => _center; }
    private void Start()
    {
        BattleManager.Instance.AddTarget(this);
    }

    public void AddlyDamage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Dead();
        }
    }
    private void Dead()
    {
        if (_center == null)
        {
            _center = transform;
        }
        EffectPool.Get(EffectType.ExplosionMachine, _center.position);
        CameraController.Shake();
        if (_body == null)
        {
            gameObject.SetActive(false);
            return;
        }
        _body.SetActive(false);
        BattleManager.Instance.ReMoveTarget(this);
    }
}
