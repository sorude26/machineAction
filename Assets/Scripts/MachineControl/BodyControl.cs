using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyControl : MonoBehaviour
{
    [SerializeField]
    Transform _target = default;
    [SerializeField]
    Animator _animator = default;
    [SerializeField]
    GroundCheck _groundCheck = default;
    [SerializeField]
    LegControl _leg = default;
    [SerializeField]
    Transform[] _bodyControlBase = new Transform[2];
    [SerializeField]
    Transform[] _rightControlBase = new Transform[4];
    [SerializeField]
    Transform[] _leftControlBase = new Transform[4];
    [SerializeField]
    Transform[] _controlTarget = new Transform[3];
    protected Quaternion _bodyRotaion = Quaternion.Euler(0, 0, 0);
    protected float _bodyRSpeed = 3.0f;
    protected Quaternion _lArmRotaion = Quaternion.Euler(0, 0, 0);
    protected float _lArmRSpeed = 4.0f;
    protected Quaternion _lArmRotaion2 = Quaternion.Euler(0, 0, 0);
    protected float _lArmRSpeed2 = 4.0f;
    protected Quaternion _rArmRotaion = Quaternion.Euler(0, 0, 0);
    protected float _rArmRSpeed = 4.0f;
    protected Quaternion _rArmRotaion2 = Quaternion.Euler(0, 0, 0);
    protected float _rArmRSpeed2 = 4.0f;
    bool _action = false;
    int _attackCount = 0;
    bool _attack = false;
    MachineController _machine = default;
    AttackControl attackControl = default;
    public Quaternion BodyAngle { get => _bodyControlBase[0].localRotation; }
    public Transform BodyTransform { get => _bodyControlBase[0]; }
    public FightingType Fighting { get; set; }
    private void Start()
    {
        GameScene.InputManager.Instance.OnFirstInputAttack += FightingAttack;
        GameScene.InputManager.Instance.OnFirstInputShotL += HandAttackLeft;
        GameScene.InputManager.Instance.OnFirstInputShotR += HandAttackRight;
        GameScene.InputManager.Instance.OnShotEnd += ResetAngle;
    }
    private void Update()
    {
        PartsMotion();
    }
    public void Set(MachineController controller)
    {
        _machine = controller;
        attackControl = new AttackControl();
        Fighting = attackControl.GetType(controller);
    }
    public void ChangeSpeed(float speed)
    {
        if (_animator)
        {
            _animator.SetFloat("Speed", speed);
        }
    }
    public void HandAttackLeft()
    {
        if (_target == null)
        {
            return;
        }
        var attack = LockOn(_target.position);
        if (_machine.LAWeapon.Type == WeaponType.Rifle && !_action)
        {
            if (attack)
            {
                ShotLeft();
            }
        }
        else
        {
            FightingAttack();
        }
    }
    public void HandAttackRight()
    {
        if (_target == null)
        {
            return;
        }
        var attack = LockOn(_target.position);
        if (_machine.RAWeapon.Type == WeaponType.Rifle && !_action)
        {
            if (attack)
            {
                ShotRight();
            }
        }
        else
        {
            FightingAttack();
        }
    }
    void ShotLeft()
    {
        _machine.LAWeapon.AttackAction();
    }
    void ShotRight()
    {
        _machine.RAWeapon.AttackAction();
    }
    bool LockOn(Vector3 targetPos)
    {
        bool attack = false;
        Vector3 targetDir = targetPos - _bodyControlBase[0].position;
        targetDir.y = 0.0f; 
        if (Vector3.Dot(targetDir.normalized, transform.forward.normalized) < 0.4f)
        {
            return true;
        }
        if (Vector3.Dot(targetDir.normalized, _bodyControlBase[0].forward.normalized) < 0.6f)
        {
            return true;
        }
        _controlTarget[0].forward = targetDir;
        _bodyRotaion = _controlTarget[0].localRotation;
        if (_machine.RAWeapon.Type == WeaponType.Rifle)
        {
            targetDir = targetPos - _rightControlBase[2].position;
            _controlTarget[1].forward = targetDir;
            _rArmRotaion2 = _controlTarget[1].localRotation * Quaternion.Euler(-90, 0, 0);
            var range = Quaternion.Dot(_rArmRotaion2, _rightControlBase[2].localRotation);
            if (range > 0.999f || range < -0.999f)
            {
                attack = true;
            }
        }
        if (_machine.LAWeapon.Type == WeaponType.Rifle)
        {
            targetDir = targetPos - _leftControlBase[2].position;
            _controlTarget[2].forward = targetDir;
            _lArmRotaion2 = _controlTarget[2].localRotation * Quaternion.Euler(-90, 0, 0);
            var range = Quaternion.Dot(_lArmRotaion2, _leftControlBase[2].localRotation);
            if (range > 0.999f || range < -0.999f)
            {
                attack = true;
            }
        }
        return attack;
    }
    void ResetAngle()
    {
        _bodyRotaion = Quaternion.Euler(0, 0, 0);
        _lArmRotaion = Quaternion.Euler(0, 0, 0);
        _lArmRotaion2 = Quaternion.Euler(0, 0, 0);
        _rArmRotaion = Quaternion.Euler(0, 0, 0);
        _rArmRotaion2 = Quaternion.Euler(0, 0, 0);
    }
    public void FightingAttack()
    {
        ResetAngle();
        _action = true;
        if (_attackCount == 0)
        {
            if (_groundCheck.IsGrounded())
            {
                ChangeAnimation(attackControl.AttackAction(Fighting, _attackCount));
                _leg?.AttackMoveR();
            }
            else
            {
                _machine?.Turn(BodyAngle.y * 10);
                ChangeAnimation(attackControl.AttackAction(Fighting, _attackCount));
            }
            _attackCount++;
            return;
        }
        _attack = true;
    }
    void Attack()
    {
        if (_attack)
        {
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
        _attack = false;
        _action = false;
    }
    void ChangeAnimation(string changeTarget, float changeTime = 0.1f)
    {
        _animator.CrossFadeInFixedTime(changeTarget, changeTime);
    }

    protected void PartsMotion()
    {
        _bodyControlBase[0].localRotation = Quaternion.Lerp(_bodyControlBase[0].localRotation, _bodyRotaion, _bodyRSpeed * Time.deltaTime);
        _leftControlBase[0].localRotation = Quaternion.Lerp(_leftControlBase[0].localRotation, _lArmRotaion, _lArmRSpeed * Time.deltaTime);
        _leftControlBase[2].localRotation = Quaternion.Lerp(_leftControlBase[2].localRotation, _lArmRotaion2, _lArmRSpeed2 * Time.deltaTime);
        _rightControlBase[0].localRotation = Quaternion.Lerp(_rightControlBase[0].localRotation, _rArmRotaion, _rArmRSpeed * Time.deltaTime);
        _rightControlBase[2].localRotation = Quaternion.Lerp(_rightControlBase[2].localRotation, _rArmRotaion2, _rArmRSpeed2 * Time.deltaTime);
    }
    public void SetBodyRotaion(Quaternion angle)
    {
        if (_action)
        {
            return;
        }
        _bodyRotaion = angle;
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
