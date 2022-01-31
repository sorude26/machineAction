using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleShotWeapon : ShotWeapon
{
    [SerializeField]
    Transform _base = default;
    [SerializeField]
    float _moveSpeed = 1f;
    [SerializeField]
    float _startAngle = 0;
    protected override IEnumerator BulletShot()
    {
        float timer = 0;
        float angleX = _startAngle;
        while (angleX > -_angle)
        {
            angleX -= _moveSpeed * Time.deltaTime;
            if (angleX < -_angle)
            {
                angleX = -_angle;
            }
            _base.localRotation = Quaternion.Euler(angleX, 0, 0);
            yield return null;
        } while (timer < _shotInterval)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        timer = _shotInterval;
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
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        while (angleX < _startAngle)
        {
            angleX += _moveSpeed * Time.deltaTime;
            if (angleX > _startAngle)
            {
                angleX = _startAngle;
            }
            _base.localRotation = Quaternion.Euler(angleX, 0, 0);
            yield return null;
        }
        _base.localRotation = Quaternion.Euler(_startAngle, 0, 0);
        ShotNow = false;
    }
}
