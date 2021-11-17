using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    static CameraController instance = default;
    [SerializeField]
    Transform m_cameraTarget = default;
    [SerializeField]
    Transform m_machineBody = default;
    [SerializeField]
    ShakeControl m_cameraShakeControl = default;
    Quaternion m_cameraRot = default;
    float minX = -80f, maxX = 80f;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        GameScene.InputManager.Instance.OnInputAxisRawExit += ResetLock;
        GameScene.InputManager.Instance.OnInputCameraRaw += FreeLock;
        m_cameraRot = transform.localRotation;
    }
    private void FixedUpdate()
    {
        m_cameraTarget.localRotation = m_machineBody.localRotation;
    }
    void DefaultLock()
    {
        m_cameraRot = m_cameraTarget.rotation;
    }
    void FreeLock(Vector2 dir)
    {
        m_cameraRot = transform.localRotation;
        if (dir.x != 0)
        {
            m_cameraRot *= Quaternion.Euler(0, dir.x, 0);
        }
        m_cameraRot = ClampRotation(m_cameraRot);
        transform.localRotation = m_cameraRot;
    }
    void ResetLock()
    {
        m_cameraRot = m_cameraTarget.localRotation;
        transform.localRotation = m_cameraRot;
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
        instance.m_cameraShakeControl?.StartShake(1.2f, 0.8f);
    }
    public static void LightShake()
    {
        instance.m_cameraShakeControl?.StartShake(0.1f, 0.3f);
    }
    public static void HitShake()
    {
        instance.m_cameraShakeControl?.StartShake(0.07f, 0.2f);
    }
}
