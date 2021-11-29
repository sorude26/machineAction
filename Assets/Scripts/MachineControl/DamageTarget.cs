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
        _unitParts = _parent.GetComponent<IUnitParts>();
        if (_unitParts == null)
        {
            Destroy(this);
        }
    }
    public void AddlyDamage(int damage)
    {
        _unitParts.AddlyDamage(damage);
    }
}
