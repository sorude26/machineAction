using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    float _fightRange = 5f;
    [SerializeField]
    CameraController _cameraControl = default;
    [SerializeField]
    DamageControl _damageControl = default;
    bool _set = false;
    float timer = 0;
    private async void Start()
    {
        await Task.Delay(1);
        StartSet();
    }
    public void SetBuild(UnitBuildData buildData)
    {
        _buildData = buildData;
    }
    public void StartSet()
    {
        Quaternion start = transform.rotation;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        _buildControl.SetData(_buildData);
        _controller.StartSet();
        _controller.MachineParts.ChangeColor(_color);
        _controller.MachineParts.Body.SetGauge(_gauge);
        _controller.OnBreak += BreakBody;
        transform.rotation = start;
        _damageControl.SetTarget();
        timer = Random.Range(1, 7);
        _controller.SetTarget(BattleManager.Instance.PlayerPos);
        _set = true;
    }

    void Update()
    {       
        if (!_set)
        {
            return;
        }
        Vector3 dir = _controller.MachineParts.Body.transform.forward - BattleManager.Instance.PlayerPos.position;
        _controller.Move(-dir.normalized);
        if (Vector3.Distance(_controller.MachineParts.Body.transform.position, BattleManager.Instance.PlayerPos.position) < _attackRange)
        {
            if (_controller.LAWeapon.Type == WeaponType.Rifle)
            {
                _controller.BodyControl.HandAttackLeft();
            }
            if (_controller.RAWeapon.Type == WeaponType.Rifle)
            {
                _controller.BodyControl.HandAttackRight();
            }
            _controller.BodyControl.BodyWeaponShot();
            if (Vector3.Distance(_controller.MachineParts.Body.transform.position, BattleManager.Instance.PlayerPos.position) < _fightRange)
            {
                _controller.BodyControl.FightingAttack();
            }
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = Random.Range(3, 9);
            _controller.Jump();
        }
    }
    void BreakBody()
    {
        _set = false;
        if (_damageControl)
        {
            _damageControl.ReMoveThis();
        }
    }
}
