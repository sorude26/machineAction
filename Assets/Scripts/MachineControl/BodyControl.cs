﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyControl : MonoBehaviour
{
    [SerializeField]
    Animator _animator = default;
    [SerializeField]
    GroundCheck _groundCheck = default;
    [SerializeField]
    LegControl _leg = default;
    [SerializeField]
    Transform _bodyRotaionTarget = default;
    [SerializeField]
    Transform[] _bodyControlBase = new Transform[2];
    [SerializeField]
    Transform[] _rightControlBase = new Transform[4];
    [SerializeField]
    Transform[] _leftControlBase = new Transform[4];
    [SerializeField]
    Transform[] _controlTarget = new Transform[3];
    protected Quaternion _bodyRotaion = Quaternion.Euler(0, 0, 0);
    protected Quaternion _headRotaion = Quaternion.Euler(0, 0, 0);
    protected Quaternion _lArmRotaion = Quaternion.Euler(0, 0, 0);
    protected Quaternion _lArmRotaion2 = Quaternion.Euler(0, 0, 0);
    protected Quaternion _rArmRotaion = Quaternion.Euler(0, 0, 0);
    protected Quaternion _rArmRotaion2 = Quaternion.Euler(0, 0, 0);
    int _attackCount = 0;
    bool _action = false;
    bool _attack = false;
    bool _shot = false;
    bool _knockDown = false;
    Vector3 _targetCurrentPos = default;
    Vector3 _targetBeforePos = default;
    Vector3 _targetTwoBeforePos = default;
    Vector3 _targetCurrentPosL = default;
    Vector3 _targetBeforePosL = default;
    Vector3 _targetTwoBeforePosL = default;
    Vector3 _targetCurrentPosR = default;
    Vector3 _targetBeforePosR = default;
    Vector3 _targetTwoBeforePosR = default;
    MachineController _machine = default;
    AttackControl attackControl = default;
    public float BodyRSpeed { get; set; } = 3.0f;
    public float BodyTurnRange { get; set; } = 0.4f;
    public float CameraRange { get; set; } = 50f;
    public Quaternion BodyAngle { get => _bodyControlBase[0].localRotation; }
    public Transform BodyTransform { get => _bodyControlBase[0]; }
    public FightingType Fighting { get; set; }
    private void Awake()
    {
        if (_animator)
        {
            _animator.enabled = false;
        }
    }
    public void Set(MachineController controller)
    {
        _machine = controller;
        attackControl = new AttackControl();
        Fighting = attackControl.GetType(controller); 
        if (_animator)
        {
            _animator.enabled = true;
        }
    }
    public void ChangeSpeed(float speed)
    {
        if (_animator)
        {
            _animator.SetFloat("Speed", speed);
        }
    }
    public void KnockDown()
    {
        if (_knockDown)
        {
            return;
        }
        _knockDown = true;
        ChangeAnimation("Damage");
    }
    public void HandAttackLeft()
    {
        if (_knockDown)
        {
            return;
        }
        _machine.SetTarget();
        if (_machine.LookTarget == null)
        {
            if (_machine.LAWeapon.Type == WeaponType.Rifle)
            {
                LockOnL(transform.position);
                ShotLeft();
            }
            else
            {
                FightingAttackL();
            }
            return;
        }
        var attack = LockOnL(_machine.LookTarget.position);
        if (_machine.LAWeapon.Type == WeaponType.Rifle)
        {
            if (attack)
            {
                ShotLeft();
            }
        }
        else
        {
            FightingAttackL();
        }
    }
    public void HandAttackRight()
    {
        if (_knockDown)
        {
            return;
        }
        _machine.SetTarget();
        if (_machine.LookTarget == null)
        {
            if (_machine.RAWeapon.Type == WeaponType.Rifle)
            {
                LockOnR(transform.position);
                ShotRight();
            }
            else
            {
                FightingAttackR();
            }
            return;
        }
        var attack = LockOnR(_machine.LookTarget.position);
        if (_machine.RAWeapon.Type == WeaponType.Rifle)
        {
            if (attack)
            {
                ShotRight();
            }
        }
        else
        {
            FightingAttackR();
        }
    }
    public void ShoulderShot()
    {
        if (_knockDown)
        {
            return;
        }
        _machine.SetTarget(); 
        if (_machine.LookTarget != null)
        {
            LockOn(_machine.LookTarget.position);
        }
        _machine.SWeapon.AttackAction(_targetCurrentPos);
    }
    public void BodyWeaponShot()
    {
        if (_knockDown)
        {
            return;
        }
        _machine.SetTarget();
        if (_machine.LookTarget != null)
        {
            LockOn(_machine.LookTarget.position);
        }
        _machine.BWeapon.AttackAction(_targetCurrentPos);
    }
    void ShotLeft()
    {
        _machine.LAWeapon.AttackAction(_targetCurrentPosL);
    }
    void ShotRight()
    {
        _machine.RAWeapon.AttackAction(_targetCurrentPosR);
    }
    bool LockOn(Vector3 targetPos)
    {
        if (_action)
        {
            return false;
        }
        _shot = true;
        bool attack = false;
        Vector3 targetDir = targetPos - _bodyControlBase[0].position;
        targetDir.y = 0.0f;
        if (BodyTurnRange > 0 && Vector3.Dot(targetDir.normalized, _bodyControlBase[0].forward) < BodyTurnRange)
        {
            return true;
        }
        _targetCurrentPos = DeviationShootingControl.CirclePrediction(_bodyControlBase[0].position, targetPos, _targetBeforePos, _targetTwoBeforePos, _machine.BWeapon.AttackSpeed());
        if (!_camera)
        {
            _controlTarget[0].forward = targetDir;
            if (BodyTurnRange > 0)
            {
                _bodyRotaion = ClampRotation(_controlTarget[0].localRotation);
            }
            else
            {
                _bodyRotaion = _controlTarget[0].localRotation;
            }
        }
        _targetTwoBeforePos = _targetBeforePos;
        _targetBeforePos = targetPos;
        return attack;
    }
    bool LockOnL(Vector3 targetPos)
    {
        if (_action)
        {
            return false;
        }
        if (LockOn(targetPos))
        {
            _controlTarget[2].forward = _bodyControlBase[0].forward;
            _lArmRotaion2 = _controlTarget[2].localRotation * Quaternion.Euler(-90, 0, 0);
            return true;
        }
        bool attack = false;
        if (_machine.LAWeapon.Type == WeaponType.Rifle)
        {
            _targetCurrentPosL = DeviationShootingControl.CirclePrediction(_leftControlBase[2].position, targetPos, _targetBeforePosL, _targetTwoBeforePosL, _machine.LAWeapon.AttackSpeed() * 0.8f);
            _controlTarget[2].forward = _targetCurrentPosL - _leftControlBase[2].position;
            _lArmRotaion2 = _controlTarget[2].localRotation * Quaternion.Euler(-90, 0, 0);
            var range = Quaternion.Dot(_lArmRotaion2, _leftControlBase[2].localRotation);
            if (range > MachineStatus.AttackRange || range < -MachineStatus.AttackRange)
            {
                attack = true;
            }
        }
        _targetTwoBeforePosL = _targetBeforePosL;
        _targetBeforePosL = targetPos;
        return attack;
    }
    bool LockOnR(Vector3 targetPos)
    {
        if (_action)
        {
            return false;
        }
        bool attack = false;
        if (LockOn(targetPos))
        {
            _controlTarget[1].forward = _bodyControlBase[0].forward;
            _rArmRotaion2 = _controlTarget[1].localRotation * Quaternion.Euler(-90, 0, 0);
            return true;
        }
        if (_machine.RAWeapon.Type == WeaponType.Rifle)
        {
            _targetCurrentPosR = DeviationShootingControl.CirclePrediction(_rightControlBase[2].position, targetPos, _targetBeforePosR, _targetTwoBeforePosR, _machine.RAWeapon.AttackSpeed() * 0.8f);
            _controlTarget[1].forward = _targetCurrentPosR - _rightControlBase[2].position;
            _rArmRotaion2 = _controlTarget[1].localRotation * Quaternion.Euler(-90, 0, 0);
            var range = Quaternion.Dot(_rArmRotaion2, _rightControlBase[2].localRotation);
            if (range > MachineStatus.AttackRange || range < -MachineStatus.AttackRange)
            {
                attack = true;
            }
        }
        _targetTwoBeforePosR = _targetBeforePosR;
        _targetBeforePosR = targetPos;
        return attack;
    }

    void ResetAngle()
    {
        _headRotaion = Quaternion.Euler(0, 0, 0);
        _bodyRotaion = Quaternion.Euler(0, 0, 0);
        _lArmRotaion = Quaternion.Euler(0, 0, 0);
        _lArmRotaion2 = Quaternion.Euler(0, 0, 0);
        _rArmRotaion = Quaternion.Euler(0, 0, 0);
        _rArmRotaion2 = Quaternion.Euler(0, 0, 0);
    }
    public void ResetAngle(float value)
    {
        if (value > 1f)
        {
            value = 1;
        }
        else if (value < 0)
        {
            value = 0;
        }
        float v = 1f - value;
        _machine?.Turn(BodyAngle.y * v);
        if (_camera)
        {
            return;
        }
        _bodyRotaion = Quaternion.Euler(_bodyRotaion.x * v, _bodyRotaion.y * v, _bodyRotaion.z * v);
    }
    public void FightingAttack()
    {
        if (_knockDown)
        {
            return;
        }
        _machine.SetTarget();
        AttackBooster();
        QuickTurn();
        _action = true;
        _shot = false;
        if (_attackCount == 0)
        {
            if (_groundCheck.IsGrounded())
            {
                ChangeAnimation(attackControl.AttackAction(Fighting, _attackCount));
                _leg.AttackMoveR();
            }
            else
            {
                ChangeAnimation(attackControl.AttackAction(Fighting, _attackCount));
            }
            _attackCount++;
            return;
        }
        _attack = true;
    }
    public void FightingAttackL()
    {
        if (_knockDown)
        {
            return;
        }
        _machine.SetTarget();
        AttackBooster(true);
        QuickTurn();
        if (_action)
        {
            return;
        }
        _action = true;
        if (_groundCheck.IsGrounded())
        {
            ChangeAnimation(attackControl.AttackActionL(Fighting, 2));
            _leg.AttackMoveL();
        }
        else
        {
            _machine.Booster();
            ChangeAnimation(attackControl.AttackActionL(Fighting, 1));
        }
    }
    public void FightingAttackR()
    {
        if (_knockDown)
        {
            return;
        }
        _machine.SetTarget();
        AttackBooster(true);
        QuickTurn();
        if (_action)
        {
            return;
        }
        _action = true;
        if (_groundCheck.IsGrounded())
        {
            ChangeAnimation(attackControl.AttackActionR(Fighting, 2));
            _leg.AttackMoveR();
        }
        else
        {
            _machine.Booster();
            ChangeAnimation(attackControl.AttackActionR(Fighting, 1));
        }
    }
    public void QuickTurn()
    {
        ResetAngle();
        _machine.Turn(BodyAngle.y);
    }
    void Attack()
    {
        if (_knockDown)
        {
            return;
        }
        if (_attack)
        {
            _shot = false;
            _machine.SetTarget();
            AttackBooster();
            if (_attackCount == 1)
            {
                ChangeAnimation(attackControl.AttackAction(Fighting, _attackCount));
                _leg?.AttackMoveL();
            }
            else if (_attackCount == 2)
            {
                ChangeAnimation(attackControl.AttackAction(Fighting, _attackCount));
                _leg?.AttackMoveR();
                _attackCount = 0;
            }
            _attackCount++;
            _attack = false;
        }
    }
    void AttackEnd()
    {
        _attackCount = 0;
        if (_action)
        {
            EndBlade();
            _action = false;
        }
        _attack = false;
    }
    void AttackBooster(bool force = false)
    {
        if (_machine.LookTarget != null)
        {           
            if (!_groundCheck.IsGrounded())
            {
                Vector3 targetDir = _machine.LookTarget.position - _bodyControlBase[0].position;
                SetBodyRotaion(targetDir.normalized);
                if (force)
                {
                    _machine.AngleBooster(targetDir.normalized);
                }
                else
                {
                    _machine.AngleMove(targetDir.normalized);
                }
            }
        }
    }
    void OnBladeL()
    {
        _machine.LAWeapon.AttackAction(_targetCurrentPosL);
    }
    void OnBladeR()
    {
        _machine.RAWeapon.AttackAction(_targetCurrentPosR);
    }
    void EndBlade()
    {
        _machine.LAWeapon.AttackEnd();
        _machine.RAWeapon.AttackEnd();
    }
    void ChangeAnimation(string changeTarget, float changeTime = 0.1f)
    {
        _animator.CrossFadeInFixedTime(changeTarget, changeTime);
    }

    public void PartsMotion()
    {
        _bodyControlBase[1].localRotation = Quaternion.Lerp(_bodyControlBase[1].localRotation, _headRotaion, BodyRSpeed * Time.deltaTime);
        _bodyControlBase[0].localRotation = Quaternion.Lerp(_bodyControlBase[0].localRotation, _bodyRotaion, BodyRSpeed * Time.deltaTime);
        _leftControlBase[0].localRotation = Quaternion.Lerp(_leftControlBase[0].localRotation, _lArmRotaion, BodyRSpeed * Time.deltaTime);
        _leftControlBase[2].localRotation = Quaternion.Lerp(_leftControlBase[2].localRotation, _lArmRotaion2, BodyRSpeed * Time.deltaTime);
        _rightControlBase[0].localRotation = Quaternion.Lerp(_rightControlBase[0].localRotation, _rArmRotaion, BodyRSpeed * Time.deltaTime);
        _rightControlBase[2].localRotation = Quaternion.Lerp(_rightControlBase[2].localRotation, _rArmRotaion2, BodyRSpeed * Time.deltaTime);
        _bodyRotaionTarget.localRotation = _bodyControlBase[0].localRotation;
    }
    int _angle = default;
    bool _camera = false;
    public void SetBodyRotaion(Vector3 angle)
    {
        _controlTarget[0].forward = angle;
        _bodyRotaion = ClampRotation(_controlTarget[0].localRotation);
    }
    public void SetBodyRotaion(Quaternion angle)
    {
        if (_knockDown)
        {
            return;
        }
        if (_action)
        {
            return;
        }
        angle.x = 0;
        angle.z = 0;
        _bodyRotaion = angle;
        _camera = true;
        if (_machine.FloatMode)
        {
            //angle.y *= MachineStatus.FloatTurnSpeed;
            angle.y *= Time.deltaTime;
            _machine.Turn(angle);
            return;
        }
        if (angle.y > 0)
        {
            if (_angle < 0)
            {
                _angle = 1;
                _machine.MoveEnd();
            }
            else
            {
                _angle = 1;
                if (angle.y > BodyTurnRange)
                {
                    _machine.Move(angle.y, _machine.InputAxis.y);
                }
            }
        }
        else if (angle.y < 0)
        {
            if (_angle > 0)
            {
                _angle = -1;
                _machine.MoveEnd();
            }
            else
            {
                _angle = -1;
                if (angle.y < -BodyTurnRange)
                {
                    _machine.Move(angle.y, _machine.InputAxis.y);
                }
            }
        }
    }
    public void BodyResetAngle()
    {
        if (_camera || _shot)
        {
            return;
        }
        _bodyRotaion = Quaternion.Euler(0, _bodyRotaion.y, 0);
    }
    public void InputEnd()
    {
        _machine.MoveEnd();
        _camera = false;
    }
    public void ShotEnd()
    {
        _shot = false;
    }
    Quaternion ClampRotation(Quaternion angle, float maxX = 80f, float maxY = 80f, float maxZ = 80f, float minX = -80f, float minY = -80f, float minZ = -80f)
    {
        angle.x /= angle.w;
        angle.y /= angle.w;
        angle.z /= angle.w;
        angle.w = 1f;
        float angleX = Mathf.Atan(angle.x) * Mathf.Rad2Deg * 2f;
        angleX = Mathf.Clamp(angleX, minX, maxX);
        angle.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);
        float angleY = Mathf.Atan(angle.y) * Mathf.Rad2Deg * 2f;
        angleY = Mathf.Clamp(angleY, minY, maxY);
        angle.y = Mathf.Tan(angleY * Mathf.Deg2Rad * 0.5f);
        float angleZ = Mathf.Atan(angle.z) * Mathf.Rad2Deg * 2f;
        angleZ = Mathf.Clamp(angleZ, minZ, maxZ);
        angle.z = Mathf.Tan(angleZ * Mathf.Deg2Rad * 0.5f);
        return angle;
    }
}
