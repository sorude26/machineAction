using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MachineBuildControl))]
public class MachineController : MonoBehaviour
{
    [SerializeField]
    MachineBuildControl _buildControl = default;
    [SerializeField]
    MachineParameter _parameter = default;
    [SerializeField]
    MoveControl _moveControl = default;
    [SerializeField]
    GroundCheck _groundCheck = default;
    [SerializeField]
    LegControl _leg = default;
    [SerializeField]
    BodyControl _body = default;
    [SerializeField]
    BoosterControl _booster = default;
    Rigidbody _rb = default;
    [SerializeField]
    TargetMark _mark = default;
    [SerializeField]
    Transform _bodyAngle = default;
    [SerializeField]
    Transform _front = default;
    bool _start = false;
    bool _jump = false;
    bool _jet = false;
    bool _knockDown = false;
    Vector3 _inputAxis = Vector3.zero;
    Quaternion _legRotaion = Quaternion.Euler(0, 0, 0);
    PartsManager _parts = default;
    Transform _target = default;
    Transform _legTransform = default;

    public event Action OnBreak = default;

    public bool FloatMode { get; private set; }
    public Vector3 InputAxis { get => _inputAxis; }
    public BodyControl BodyControl { get => _body; }
    public PartsManager MachineParts { get => _parts; }
    public WeaponMaster RAWeapon { get => _parts.RAWeapon; }
    public WeaponMaster LAWeapon { get => _parts.LAWeapon; }
    public WeaponMaster BWeapon { get => _parts.BodyWeapon; }
    public ShoulderWeapon SWeapon { get => _parts.ShoulderWeapon; }
    public Transform LookTarget { get; protected set; }
    public MachineParameter Parameter { get => _parameter; }
    public void StartSet()
    {
        _parts = new PartsManager();
        _buildControl.StartSet(_parts);
        _legTransform = _buildControl.LegBase;
        _parameter.SetParameter(_parts);
        _rb = GetComponent<Rigidbody>();
        _leg.Set(this);
        _leg.SetLandingTime(_parameter.LandingTime);
        _leg.ChangeSpeed(_parameter.ActionSpeed);
        _body.Set(this);
        _body.ChangeSpeed(_parameter.ActionSpeed);
        _body.BodyRSpeed = _parameter.BodyTurnSpeed;
        _body.BodyTurnRange = _parameter.BodyTurnRange;
        _body.CameraRange = _parameter.CameraTurnRange;
        _booster.Set(this);
        RAWeapon.OwnerRb = _rb;
        LAWeapon.OwnerRb = _rb;
        BWeapon.OwnerRb = _rb;
        SWeapon.OwnerRb = _rb;
        _parts.Body.OnBodyBreak += BodyBreak; 
        LookTarget = _front;
        _start = true;
    }
    private void Update()
    {
        if (!_start)
        {
            return;
        }
        _body.PartsMotion();
        _legTransform.localRotation = Quaternion.Lerp(_legTransform.localRotation, _legRotaion, _parameter.TurnSpeed * Time.deltaTime);
    }
    public void SetTarget()
    {
        if (_knockDown)
        {
            return;
        }
        if (_target)
        {
            LookTarget = _target;
        }
        else
        {
            var target = BattleManager.Instance.GetTarget(_parameter.LockOnRange);
            if (target)
            {
                LookTarget = target.Center;
            }
            else
            {
                LookTarget = _front;
            }
            _mark.SetTarget(target);
        }
    }
    public void SetTarget(Transform target)
    {
        _target = target;
    }
    public void Move(Vector3 dir)
    {
        Move(dir.x, dir.z);
    }
    public void Move(float horizonal, float vertical)
    {
        if (_knockDown)
        {
            return;
        }
        _inputAxis = new Vector3(horizonal, 0, vertical);
        if (!FloatMode)
        {
            var dir = _inputAxis;
            dir.x += _body.BodyAngle.y;
            if (_groundCheck.IsGrounded())
            {
                if (dir.z > InputStatus.MoveLimit)
                {
                    _leg.WalkStart(1);
                }
                else if (dir.z < -InputStatus.MoveLimit)
                {
                    _leg.WalkStart(-1);
                }
                if (dir.x > InputStatus.MoveLimit)
                {
                    _leg.TurnStartRight();
                }
                else if (dir.x < -InputStatus.MoveLimit)
                {
                    _leg.TurnStartLeft();
                }
            }
            else if (_jump && !_jet)
            {
                if (!_booster.BoostCheckFly())
                {
                    _moveControl.Jet(_rb, _body.BodyTransform.forward * _inputAxis.z + _body.BodyTransform.right * _inputAxis.x, _parameter.JetPower, _parameter.JetControlPower);
                }
                Turn(horizonal * MachineStatus.FlyTurnDelay);
                _body.ResetAngle(MachineStatus.FlyBodyReset);
            }
        }
        else
        {
            _moveControl.MoveFloat(_rb,  transform.forward * _inputAxis.z + transform.right * _inputAxis.x, _parameter.FloatSpeed, _parameter.MaxFloatSpeed);
        }
    }
    public void MoveEnd()
    {
        if (!FloatMode)
        {
            _leg.WalkStop();
            if (_groundCheck.IsGrounded())
            {
                ActionStop();
                Brake();
            }
        }
    }
    public void Walk(int angle)
    {
        if (_groundCheck.IsGrounded())
        {
            _rb.angularVelocity = Vector3.zero;
            Brake();
            _moveControl.MoveWalk(_rb, _leg.transform.forward * angle, _parameter.WalkPower, _parameter.MaxWalkSpeed * _parameter.ActionSpeed);
        }
    }
    public void Run(int angle)
    {
        if (_groundCheck.IsGrounded())
        {
            _rb.angularVelocity = Vector3.zero;
            _moveControl.MoveWalk(_rb, _leg.transform.forward * angle, _parameter.RunPower, _parameter.MaxRunSpeed * _parameter.ActionSpeed);
        }
    }
    public void Move(int angle)
    {
        if (_groundCheck.IsGrounded())
        {
            _rb.angularVelocity = Vector3.zero;
            _moveControl.MoveFloat(_rb, _leg.transform.forward * angle, _parameter.WalkPower, _parameter.MaxWalkSpeed * _parameter.ActionSpeed);
        }
    }
    public void Jump()
    {
        if (_knockDown)
        {
            return;
        }
        if (FloatMode)
        {
            ActionStop();
            _moveControl.Jet(_rb, Vector3.up, _parameter.JetPower);
            return;
        }
        if (_parameter.JetPower >= MachineStatus.JetLimit)
        {
            if (_booster.BoostCheckFly())
            {
                _booster.Boost();
            }
        }
        ActionStop();
        _leg.StartJump();
    }
    public void Boost()
    {
        if (_knockDown)
        {
            return;
        }
        if (_parameter.JetPower < MachineStatus.JetLimit)
        {
            return;
        }
        if (_jump && _booster.BoostCheckFly())
        {
            Vector3 vector = _bodyAngle.forward * _inputAxis.z + _bodyAngle.right * _inputAxis.x;
            _moveControl.Jet(_rb, Vector3.up + vector * _parameter.JetMovePower, _parameter.JetPower);
            if (_booster.CurrentBoostPower <= 0)
            {
                _booster.BoostEnd();
            }
        }
    }
    public void JetStart()
    {
        if (_knockDown)
        {
            return;
        }
        if (_jet || _parameter.JetPower < MachineStatus.JetLimit)
        {
            return;
        }
        if (_inputAxis == Vector3.zero)
        {
            return;
        }
        if (!_booster.BoostCheckJet())
        {
            return;
        }
        _jet = true;
        if (_inputAxis.x > InputStatus.InputLimit)
        {
            _booster.BoostL();
        }
        else if (_inputAxis.x < -InputStatus.InputLimit)
        {
            _booster.BoostR();
        }
        else
        {
            _booster.BoostL();
            _booster.BoostR();
        }
        if (_inputAxis.z > 0)
        {
            _booster.BoostF();
        }
        else
        {
            _booster.BoostB();
        }
        _leg.StartJet();
        _booster.Boost();
    }
    public void Jet()
    {
        if (_knockDown)
        {
            return;
        }
        _body.QuickTurn();
        if (_parameter.JetPower < MachineStatus.JetLimit)
        {
            return;
        }
        if (_groundCheck.IsGrounded())
        {
            _booster.BoostCheckJet();
        }
        StrongTurn();
        Vector3 vector = _bodyAngle.forward * _inputAxis.z + _bodyAngle.right * _inputAxis.x;
        _rb.AddForce(vector * _parameter.JetImpulsePower + Vector3.up, ForceMode.Impulse);
        _legTransform.localRotation = _legRotaion;
        _jet = false;
    }
    public void Landing()
    {
        _jump = false;
        _jet = false;
        Brake();
        ActionStop();
        _body.ResetAngle(MachineStatus.LandingBodyReset);
    }
    public void StartJump(Vector3 dir)
    {
        _jump = true;
        _rb.angularVelocity = Vector3.zero;
        _moveControl.Jump(_rb, dir, _parameter.JumpPower);
        _body.QuickTurn();
    }
    public void AngleMove(Vector3 dir)
    {
        if (!_booster.BoostCheckFly())
        {
            return;
        }
        _rb.velocity = dir * _parameter.JetPower * _parameter.WalkPower;
    }
    public void AngleBooster(Vector3 dir)
    {
        if (!_booster.BoostCheckFly())
        {
            return;
        }
        _inputAxis = dir.normalized;
        _rb.velocity = dir * _parameter.JetPower * _parameter.RunPower;
    }
    public void Booster()
    {
        _booster.BoostL();
        _booster.BoostR();
        _booster.BoostF();
    }
    public void Turn(float angle)
    {
        _legRotaion = Quaternion.Euler(Vector3.up * angle * _parameter.TurnPower) * _legRotaion;
    }
    public void Turn(Quaternion angle)
    {
        _legRotaion = angle * _legRotaion;
    }
    public void Turn()
    {
        _legRotaion = Quaternion.Euler(Vector3.up * _body.BodyAngle.y * _parameter.TurnPower) * _legRotaion;
    }
    public void StrongTurn()
    {
        _legRotaion = Quaternion.Euler(Vector3.up * _body.BodyAngle.y * _parameter.TurnPower * MachineStatus.StrongTurn) * _legRotaion;
    }
    public void SetAngle(Vector3 angle)
    {
        angle.y = 0;
        _legRotaion = Quaternion.Euler(angle);
    }
    public void ActionStop()
    {
        _rb.angularVelocity = Vector3.zero;
        _inputAxis = Vector3.zero;
        _body.BodyResetAngle();
        _jet = false;
    }
    public void Brake()
    {
        var v = _rb.velocity;
        _rb.velocity = v * MachineStatus.BrakePower; 
        if (FloatMode)
        {
            return;
        }
        if (_groundCheck.IsGrounded())
        {
            _booster.BoostEnd();
            _booster.BoostRecovery();
        }
    }
    public bool IsGrounded()
    {
        return _groundCheck.IsGrounded();
    }
    void BodyBreak()
    {
        if (_knockDown)
        {
            return;
        }
        _knockDown = true;
        _body.KnockDown();
        _leg.KnockDown();
        _booster.BoostEnd();
        StartCoroutine(Explosion());
    }
    public void ChangeFloat()
    {
        if (_leg.IsLanding)
        {
            return;
        }
        if (FloatMode)
        {
            FloatMode = false;
            _booster.BoostEnd();
            _leg.ChangeMode(false);
        }
        else
        {
            if (_booster.BoostCheckFly())
            {
                FloatMode = true;
                _booster.Boost();
                _leg.ChangeMode(true);
            }
            else
            {
                _booster.BoostEnd();
            }
        }
    }
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(MachineStatus.ExplosionWait);
        EffectPool.Get(EffectType.HeavyExplosion, BodyControl.BodyTransform.position);
        gameObject.SetActive(false);
        OnBreak?.Invoke();
    }
}
