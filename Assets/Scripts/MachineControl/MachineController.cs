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
    bool _fly = default;
    bool _jump = false;
    bool _jet = false;
    float _boosterTimer = -1;
    Vector3 _inputAxis = Vector3.zero;
    Quaternion _legRotaion = Quaternion.Euler(0, 0, 0);
    PartsManager _parts = default;
    public Vector3 InputAxis { get => _inputAxis; }
    public BodyControl BodyControl { get => _body; }
    public PartsManager MachineParts { get => _parts; }
    public WeaponMaster RAWeapon { get => _parts.RAWeapon; }
    public WeaponMaster LAWeapon { get => _parts.LAWeapon; }
    public WeaponMaster BWeapon { get => _parts.BodyWeapon; }
    public ShoulderWeapon SWeapon { get => _parts.ShoulderWeapon; }
    public Transform LookTarget { get; protected set; }
    public void StartSet()
    {
        _parts = new PartsManager();
        _buildControl.StartSet(_parts);
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
    }
    private void Update()
    {
        _body.PartsMotion();
        _leg.transform.localRotation = Quaternion.Lerp(_leg.transform.localRotation, _legRotaion, _parameter.TurnSpeed * Time.deltaTime);
    }
    public void SetTarget()
    {
        var target = BattleManager.Instance.GetTarget();
        if (target)
        {
            LookTarget = target.Center;
        }
        else
        {
            LookTarget = null;
        }
        _mark.SetTarget(target);
    }
    public void Move(float horizonal, float vertical)
    {
        _inputAxis = new Vector3(horizonal, 0, vertical);
        var dir = _inputAxis;
        dir.x += _body.BodyAngle.y;
        if (!_fly)
        {
            if (_groundCheck.IsGrounded())
            {
                if (dir.z > 0.1f)
                {
                    _leg.WalkStart(1);
                }
                else if (dir.z < -0.1f)
                {
                    _leg.WalkStart(-1);
                }
                if (dir.x > 0.1f)
                {
                    _leg.TurnStartRight();
                }
                else if (dir.x < -0.1f)
                {
                    _leg.TurnStartLeft();
                }
            }
            else if (_jump && !_jet)
            {
                Turn(horizonal * 0.05f);
                _body.ResetAngle(0.8f);
                _moveControl.Jet(_rb, _body.BodyTransform.forward * _inputAxis.z + _body.BodyTransform.right * _inputAxis.x, _parameter.JetPower, _parameter.JetControlPower);
            }

        }
        else
        {
            if (vertical != 0)
            {
                Stop();
            }
            _moveControl.MoveFloat(_rb, dir, _parameter.FloatSpeed, _parameter.MaxFloatSpeed);
            if (horizonal > 0)
            {

            }
            else if (horizonal < 0)
            {

            }
        }
    }
    public void MoveEnd()
    {
        if (!_fly)
        {
            _leg.WalkStop();
            if (_groundCheck.IsGrounded())
            {
                Stop();
                Brake();
            }
        }
    }
    public void Walk(int angle)
    {
        if (_groundCheck.IsGrounded())
        {
            _rb.angularVelocity = Vector3.zero;
            _moveControl.MoveWalk(_rb, _leg.transform.forward * angle, _parameter.WalkPower, _parameter.MaxWalkSpeed);
        }
    }
    public void Jump()
    {
        if (_fly)
        {
            Stop();
            _moveControl.Jet(_rb, Vector3.up * 0.5f, _parameter.JetPower);
            return;
        }
        if (_parameter.JetPower >= 1f)
        {
            if (_boosterTimer <= -1 || _boosterTimer > 0)
            {
                _booster.Boost();
            }
        }
        Stop();
        _leg.StartJump();
    }
    public void Boost()
    {
        if (_parameter.JetPower < 1f)
        {
            return;
        }
        if (_jump && _boosterTimer > 0)
        {
            _boosterTimer -= Time.deltaTime;
            Vector3 vector = _bodyAngle.forward * _inputAxis.z + _bodyAngle.right * _inputAxis.x;
            _moveControl.Jet(_rb, Vector3.up + vector * _parameter.JetMovePower, _parameter.JetPower);
            if (_boosterTimer <= 0)
            {
                _booster.BoostEnd();
            }
        }
    }
    public void JetStart()
    {
        if (_jet || _parameter.JetPower < 1f)
        {
            return;
        }
        if (_inputAxis == Vector3.zero)
        {
            return;
        }
        _jet = true;
        if (_inputAxis.x > 0.5f)
        {
            _booster.BoostL();
        }
        else if (_inputAxis.x < -0.5f)
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
        _body.QuickTurn();
        if (_parameter.JetPower < 1f)
        {
            return;
        }
        StrongTurn();
        Vector3 vector = _bodyAngle.forward * _inputAxis.z * 1.5f + _bodyAngle.right * _inputAxis.x;
        _rb.AddForce(vector * _parameter.FloatSpeed + Vector3.up * 0.7f, ForceMode.Impulse);
        _leg.transform.localRotation = _legRotaion;
        _jet = false;
    }
    public void Landing()
    {
        _jump = false;
        _jet = false;
        _booster.BoostEnd();
        _boosterTimer = -1;
        Brake();
        Stop();
        _body.ResetAngle(0.1f);
    }
    public void StartJump(Vector3 dir)
    {
        _boosterTimer = _parameter.JetTime;
        _jump = true;
        _rb.angularVelocity = Vector3.zero;
        _moveControl.Jump(_rb, dir, _parameter.JumpPower);
        _body.QuickTurn();
    }
    public void AngleMove(Vector3 dir)
    {
        _booster.BoostL();
        _booster.BoostR();
        _rb.velocity = dir * _parameter.JetPower * _parameter.WalkPower;
    }
    public void AngleBooster(Vector3 dir)
    {
        _booster.BoostL();
        _booster.BoostR();
        _booster.BoostF();
        _rb.velocity = dir * _parameter.JetPower * _parameter.WalkPower * 2f;
    }
    public void Turn(float angle)
    {
        _legRotaion = Quaternion.Euler(Vector3.up * angle * _parameter.TurnPower) * _legRotaion;
    }
    public void Turn()
    {
        _legRotaion = Quaternion.Euler(Vector3.up * _body.BodyAngle.y * _parameter.TurnPower) * _legRotaion;
    }
    public void StrongTurn()
    {
        _legRotaion = Quaternion.Euler(Vector3.up * _body.BodyAngle.y * _parameter.TurnPower * 8f) * _legRotaion;
    }
    public void SetAngle(Vector3 angle)
    {
        angle.y = 0;
        _legRotaion = Quaternion.Euler(angle);
    }
    public void Stop()
    {
        _rb.angularVelocity = Vector3.zero;
        _inputAxis = Vector3.zero;
        _jet = false;
    }
    public void Brake()
    {
        var v = _rb.velocity;
        _rb.velocity = v * 0.3f;
        if (_groundCheck.IsGrounded())
        {
            _booster.BoostEnd();
        }
    }
    public bool IsGrounded()
    {
        return _groundCheck.IsGrounded();
    }
    void ChangeFloat()
    {
        if (_fly)
        {
            _fly = false;
        }
        else
        {
            _fly = true;
        }
        _leg.ChangeMode();
    }
}
