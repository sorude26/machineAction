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
    Transform _camera = default;
    [SerializeField]
    LegControl _leg = default;
    [SerializeField]
    float _maxUpAngle = 20f;
    [SerializeField]
    float _maxDownAngle = -10f;
    [SerializeField]
    Transform[] _bodyControlBase = new Transform[2];
    [SerializeField]
    Transform[] _rightControlBase = new Transform[4];
    [SerializeField]
    Transform[] _leftControlBase = new Transform[4];
    [SerializeField]
    Transform[] _controlTarget = new Transform[3];
    protected Quaternion _headRotaion = Quaternion.Euler(0, 0, 0);
    protected float _headRSpeed = 1.0f;
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
    public Transform BaseTransform { get => _bodyControlBase[0]; }
    private void Start()
    {
        GameScene.InputManager.Instance.OnFirstInputAttack += HandAttackRight;
        GameScene.InputManager.Instance.OnFirstInputShotR += HandAttackLeft;
        GameScene.InputManager.Instance.OnShotEnd += ResetAngle;
    }
    private void LateUpdate()
    {
        PartsMotion();
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
        _rArmRotaion = Quaternion.Euler(-10, 0, 10);
        _lArmRotaion = Quaternion.Euler(-10, 0, -10);
        Vector3 targetPos = _target.position;
        Vector3 targetDir = targetPos - _bodyControlBase[0].position ;
        targetDir.y = 0.0f;
        _controlTarget[0].forward = targetDir;
        targetDir = targetPos - _rightControlBase[2].position;
        _controlTarget[1].forward = targetDir;
        targetDir = targetPos - _leftControlBase[2].position;
        _controlTarget[2].forward = targetDir;
        _bodyRotaion = _controlTarget[0].localRotation;
        _rArmRotaion2 = _controlTarget[1].localRotation * Quaternion.Euler(-90, 0, 0);
        _lArmRotaion2 = _controlTarget[2].localRotation * Quaternion.Euler(-90, 0, 0);
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
    public void HandAttackRight()
    {
        ResetAngle();
        _action = true;
        if (_attackCount == 0)
        {
            _attackCount++;
            if (_groundCheck.IsGrounded())
            {
                //ChangeAnimation("attackSwingDArm");
                ChangeAnimation("attackKnuckleRArm");
                _leg?.AttackMoveR();
            }
            else
            {
                //ChangeAnimation("attackSwingDArm3", 0.5f);
                ChangeAnimation("attackKnuckleRArm");
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
                //ChangeAnimation("attackSwingDArm2");
                ChangeAnimation("attackKnuckleLArm");
                _leg?.AttackMoveL();
            }
            else if (_attackCount == 2)
            {
                //ChangeAnimation("attackSwingDArm3", 0.5f);
                ChangeAnimation("attackKnuckleRArm");
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
        _bodyControlBase[1].localRotation = Quaternion.Lerp(_bodyControlBase[1].localRotation, _headRotaion, _headRSpeed * Time.deltaTime);
        _bodyControlBase[0].localRotation = Quaternion.Lerp(_bodyControlBase[0].localRotation, _bodyRotaion, _bodyRSpeed * Time.deltaTime);
        _leftControlBase[0].localRotation = Quaternion.Lerp(_leftControlBase[0].localRotation, _lArmRotaion, _lArmRSpeed * Time.deltaTime);
        _leftControlBase[2].localRotation = Quaternion.Lerp(_leftControlBase[2].localRotation, _lArmRotaion2, _lArmRSpeed2 * Time.deltaTime);
        _rightControlBase[0].localRotation = Quaternion.Lerp(_rightControlBase[0].localRotation, _rArmRotaion, _rArmRSpeed * Time.deltaTime);
        _rightControlBase[2].localRotation = Quaternion.Lerp(_rightControlBase[2].localRotation, _rArmRotaion2, _rArmRSpeed2 * Time.deltaTime);
    }
}
