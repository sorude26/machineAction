using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffectManager : MonoBehaviour
{
    static CameraEffectManager instance = default;
    [SerializeField]
    float _shakeRange = 300f;
    [SerializeField]
    float _shakePower = 5f;
    [SerializeField]
    float _lightShakeRange = 50f;
    [SerializeField]
    float _lightShakePower = 1f;
    [SerializeField]
    ShakeControl _cameraShakeControl = default;
    private void Awake()
    {
        instance = this;
    }
    public static void Shake(Vector3 pos)
    {
        var range = (BattleManager.Instance.PlayerPos.position - pos).magnitude;
        if (range < instance._shakeRange)
        {
            instance._cameraShakeControl.StartShake(instance._shakePower / range, 1.8f);
        }
    }
    public static void LightShake(Vector3 pos)
    {
        var range = (BattleManager.Instance.PlayerPos.position - pos).magnitude;
        if (range < instance._lightShakeRange)
        {
            instance._cameraShakeControl.StartShake(instance._lightShakePower / range, 0.8f);
        }
    }
}
