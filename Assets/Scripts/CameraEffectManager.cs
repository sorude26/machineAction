using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffectManager : MonoBehaviour
{
    static CameraEffectManager instance = default;
    [SerializeField]
    float _shakeLevel = 0.1f;
    [SerializeField]
    float _shakeRange = 300f;
    [SerializeField]
    float _shakePower = 5f;
    [SerializeField]
    float _shakeTime = 2f;
    [SerializeField]
    float _lightShakeRange = 50f;
    [SerializeField]
    float _lightShakePower = 1f;
    [SerializeField]
    float _lightShakeTime = 1f;
    [SerializeField]
    ShakeControl _cameraShakeControl = default;
    private void Awake()
    {
        instance = this;
    }
    public static void Shake(Vector3 pos)
    {
        var range = (BattleManager.Instance.PlayerPos.position - pos).magnitude / 2f;
        if (range < instance._shakeRange)
        {
            instance._cameraShakeControl.StartShake(instance._shakePower / range, instance._shakeTime, instance._shakeLevel);
        }
    }
    public static void LightShake(Vector3 pos)
    {
        var range = (BattleManager.Instance.PlayerPos.position - pos).magnitude;
        if (range < instance._lightShakeRange)
        {
            instance._cameraShakeControl.StartShake(instance._lightShakePower / range, instance._lightShakeTime, instance._shakeLevel);
        }
    }
    public static void SmallShake(Vector3 pos)
    {
        var range = (BattleManager.Instance.PlayerPos.position - pos).magnitude * 2f;
        if (range < instance._lightShakeRange)
        {
            instance._cameraShakeControl.StartShake(instance._lightShakePower / range, instance._lightShakeTime / 2f, instance._shakeLevel / 2f);
        }
    }
    public static void ExplosionShake(Vector3 pos,float time)
    {
        var range = (BattleManager.Instance.PlayerPos.position - pos).magnitude / 2f;
        if (range < instance._shakeRange)
        {
            instance._cameraShakeControl.StartShake(instance._shakePower / range, instance._shakeTime + time, instance._shakeLevel);
        }
    }
}
