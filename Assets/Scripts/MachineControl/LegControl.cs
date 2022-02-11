using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegControl : MonoBehaviour
{
    [SerializeField]
    Animator _animator = default;
    [SerializeField]
    ParticleSystem _walkEffect = default;
    [SerializeField]
    ParticleSystem _landingEffect = default;
    int _walk = default;
    int _turn = default;
    bool _jump = false;
    bool _jumpEnd = false;
    bool _isGround = false;
    bool _landing = false;
    bool _float = false;
    bool _knockDown = false;
    float _landingTime = 0.5f;
    float _landingTimer = 0;
    MachineController _machine = default;
    MoveAnimation _moveAnimation = default;
    LegType _legType = default;
    public void Set(MachineController controller)
    {
        _machine = controller;
        _legType = controller.MachineParts.Leg.Type;
        if (_legType == LegType.Animation)
        {
            _moveAnimation = controller.MachineParts.Leg.MoveAnimation;
            _moveAnimation.Set(controller);
        }
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
    public void WalkStart(int angle)
    {
        if (_jump || _knockDown)
        {
            return;
        }
        if (_walk == angle)
        {
            return;
        }
        _walk = angle;
        if (angle > 0)
        {
            switch (_legType)
            {
                case LegType.Normal:
                    if (_machine.Parameter.ActionSpeed > 1)
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
                    _moveAnimation.WalkStart(angle);
                    break;
                default:
                    break;
            }
        }
        else if (angle < 0)
        {
            switch (_legType)
            {
                case LegType.Normal:
                    ChangeAnimation("WalkBack", 0.5f);
                    break;
                case LegType.Crawler:
                    ChangeAnimation("MoveFront", 0.1f);
                    break;
                case LegType.Animation:
                    _moveAnimation.WalkStart(angle);
                    break;
                default:
                    break;
            }
        }
        else
        {
            WalkStop();
        }
    }
    public void WalkStop()
    {
        if (_jump || _knockDown)
        {
            return;
        }
        if (_walk == 0 && _turn == 0)
        {
            return;
        }
        _walk = 0;
        _turn = 0;
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
        if (_turn < 0)
        {
            if (_legType == LegType.Animation)
            {
                _moveAnimation.SetTurn(_turn);
            }
            return;
        }
        _turn = -1;        
        if (_walk != 0)
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
        if (_turn > 0)
        {
            if (_legType == LegType.Animation)
            {
                _moveAnimation.SetTurn(_turn);
            }
            return;
        }
        _turn = 1;
        if (_walk != 0)
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
    public void ChangeMode()
    {
        if (_float)
        {
            _float = false;
        }
        else
        {
            _float = true;
        }
        _jump = false;
        _walk = 0;
        _turn = 0;
        _landing = false;
        _animator.SetBool("Float", _float);
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
        _isGround = false;
        if (_legType == LegType.Animation)
        {
            _moveAnimation.StartJump();
            return;
        }
        if (_walk != 0)
        {
            ChangeAnimation("JunpStart");
            return;
        }
        if (_turn > 0)
        {
            ChangeAnimation("JunpStartR");
        }
        else if (_turn < 0)
        {

            ChangeAnimation("JunpStartL");
        }
        else
        {
            ChangeAnimation("JunpStart");
        }
    }
    public void StartJet()
    {
        if (_legType == LegType.Crawler)
        {
            return;
        }
        if (_landing || _jumpEnd || _knockDown)
        {
            return;
        }
        if (_machine.InputAxis == Vector3.zero)
        {
            return;
        }
        if (_jump)
        {
            if (_isGround)
            {
                return;
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
        _walk = 0;
        _turn = 0;
        _landing = false;
        _jumpEnd = false;
    }

    void Walk()
    {
        _machine?.Walk(_walk);
        if (_turn > 0)
        {
            _machine.Turn(0.2f);
        }
        else if (_turn < 0)
        {
            _machine?.Turn(-0.2f);
        }
        _turn = 0;
    }
    void Run()
    {
        _machine.Run(_walk);
        if (_turn > 0)
        {
            _machine.Turn(0.1f);
        }
        else if (_turn < 0)
        {
            _machine.Turn(-0.1f);
        }
        _turn = 0;
    }
    void Move()
    {
        _machine.Move(_walk);
        if (_turn > 0)
        {
            _machine.Turn(0.05f);
        }
        else if (_turn < 0)
        {
            _machine.Turn(-0.05f);
        }
        _turn = 0;
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
    void TurnLeft()
    {
        _machine?.Turn(-1f);
    }
    void TurnRight()
    {
        _machine?.Turn(1f);
    }
    void Jump()
    {
        Vector3 dir = transform.forward * _walk + transform.right * _turn;
        _machine?.StartJump((Vector3.up + dir).normalized);
    }
    void Landing()
    {
        if (_landing || _float || _knockDown)
        {
            return;
        }
        _landing = true;
        if (_landingEffect)
        {
            _landingEffect.Play();
        }
        StartCoroutine(LandingWait());
    }
    void Jet()
    {
        _machine?.Jet();
    }
    void Stop()
    {
        _machine?.Stop();
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
        _isGround = _machine.IsGrounded();
        if (_isGround && _jump && !_jumpEnd)
        {
            _jumpEnd = true;
            _machine?.Landing();
            CameraEffectManager.Shake(transform.position);
            ChangeAnimation("JunpEnd");
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
