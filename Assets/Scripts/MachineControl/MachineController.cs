using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MachineController : MonoBehaviour
{
    [SerializeField]
    MachineParameter _parameter = default;
    [SerializeField]
    MoveControl _moveControl = default;
    [SerializeField]
    TurnControl _trunControl = default;
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
    Transform _camera = default;
    [SerializeField]
    bool _fly = default;
    bool _jump = false;
    float _boosterTimer = -1;
    Vector3 _inputAxis = Vector3.zero;
    public Vector3 InputAxis { get => _inputAxis; }

    public WeaponMaster RAWeapon { get; set; }
    public WeaponMaster LAWeapon { get; set; }
    private void Start()
    {
        GameScene.InputManager.Instance.OnInputAxisRaw += Move;
        GameScene.InputManager.Instance.OnInputAxisRawExit += MoveEnd;
        GameScene.InputManager.Instance.OnFirstInputJump += Jump;
        GameScene.InputManager.Instance.OnInputJump += Boost;
        GameScene.InputManager.Instance.OnFirstInputBooster += JetStart;
        _rb = GetComponent<Rigidbody>();        
        _leg.Set(this);
        _leg.SetLandingTime(_parameter.LandingTime);
        _leg.ChangeSpeed(_parameter.ActionSpeed);
        _body.Set(this);
        _body.ChangeSpeed(_parameter.ActionSpeed);
    }

    private void OnValidate()
    {
        _leg.ChangeSpeed(_parameter.ActionSpeed);
        _body.ChangeSpeed(_parameter.ActionSpeed);
        _leg.SetLandingTime(_parameter.LandingTime);
    }
    private void Move(float horizonal, float vertical)
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
            else if(_jump)
            {
                _rb.angularVelocity = Vector3.zero;
                _trunControl.Turn(_rb, dir.x, _parameter.TurnPower, _parameter.TurnSpeed);
            }

        }
        else
        {
            if(vertical != 0)
            {
                Stop();
            }
            _moveControl.MoveFloat(_rb, dir, _parameter.FloatSpeed, _parameter.MaxFloatSpeed);
            if (horizonal > 0)
            {
                _trunControl.Turn(_rb, 1, _parameter.TurnPower * 0.05f, _parameter.TurnSpeed * 0.1f);
            }
            else if (horizonal < 0)
            {
                _trunControl.Turn(_rb, -1, _parameter.TurnPower * 0.05f, _parameter.TurnSpeed * 0.1f);
            }
        }
    }
    private void MoveEnd()
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
            _moveControl.MoveWalk(_rb, transform.forward * angle, _parameter.WalkPower, _parameter.MaxWalkSpeed);
        }
    }
    public void Jump()
    {
        if (_fly)
        {
            Stop();
            _moveControl.Jet(_rb,Vector3.up * 0.5f, _parameter.JetPower);
            return;
        }
        if (_boosterTimer <= -1 || _boosterTimer > 0)
        {
            _booster.Boost();
        }
        Stop();
        _leg.StartJump();
    }
    public void Boost()
    {
        if (_jump && _boosterTimer > 0)
        {
            _rb.AddForce(Vector3.zero, ForceMode.Acceleration);
            _boosterTimer -= Time.deltaTime;
            Vector3 vector = _body.BodyTransform.forward * _inputAxis.z + _body.BodyTransform.right * _inputAxis.x;
            _moveControl.Jet(_rb, Vector3.up + vector * _parameter.JetControlPower, _parameter.JetPower);
            if (_boosterTimer <= 0)
            {
                _booster.BoostEnd();
            }
        }
    }
    public void JetStart()
    {
        if (_inputAxis == Vector3.zero)
        {
            return;
        }
        if (_inputAxis.x != 0)
        {
            if (_inputAxis.x > 0.5f)
            {
                _booster.BoostL();
            }
            else if(_inputAxis.x < -0.5f)
            {
                _booster.BoostR();
            }
            else
            {
                _booster.BoostL();
                _booster.BoostR();
            }
        }
        else
        {
            if (_inputAxis.z > 0)
            {
                _booster.BoostL();
                _booster.BoostR();
            }
        }
        _leg.StartJet();
        _booster.Boost();
    }
    public void Jet()
    {
        Vector3 vector = transform.forward * _inputAxis.z + transform.right * _inputAxis.x;
        _rb.AddForce(vector * _parameter.FloatSpeed + Vector3.up * 0.7f, ForceMode.Impulse);
    }
    public void Landing()
    {
        _jump = false;
        _booster.BoostEnd();
        _boosterTimer = -1;
        Brake();
        Stop();
    }
    public void StartJump(Vector3 dir)
    {
        _boosterTimer = _parameter.JetTime;
        _jump = true;
        _rb.angularVelocity = Vector3.zero;
        _moveControl.Jump(_rb, dir, _parameter.JumpPower);
    }
    public void Turn(float angle)
    {
        _trunControl.Turn(_rb, angle, _parameter.TurnPower, _parameter.TurnSpeed);
    }
    public void Turn()
    {
        _trunControl.StrongTurn(_rb, _body.BodyAngle.y, _parameter.TurnPower, _parameter.TurnSpeed);
    }
    public void Stop()
    {
        _rb.angularVelocity = Vector3.zero;
        _inputAxis = Vector3.zero;
    }
    public void Brake()
    {
        var v = _rb.velocity;
        _rb.velocity = v * 0.3f; 
        _booster.BoostEnd();
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
