using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    MachineController _controller = default;
    [SerializeField]
    MachineBuildControl _buildControl = default;
    void Start()
    {
        _buildControl.SetData(GameManager.Instance.CurrentBuildData);
        _controller.StartSet();
        GameScene.InputManager.Instance.OnInputAxisRaw += _controller.Move;
        GameScene.InputManager.Instance.OnInputAxisRawExit += _controller.MoveEnd;
        GameScene.InputManager.Instance.OnFirstInputJump += _controller.Jump;
        GameScene.InputManager.Instance.OnInputJump += _controller.Boost;
        GameScene.InputManager.Instance.OnFirstInputBooster += _controller.JetStart;
        GameScene.InputManager.Instance.OnInputLockOn += _controller.SetTarget;
        GameScene.InputManager.Instance.OnFirstInputAttack += _controller.BodyControl.FightingAttack;
        GameScene.InputManager.Instance.OnFirstInputShotL += _controller.BodyControl.HandAttackLeft;
        GameScene.InputManager.Instance.OnFirstInputShotR += _controller.BodyControl.HandAttackRight;
        GameScene.InputManager.Instance.OnFirstInputShot1 += _controller.BodyControl.ShoulderShot;
        GameScene.InputManager.Instance.OnFirstInputShot2 += _controller.BodyControl.BodyWeaponShot;
    }

    public void OutControl()
    {
        GameScene.InputManager.Instance.OnInputAxisRaw -= _controller.Move;
        GameScene.InputManager.Instance.OnInputAxisRawExit -= _controller.MoveEnd;
        GameScene.InputManager.Instance.OnFirstInputJump -= _controller.Jump;
        GameScene.InputManager.Instance.OnInputJump -= _controller.Boost;
        GameScene.InputManager.Instance.OnFirstInputBooster -= _controller.JetStart;
        GameScene.InputManager.Instance.OnInputLockOn -= _controller.SetTarget;
        GameScene.InputManager.Instance.OnFirstInputAttack -= _controller.BodyControl.FightingAttack;
        GameScene.InputManager.Instance.OnFirstInputShotL -= _controller.BodyControl.HandAttackLeft;
        GameScene.InputManager.Instance.OnFirstInputShotR -= _controller.BodyControl.HandAttackRight;
        GameScene.InputManager.Instance.OnFirstInputShot1 -= _controller.BodyControl.ShoulderShot;
        GameScene.InputManager.Instance.OnFirstInputShot2 -= _controller.BodyControl.BodyWeaponShot;
    }
}
