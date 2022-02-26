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
    float _hitEffectRange = 1f;
    [SerializeField]
    ShakeControl _cameraShakeControl = default;
    [SerializeField]
    GameObject _cameraffect = default;
    bool _lock = false;
    private void Awake()
    {
        instance = this;
    }
    public static void LockChange()
    {
        if (instance._lock)
        {
            instance._lock = false;
            instance._cameraffect.SetActive(false);
        }
        else
        {
            instance._lock = true;
            instance._cameraffect.SetActive(true);
        }
    }
    public static void HitStop(Vector3 pos)
    {
        if (instance._lock)
        {
            return;
        }
        if (Vector3.Distance(BattleManager.Instance.PlayerPos.position, pos) > instance._hitEffectRange)
        {
            return;
        }
        GameScene.TimeManager.Instance.HitStop();
    }
    public static void Shake(Vector3 pos)
    {
        var range = (BattleManager.Instance.PlayerPos.position - pos).magnitude / 2f + 1f;
        if (range < instance._shakeRange)
        {
            instance._cameraShakeControl.StartShake(instance._shakeTime, instance._shakePower / range, instance._shakeLevel);
        }
    }
    public static void LightShake(Vector3 pos)
    {
        var range = (BattleManager.Instance.PlayerPos.position - pos).magnitude + 1f;
        if (range < instance._lightShakeRange)
        {
            instance._cameraShakeControl.StartShake(instance._lightShakeTime, instance._lightShakePower / range, instance._shakeLevel);
        }
    }
    public static void SmallShake(Vector3 pos)
    {
        var range = (BattleManager.Instance.PlayerPos.position - pos).magnitude * 2f + 1f;
        if (range < instance._lightShakeRange)
        {
            instance._cameraShakeControl.StartShake(instance._lightShakeTime / 2f, instance._lightShakePower / range, instance._shakeLevel / 2f);
        }
    }
    public static void ExplosionShake(Vector3 pos, float time)
    {
        var range = (BattleManager.Instance.PlayerPos.position - pos).magnitude / 3f + 1f;
        if (range < instance._shakeRange)
        {
            instance._cameraShakeControl.StartShake(instance._shakeTime + time, instance._shakePower * 2 / range, instance._shakeLevel);
        }
    }
}
