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
    public Quaternion BodyAngle { get => _bodyControlBase[0].localRotation; }
    public Transform BodyTransform { get => _bodyControlBase[0]; }
    private void Start()
    {
        GameScene.InputManager.Instance.OnFirstInputAttack += HandAttackRight;
        GameScene.InputManager.Instance.OnFirstInputShotR += HandAttackLeft;
        GameScene.InputManager.Instance.OnShotEnd += ResetAngle;
    }
    private void Update()
    {
        PartsMotion();
    }
    public void Set(MachineController controller)
    {
        _machine = controller;
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
        if (_action)
        {
            return;
        }
        if (_machine?.LAWeapon.Type == WeaponType.Rifle)
        {
            ShotLeft();
        }
        else if (_machine?.LAWeapon.Type == WeaponType.Blade)
        {

        }
        LockOn(_target.position);
    }
    void ShotLeft()
    {
        _machine.LAWeapon.AttackAction();
    }
    void ShotRight()
    {
        _machine.RAWeapon.AttackAction();
    }
    void LockOn(Vector3 targetPos)
    {
        Vector3 targetDir = targetPos - _bodyControlBase[0].position;
        targetDir.y = 0.0f; 
        if (Vector3.Dot(targetDir.normalized, transform.forward.normalized) < 0.4f)
        {
            return;
        }
        _controlTarget[0].forward = targetDir;
        _bodyRotaion = _controlTarget[0].localRotation;
        if (true)
        {
            _rArmRotaion = Quaternion.Euler(-10, 0, 10);
            targetDir = targetPos - _rightControlBase[2].position;
            _controlTarget[1].forward = targetDir;
            _rArmRotaion2 = _controlTarget[1].localRotation * Quaternion.Euler(-90, 0, 0);
        }
        if (true)
        {
            _lArmRotaion = Quaternion.Euler(-10, 0, -10);
            targetDir = targetPos - _leftControlBase[2].position;
            _controlTarget[2].forward = targetDir;
            _lArmRotaion2 = _controlTarget[2].localRotation * Quaternion.Euler(-90, 0, 0);
        }
    }
    void ResetAngle()
    {
        _bodyRotaion = Quaternion.Euler(0, 0, 0);
        _lArmRotaion = Quaternion.Euler(0, 0, 0);
        _lArmRotaion2 = Quaternion.Euler(0, 0, 0);
        _rArmRotaion = Quaternion.Euler(0, 0, 0);
        _rArmRotaion2 = Quaternion.Euler(0, 0, 0);
    }
    public void HandAttackRight()
    {
        ResetAngle();
        _action = true;
        if (_attackCount == 0)
        {
            _attackCount++;
            if (_groundCheck.IsGrounded())
            {
                ChangeAnimation("attackSwingRArm");
                //ChangeAnimation("attackSwingDArm");
                //ChangeAnimation("attackKnuckleRArm");
                _leg?.AttackMoveR();
            }
            else
            {
                _machine?.Turn(BodyAngle.y * 10);
                ChangeAnimation("attackSwingRArm3");
                //ChangeAnimation("attackSwingDArm3");
                //ChangeAnimation("attackKnuckleRArm");
            }
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
                ChangeAnimation("attackSwingRArm2");
                //ChangeAnimation("attackSwingDArm2");
                //ChangeAnimation("attackKnuckleLArm");
                _leg?.AttackMoveL();
            }
            else if (_attackCount == 2)
            {
                ChangeAnimation("attackSwingRArm3",0.1f);
                //ChangeAnimation("attackSwingDArm3");
                //ChangeAnimation("attackKnuckleRArm");
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
    void ChangeAnimation(string changeTarget, float changeTime = 0.5f)
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
