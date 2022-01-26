using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContorller : MonoBehaviour
{
    [SerializeField]
    MachineController _controller = default;
    [SerializeField]
    MachineBuildControl _buildControl = default;
    [SerializeField]
    GaugeControl _gauge = default;
    [SerializeField]
    UnitBuildData _buildData = default;
    [SerializeField]
    Color _color = default;
    [SerializeField]
    float _attackRange = 100f;
    [SerializeField]
    CameraController _cameraControl = default;
    [SerializeField]
    DamageControl _damageControl = default;
    bool _set = false;
    float timer = 0;
    void Start()
    {
        Quaternion start = transform.rotation;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        _buildControl.SetData(_buildData);
        _controller.StartSet();
        _controller.MachineParts.ChangeColor(_color);
        _controller.MachineParts.Body.SetGauge(_gauge);
        _controller.OnBreak += BreakBody;
        _controller.transform.rotation = start;
        timer = Random.Range(1, 7);
    }

    void Update()
    {       
        if (!_set)
        {
            _controller.SetTarget(BattleManager.Instance.PlayerPos);
            _set = true;
        }
        Vector3 dir = _controller.MachineParts.Body.transform.forward - BattleManager.Instance.PlayerPos.position;
        _controller.Move(dir.normalized);
        if (dir.magnitude < _attackRange)
        {
            _controller.BodyControl.HandAttackLeft();
            _controller.BodyControl.HandAttackRight();
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = Random.Range(1, 7);
            _controller.Jump();
        }
    }
    void BreakBody()
    {
        if (_damageControl)
        {
            _damageControl.ReMoveThis();
        }
    }
}
