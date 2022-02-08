using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeControl : MonoBehaviour
{
    [SerializeField]
    float _startShakeRange = 0.5f;
    float _shakeRange = 0.5f;
    float _shakeLevel = 0.1f;
    float _timer = 0;
    bool _shake = default;
    Vector3 _startPos = default;
    Vector3 _current = default;
    private void Start()
    {
        _startPos = transform.localPosition;
    }
    public void StartShake(float time)
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        if (_timer < time)
        {
            _timer = time;
        }
        if (_shakeRange < _startShakeRange)
        {
            _shakeRange = _startShakeRange;
        }
        if (!_shake)
        {
            _shake = true;
            StartCoroutine(Shake());
        }
    }
    public void StartShake(float time, float power,float level)
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        if (_timer < time)
        {
            _timer = time;
        }
        if (_shakeRange < power)
        {
            _shakeLevel = level;
            _shakeRange = power;
        }
        if (!_shake)
        {
            _shakeLevel = level;
            _shake = true;
            _shakeRange = power;
            _timer = time;
            StartCoroutine(Shake());
        }
    }
    private IEnumerator Shake()
    {
        float timer = 0;
        Vector3 v = _startPos;
        while (_timer > 0 && _shakeRange > 0)
        {
            timer += Time.deltaTime;
            if (timer > _shakeLevel)
            {
                timer = 0; 
                v = _startPos;
                v.x += Random.Range(-_shakeRange, _shakeRange);
                v.y += Random.Range(-_shakeRange, _shakeRange);
            }
            _timer -= Time.deltaTime;
            transform.localPosition = Vector3.Lerp(_startPos, v, timer * 10);
            _shakeRange -= Time.deltaTime * 0.5f;
            yield return null;
        }
        transform.localPosition = _startPos; 
        _timer = 0;
        _shakeRange = 0;
        _shake = false;
    }
}
