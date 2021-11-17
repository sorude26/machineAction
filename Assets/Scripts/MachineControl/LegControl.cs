using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegControl : MonoBehaviour
{
    [SerializeField]
    Animator _animator = default;
    int _walk = default;
    int _turn = default;
    bool _jump = default;
    bool _isGround = false;
    bool _landing = default;
    bool _float = default;
    float _landingTime = 0.5f;
    float _landingTimer = 0;
    MachineController _machine = default;
    public void Set(MachineController controller)
    {
        _machine = controller;
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
    public void WalkStart(int angle)
    {
        if (_jump)
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
            ChangeAnimation("Walk", 0.5f);
        }
        else if(angle < 0)
        {
            ChangeAnimation("WalkBack", 0.5f);
        }
        else
        {
            WalkStop();
        }
    }
    public void WalkStop()
    {
        if (_jump)
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
    }
    public void TurnStartLeft()
    {
        if (_jump)
        {
            return;
        }
        if (_turn < 0)
        {
            return;
        }
        _turn = -1;
        if (_walk != 0)
        {
            return;
        }
        ChangeAnimation("TurnLeft");
    }
    public void TurnStartRight()
    {
        if (_jump)
        {
            return;
        }
        if (_turn > 0)
        {
            return;
        }
        _turn = 1;
        if (_walk != 0)
        {
            return;
        }
        ChangeAnimation("TurnRight");
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
        if (_jump || _float)
        {
            return;
        }
        _jump = true;
        _isGround = false;
        StartCoroutine(JumpFall());
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
        if (_landing)
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
            if (_machine.InputAxis.y <= 0)
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
            if (_machine.InputAxis.y < 0)
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
    IEnumerator JumpFall()
    {
        while (!_isGround)
        {
            yield return null;
        }
        _machine?.Landing();
        CameraController.Shake();
        ChangeAnimation("JunpEnd");
    }
    IEnumerator LandingWait()
    {
        _landingTimer = 0;
        while (_landingTimer < _landingTime)
        {
            _landingTimer += Time.deltaTime;
            yield return null;
        }
        _animator.Play("LandingEnd");
        _jump = false;
        _walk = 0;
        _turn = 0;
        _landing = false;
    }

    void Walk()
    {
        _machine?.Walk(_walk);
        if (_turn > 0)
        {
            TurnRight();
        }
        else if (_turn < 0)
        {
            TurnLeft();
        }
        _turn = 0;
    }
    void AttackMove()
    {
        _machine?.Walk(1);
    }
    void AttackMoveStrong()
    {
        _machine?.Walk(2);
    }
    void Shake()
    {
        CameraController.LightShake();
    }
    void TurnLeft()
    {
        _machine?.Turn(-1);
    }
    void TurnRight()
    {
        _machine?.Turn(1);
    }
    void Jump()
    {
        Vector3 dir = transform.forward * _walk + transform.right * _turn;
        _machine?.StartJump((Vector3.up + dir).normalized);
    }
    void Landing()
    {
        if (_landing || _float)
        {
            return;
        }
        _landing = true;
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
        _isGround = _machine.IsGrounded();
    }
    void ChangeAnimation(string changeTarget,float changeTime = 0.2f) 
    { 
        _animator.CrossFadeInFixedTime(changeTarget, changeTime);
    }
    
    public void AttackMoveR()
    {
        if (_jump || _float || _landing)
        {
            return;
        }
        ChangeAnimation("AttackR1");
    }
    public void AttackMoveL()
    {
        if (_jump || _float || _landing)
        {
            return;
        }
        ChangeAnimation("AttackL1");
    }
}
