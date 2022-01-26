using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimation : MonoBehaviour
{
    [SerializeField]
    Animator _animator = default;
    MachineController _machine = default;
    int _walk = default;
    int _turn = default;
    public void Set(MachineController controller)
    {
        _machine = controller;
    }
    public void KnockDown()
    {
        ChangeAnimation("Destroy");
    }
    public void WalkStart(int angle)
    {
        _walk = angle;
        ChangeAnimation("Walk");
    }
    public void WalkStop()
    {
        _walk = 0;
        _turn = 0;
        ChangeAnimation("Wait");
    }
    public void TurnStartLeft()
    {
        _turn = -1;
        ChangeAnimation("TurnLeft");
    }
    public void TurnStartRight()
    {
        _turn = 1;
        ChangeAnimation("TurnRight");
    }
    public void ChangeMode()
    {
        
    }
    public void StartJump()
    {
        
    }
    public void StartJet()
    {
       
    }

    void Walk()
    {
        _machine.Walk(_walk);
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
    void Move()
    {
        
    }
    void AttackMove()
    {

    }
    void AttackMoveStrong()
    {

    }
    void Shake()
    {
        CameraEffectManager.LightShake(transform.position);
    }
    void TurnLeft()
    {
        _machine.Turn(-1f);
    }
    void TurnRight()
    {
        _machine.Turn(1f);
    }
    void Jump()
    {

    }
    void Landing()
    {

    }
    void Jet()
    {

    }
    void Stop()
    {

    }
    void Brake()
    {

    }
    void GroundCheck()
    {

    }
  
    public void AttackMoveR()
    {
       
    }
    public void AttackMoveL()
    {

    }
    void ChangeAnimation(string changeTarget, float changeTime = 0.2f)
    {
        _animator.CrossFadeInFixedTime(changeTarget, changeTime);
    }
}
