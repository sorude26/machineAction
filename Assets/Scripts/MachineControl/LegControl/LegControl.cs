﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveState
{
    Back = -1,
    Stop = 0,
    Forward = 1,
}
public enum LegState
{
    Idle,
    WalkForward,
    WalkBack,
    Ran,
    Float,
    JumpStart,
    Fly,
    FlyEnd,
    Landing,
    FlyJet,
    GroundJet,
}
public partial class LegControl : MonoBehaviour
{
    public enum LegDirection
    {
        Left = -1,
        Flont = 0,
        Right = 1,
    }
    [SerializeField]
    Animator _animator = default;
    [SerializeField]
    ParticleSystem _walkEffect = default;
    [SerializeField]
    ParticleSystem _landingEffect = default;
    MoveState _move = default;
    LegDirection _direction = default;
    bool _set = false;
    bool _jump = false;
    bool _jumpEnd = false;
    bool _landing = false;
    bool _float = false;
    bool _knockDown = false;
    float _landingTime = 0.5f;
    float _landingTimer = 0;
    MachineController _machine = default;
    MoveAnimation _moveAnimation = default;
    LegActionControl _actionControl = default;
    Idle _stateIdle = new Idle();
    JumpState _stateJump = new JumpState();
    GroundMoveState _stateMove = new GroundMoveState();
    ActionWaitState _stateWait = new ActionWaitState();
    LegType _legType = default;
    public bool IsLanding { get => _landing; }
    bool IsGround { get => _machine.IsGrounded(); }
    private void Awake()
    {
        if (_animator)
        {
            _animator.enabled = false;
        }
        _actionControl = new LegActionControl(this, _stateIdle);
    }
    private void Update()
    {
        if (!_set) { return; }
        _actionControl?.Update();
    }
    public void Set(MachineController controller)
    {
        _machine = controller;
        _legType = controller.MachineParts.Leg.Type;
        if (_legType == LegType.Animation)
        {
            _moveAnimation = controller.MachineParts.Leg.MoveAnimation;
            _moveAnimation.Set(controller);
        }
        if (_animator)
        {
            _animator.enabled = true;
        }
        _set = true;
    }
    public void SetLandingTime(float time)
    {
        _landingTime = time;
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
        switch (_legType)
        {
            case LegType.Normal:
                ChangeAnimation("KnockDownF");
                break;
            case LegType.Crawler:
                ChangeAnimation("Idle", 0.1f);
                break;
            case LegType.Animation:
                _moveAnimation.KnockDown();
                break;
            default:
                break;
        }
    }
    public void WalkStart(MoveState angle)
    {
        if (_jump || _knockDown)
        {
            return;
        }
        if (_move == angle)
        {
            return;
        }
        _move = angle;
        _actionControl.ChengeState(_stateMove);
        switch (angle)
        {
            case MoveState.Stop:
                WalkStop();
                break;
            case MoveState.Forward:
                switch (_legType)
                {
                    case LegType.Normal:
                        if (_machine.Parameter.ActionSpeed > 1.1f)
                        {
                            ChangeAnimation("Run");
                        }
                        else
                        {
                            ChangeAnimation("Walk", 0.5f);
                        }
                        break;
                    case LegType.Crawler:
                        ChangeAnimation("MoveFront", 0.1f);
                        break;
                    case LegType.Animation:
                        _moveAnimation.WalkStart((int)angle);
                        break;
                    default:
                        break;
                }
                break;
            case MoveState.Back:
                switch (_legType)
                {
                    case LegType.Normal:
                        ChangeAnimation("WalkBack", 0.5f);
                        break;
                    case LegType.Crawler:
                        ChangeAnimation("MoveFront", 0.1f);
                        break;
                    case LegType.Animation:
                        _moveAnimation.WalkStart((int)angle);
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }
    public void WalkStop()
    {
        if (_jump || _knockDown)
        {
            return;
        }
        if (_move == MoveState.Stop && _direction == LegDirection.Flont)
        {
            return;
        }
        _move = MoveState.Stop;
        _direction = LegDirection.Flont;
        _actionControl.ChengeState(_stateIdle);
        ChangeAnimation("Idle", 0.5f);
        switch (_legType)
        {
            case LegType.Normal:
                break;
            case LegType.Crawler:
                break;
            case LegType.Animation:
                _moveAnimation.WalkStop();
                break;
            default:
                break;
        }
    }
    public void TurnStartLeft()
    {
        if (_jump || _knockDown)
        {
            return;
        }       
        if (_direction == LegDirection.Left)
        {
            if (_legType == LegType.Animation)
            {
                _moveAnimation.SetTurn((int)_direction);
            }
            return;
        }
        _direction = LegDirection.Left;        
        if (_move != MoveState.Stop)
        {
            return;
        }       
        switch (_legType)
        {
            case LegType.Normal:
                ChangeAnimation("TurnLeft");
                break;
            case LegType.Crawler:
                ChangeAnimation("MoveLeft", 0.1f);
                break;
            case LegType.Animation:
                _moveAnimation.TurnStartLeft();
                break;
            default:
                break;
        }
    }
    public void TurnStartRight()
    {
        if (_jump || _knockDown)
        {
            return;
        } 
        if (_direction == LegDirection.Right)
        {
            if (_legType == LegType.Animation)
            {
                _moveAnimation.SetTurn((int)_direction);
            }
            return;
        }
        _direction = LegDirection.Right;
        if (_move != MoveState.Stop)
        {
            return;
        }
        switch (_legType)
        {
            case LegType.Normal:
                ChangeAnimation("TurnRight");
                break;
            case LegType.Crawler:
                ChangeAnimation("MoveRight", 0.1f);
                break;
            case LegType.Animation:
                _moveAnimation.TurnStartRight();
                break;
            default:
                break;
        }
    }
    public void ChangeMode(bool floatMode)
    {
        _float = floatMode;
        _animator.SetBool("Float", _float);
        if (_jump)
        {
            _jumpEnd = false;
            ChangeAnimation("Junp");
        }
    }
    public void StartJump()
    {
        if (_legType != LegType.Normal)
        {
            return;
        }
        if (_jump || _float || _knockDown)
        {
            return;
        }
        _jump = true;
        _jumpEnd = false;
        if (_legType == LegType.Animation)
        {
            _moveAnimation.StartJump();
            return;
        }
        if (_move != MoveState.Stop)
        {
            ChangeAnimation("JunpStart");
            return;
        }
        if (_direction == LegDirection.Right)
        {
            ChangeAnimation("JunpStartR");
        }
        else if (_direction == LegDirection.Left)
        {

            ChangeAnimation("JunpStartL");
        }
        else
        {
            ChangeAnimation("JunpStart");
        }
    }
    public bool StartJet()
    {
        if (_legType == LegType.Crawler)
        {
            return false;
        }
        if (_landing || _knockDown)
        {
            return false;
        }
        if (_machine.InputAxis == Vector3.zero)
        {
            return false;
        }
        if (_jump)
        {
            if (IsGround)
            {
                return false;
            }
            if (_machine.InputAxis.z > 0)
            {
                if (_machine.InputAxis.x > 0)
                {
                    ChangeAnimation("JetMoveFlyR", 0.01f);
                }
                else if (_machine.InputAxis.x < 0)
                {
                    ChangeAnimation("JetMoveFlyL", 0.01f);
                }
                else
                {
                    ChangeAnimation("JetMoveFly", 0.01f);
                }
            }
            else
            {
                if (_machine.InputAxis.x > 0)
                {
                    ChangeAnimation("JetMoveFlyR", 0.01f);
                }
                else if (_machine.InputAxis.x < 0)
                {
                    ChangeAnimation("JetMoveFlyL", 0.01f);
                }
                else
                {
                    ChangeAnimation("JetMoveFlyB", 0.01f);
                }
            }
        }
        else
        {
            if (_machine.InputAxis.z < 0)
            {
                ChangeAnimation("JetMoveB", 0.01f);
            }
            else
            {
                if (_machine.InputAxis.x > 0)
                {
                    ChangeAnimation("JetMoveR", 0.01f);
                }
                else if (_machine.InputAxis.x < 0)
                {
                    ChangeAnimation("JetMoveL", 0.01f);
                }
                else
                {
                    ChangeAnimation("JetMove", 0.01f);
                }
            }
        }
        return true;
    }
    IEnumerator LandingWait()
    {
        _landingTimer = 0;
        while (_landingTimer < _landingTime)
        {
            _landingTimer += Time.deltaTime;
            yield return null;
        }
        if (!_knockDown)
        {
            _animator.Play("LandingEnd");
        }
        _jump = false;
        _move = MoveState.Stop;
        _direction = LegDirection.Flont;
        _landing = false;
        _jumpEnd = false;
    }

    void Walk()
    {
        _machine?.Walk((int)_move);
        TurnCheck(MachineStatus.TurnPower);
    }
    void Run()
    {
        _machine.Run((int)_move);
        TurnCheck(MachineStatus.TurnPower);
    }
    void Move()
    {
        _machine.Move((int)_move);
        TurnCheck(MachineStatus.TurnMovePower);
    }
    void TurnCheck(float power)
    {
        if (_direction == LegDirection.Right)
        {
            _machine.Turn(power);
        }
        else if (_direction == LegDirection.Left)
        {
            _machine.Turn(-power);
        }
        _direction = LegDirection.Flont;
    }
    void AttackMove()
    {
        _machine?.Walk(1);
        _machine?.Turn();
    }
    void AttackMoveStrong()
    {
        _machine?.Walk(2);
        _machine?.Turn();
    }
    void Shake()
    {
        CameraEffectManager.LightShake(transform.position);       
        if (_walkEffect)
        {
            _walkEffect.Play();
        }
    }
    void SmokeEffect()
    {
        if (_landingEffect && _machine.IsGrounded())
        {
            _landingEffect.Play();
        }
    }
    void TurnLeft()
    {
        _machine?.Turn(-MachineStatus.Turn);
    }
    void TurnRight()
    {
        _machine?.Turn(MachineStatus.Turn);
    }
    void Jump()
    {
        SmokeEffect();
        Vector3 dir = transform.forward * (int)_move + transform.right * (int)_direction;
        _machine?.StartJump((Vector3.up + dir).normalized);
    }
    void Landing()
    {
        if (_landing || _float || _knockDown)
        {
            return;
        }
        _landing = true;
        //StartCoroutine(LandingWait());
    }
    void Jet()
    {
        _machine?.Jet();
    }
    void Stop()
    {
        _machine?.ActionStop();
    }
    void Brake()
    {
        _machine?.Brake();
    }
    void GroundCheck()
    {
        if (_knockDown)
        {
            return;
        }
        if (!_jumpEnd)
        {
            _jumpEnd = true;
        }
    }
    void ChangeAnimation(string changeTarget, float changeTime = 0.2f)
    {
        _animator.CrossFadeInFixedTime(changeTarget, changeTime);
    }

    public void AttackMoveR()
    {
        if (_legType == LegType.Crawler)
        {
            return;
        }
        if (_jump || _float || _landing || _knockDown)
        {
            return;
        }
        ChangeAnimation("AttackR1");
    }
    public void AttackMoveL()
    {
        if (_legType == LegType.Crawler)
        {
            return;
        }
        if (_jump || _float || _landing || _knockDown)
        {
            return;
        }
        ChangeAnimation("AttackL1");
    }
}
