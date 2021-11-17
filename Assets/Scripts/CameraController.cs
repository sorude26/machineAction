using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    static CameraController instance = default;
    [SerializeField]
    Transform _cameraTarget = default;
    [SerializeField]
    ShakeControl _cameraShakeControl = default;
    Quaternion _cameraRot = default;
    float minX = -80f, maxX = 80f;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        GameScene.InputManager.Instance.OnInputAxisRawExit += ResetLock;
        GameScene.InputManager.Instance.OnInputCameraRaw += FreeLock;
        _cameraRot = transform.localRotation;
    }
   
    void DefaultLock()
    {
        _cameraRot = _cameraTarget.rotation;
    }
    void FreeLock(Vector2 dir)
    {
        _cameraRot = transform.localRotation;
        if (dir.x != 0)
        {
            _cameraRot *= Quaternion.Euler(0, dir.x, 0);
        }
        _cameraRot = ClampRotation(_cameraRot);
        transform.localRotation = _cameraRot;
    }
    void ResetLock()
    {
        _cameraRot = _cameraTarget.localRotation;
        transform.localRotation = _cameraRot;
    }
    Quaternion ClampRotation(Quaternion angle)
    {
        angle.x /= angle.w;
        angle.y /= angle.w;
        angle.z /= angle.w;
        angle.w = 1f;
        float angleX = Mathf.Atan(angle.y) * Mathf.Rad2Deg * 2f;
        angleX = Mathf.Clamp(angleX, minX, maxX);
        angle.y = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);
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
