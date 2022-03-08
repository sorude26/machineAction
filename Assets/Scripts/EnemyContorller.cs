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
    float _longAttackRange = 200f;
    [SerializeField]
    float _fightRange = 5f;
    [SerializeField]
    float _noneAttackRange = 0f;
    [SerializeField]
    bool _jump = true;
    [SerializeField]
    Vector2 _jumpRange = new Vector2(3, 9);
    [SerializeField]
    DamageControl _damageControl = default;
    [SerializeField]
    float _startWaitTime = 2f;
    [SerializeField]
    Transform _target = default;
    bool _set = false;
    float _timer = 0;
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
        _timer = Random.Range(1, 7);
        if (_target == null)
        {
            _target = BattleManager.Instance.PlayerPos;
        }
        _controller.SetTarget(_target);
        StartCoroutine(StartContorlWait());
    }
    IEnumerator StartContorlWait()
    {
        float timer = 0;
        while (timer < _startWaitTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        _set = true;
    }
    void Update()
    {       
        if (!_set)
        {
            return;
        }
        Vector3 dir = _controller.MachineParts.Body.transform.forward - _target.position;
        _controller.Move(-dir.normalized);
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            _timer = Random.Range(_jumpRange.x, _jumpRange.y);
            if (_jump)
            {
                _controller.Jump();
            }
        }
        float range = Vector3.Distance(_controller.MachineParts.Body.transform.position, _target.position);
        if (range < _noneAttackRange)
        {
            _controller.BodyControl.BodyResetAngle();
            return;
        }
        if (range < _attackRange)
        {
            if (range > _longAttackRange)
            {
                _controller.BodyControl.ShoulderShot();
            }
            if (_controller.LAWeapon.Type == WeaponType.Rifle)
            {
                _controller.BodyControl.HandAttackLeft();
            }
            if (_controller.RAWeapon.Type == WeaponType.Rifle)
            {
                _controller.BodyControl.HandAttackRight();
            }
            _controller.BodyControl.BodyWeaponShot();
            if (range < _fightRange)
            {
                _controller.BodyControl.FightingAttack();
            }
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
