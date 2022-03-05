using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTarget : MonoBehaviour, IDamageApplicable
{
    [SerializeField]
    GameObject _parent = default;
    IUnitParts _unitParts = default;
    private void Start()
    {
        if (_parent)
        {
            _parent.TryGetComponent<IUnitParts>(out _unitParts);
            if (_unitParts == null)
            {
                Destroy(this);
            }
        }
        else
        {
            Destroy(this);
        }
    }
    public void AddlyDamage(int damage)
    {
        _unitParts.AddlyDamage(damage);
    }
}
