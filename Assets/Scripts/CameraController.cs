using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    static CameraController instance = default;
    [SerializeField]
    Transform _cameraTarget = default;
    [SerializeField]
    BodyControl _body = default;
    [SerializeField]
    ShakeControl _cameraShakeControl = default;
    [SerializeField]
    float _followSpeed = 5f;
    [SerializeField]
    float _lockSpeed = 20f;
    Quaternion _cameraRot = default;
    float _minY = -70f;
    float _maxY = 20f;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        GameScene.InputManager.Instance.OnInputCameraRawExit += ResetLock;
        GameScene.InputManager.Instance.OnInputCameraRaw += FreeLock;
        _cameraRot = transform.localRotation;
    }
    private void Update()
    {
        _cameraRot = _cameraTarget.localRotation;
        transform.localRotation = Quaternion.Lerp(transform.localRotation, _cameraRot, _followSpeed * Time.deltaTime);
    }
    void DefaultLock()
    {
        _cameraRot = _cameraTarget.rotation;
    }
    void FreeLock(Vector2 dir)
    {
        _cameraRot = _cameraTarget.localRotation;
        if (Mathf.Abs(dir.x) > 0.1f)
        {
            _cameraRot *= Quaternion.Euler(0, dir.x * _lockSpeed, 0);
        }        
        _cameraRot = ClampRotation(_cameraRot);
        _body.SetBodyRotaion(_cameraRot);
    }
    void ResetLock()
    {
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
    public static void Shake()
    {
        instance._cameraShakeControl?.StartShake(1.2f, 0.8f);
    }
    public static void LightShake()
    {
        instance._cameraShakeControl?.StartShake(0.1f, 0.3f);
    }
    public static void HitShake()
    {
        instance._cameraShakeControl?.StartShake(0.07f, 0.2f);
    }
}
