using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStayTrigger : MonoBehaviour
{
    [SerializeField]
    float _hitInterval = 0.1f;
    [SerializeField]
    AttackTriger _triger = default;
    float _timer = 0;
    bool _active = false;
    private void Start()
    {
        if (_triger == null)
        {
            Destroy(this);
            return;
        }
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _hitInterval)
        {
            _timer = 0;
            if (_active)
            {
                _active = false;
                _triger.gameObject.SetActive(false);
            }
            else
            {
                _active = true;
                _triger.gameObject.SetActive(true);
            }
        }
    }
}
