using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform _cameraTarget = default;
    [SerializeField]
    BodyControl _body = default;
    [SerializeField]
    float _followSpeed = 5f;
    [SerializeField]
    float _lockSpeed = 20f;
    [SerializeField]
    float _upSpeed = 1f;
    Quaternion _cameraRot = default;
    float _minY = -40f;
    float _maxY = 40f;
    float _angleY = 0;
    Vector3 _startCameraPos = default;
    void Start()
    {       
        _cameraRot = transform.localRotation;
        _startCameraPos = transform.localPosition;
    }
    private void Update()
    {
        _cameraRot = _cameraTarget.localRotation;
        _cameraRot.x = _angleY;
        transform.localRotation = Quaternion.Lerp(transform.localRotation, _cameraRot, _followSpeed * Time.deltaTime);
    }
    void DefaultLock()
    {
        _cameraRot = _cameraTarget.rotation;
    }
    public void FreeLock(Vector2 dir)
    {
        _cameraRot = _cameraTarget.localRotation;
        if (Mathf.Abs(dir.x) > 0.1f)
        {
            _cameraRot *= Quaternion.Euler(0, dir.x * _lockSpeed, 0);
        }
        if (Mathf.Abs(dir.y) > 0.5f)
        {
            _cameraRot *= Quaternion.Euler(dir.y * _upSpeed, 0, 0);
        }
        _cameraRot = ClampRotation(_cameraRot);
        _angleY = _cameraRot.x;
        _body.SetBodyRotaion(_cameraRot);
    }
    public void ResetLock()
    {
        _angleY = 0;
        _cameraRot = _cameraTarget.localRotation;
        _body.InputEnd();
    }
    Quaternion ClampRotation(Quaternion angle)
    {
        angle.x /= angle.w;
        angle.y /= angle.w;
        angle.z /= angle.w;
        angle.w = 1f;
        float angleY = Mathf.Atan(angle.y) * Mathf.Rad2Deg * 2f;
        angleY = Mathf.Clamp(angleY, -_body.CameraRange, _body.CameraRange);
        angle.y = Mathf.Tan(angleY * Mathf.Deg2Rad * 0.5f);
        float angleX = Mathf.Atan(angle.x) * Mathf.Rad2Deg * 2f;
        angleX = Mathf.Clamp(angleX, _minY, _maxY);
        angle.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);
        return angle;
    }
}
