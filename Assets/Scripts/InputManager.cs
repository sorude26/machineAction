using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameScene
{
    /// <summary>
    /// 全入力を管理する
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }
        public event Action<float, float> OnInputAxisRaw;
        public event Action<Vector2> OnInputCameraRaw;
        public event Action OnInputAxisRawExit;
        public event Action OnInputCameraRawExit;
        public event Action OnFirstInputJump;
        public event Action OnInputJump;
        public event Action OnInputJumpEnd;
        public event Action OnFirstInputBooster;
        public event Action OnFirstInputShot;
        public event Action OnFirstInputShotL;
        public event Action OnFirstInputShotR;
        public event Action OnFirstInputAttack;
        public event Action OnShotEnd;
        bool _inputMove;
        bool _inputCamera;
        bool _firstInputJump;
        bool _firstInputAxisRaw;
        bool _shot1;
        bool _shotL;
        bool _shotR;
        bool _attack;
        ControlAction _inputActions;
        private void Awake()
        {
            Instance = this;
            InputActionsSet();
        }
        void InputActionsSet()
        {
            _inputActions = new ControlAction();
            _inputActions.Enable();
            _inputActions.PlayerController.Move.performed += context => { StartMove(context); };
            _inputActions.PlayerController.Move.canceled += context => { EndMove(); };
            _inputActions.PlayerController.CameraMove.performed += context => { StartCamera(context); };
            _inputActions.PlayerController.CameraMove.canceled += context => { EndCamera(); };
            _inputActions.PlayerController.Jump.started += context => { OnJump(); };
            _inputActions.PlayerController.Jump.canceled += context => { EndJump(); };
            _inputActions.PlayerController.Shot1.started += context => { OnShot(); };
            _inputActions.PlayerController.ShotL.started += context => { OnShotL(); };
            _inputActions.PlayerController.ShotR.started += context => { OnShotR(); };
            _inputActions.PlayerController.Attack1.started += context => { OnAttack(); };
            _inputActions.PlayerController.Shot1.canceled += context => { EndShot(); };
            _inputActions.PlayerController.ShotL.canceled += context => { EndShotL(); };
            _inputActions.PlayerController.ShotR.canceled += context => { EndShotR(); };
            _inputActions.PlayerController.Attack1.canceled += context => { EndAttack(); };
            _inputActions.PlayerController.Jet.started += context => { OnJet(); };
            StartCoroutine(ActionUpdate());
        }
        void OnMove(Vector2 dir)
        {
            float h = dir.x;
            float v = dir.y;
            if (new Vector2(h, v) != Vector2.zero)
            {
                if (Math.Abs(v) < 0.3f)
                {
                    v = 0;
                }
                if (Math.Abs(h) < 0.3f)
                {
                    h = 0;
                }
                OnInputAxisRaw?.Invoke(h, v);
                if (!_firstInputAxisRaw)
                {
                    _firstInputAxisRaw = true;
                }
            }            
        }
        void EndMove()
        {
            if (_firstInputAxisRaw)
            {
                _firstInputAxisRaw = false;
                OnInputAxisRawExit?.Invoke();
            }
            _inputMove = false;
        }
        void EndCamera()
        {
            _inputCamera = false;
            OnInputCameraRawExit?.Invoke();
        }
        void OnMove(InputAction.CallbackContext context)
        {
            OnMove(context.ReadValue<Vector2>());
        }
        void OnCamera(InputAction.CallbackContext context)
        {
            OnInputCameraRaw?.Invoke(context.ReadValue<Vector2>());
        }
        void StartMove(InputAction.CallbackContext context)
        {
            if (!_inputMove)
            {
                _inputMove = true;
                StartCoroutine(Move(context));
            }
        }
        void StartCamera(InputAction.CallbackContext context)
        {
            if (!_inputCamera)
            {
                _inputCamera = true;
                StartCoroutine(Camera(context));
            }
        }
        IEnumerator Move(InputAction.CallbackContext context)
        {           
            while (_inputMove)
            {
                OnMove(context);
                yield return null;
            }
        }
        IEnumerator Camera(InputAction.CallbackContext context)
        {
            while (_inputCamera)
            {
                OnCamera(context);
                yield return null;
            }
        }
        void OnJump()
        {
            if (!_firstInputJump)
            {
                _firstInputJump = true;
                OnFirstInputJump?.Invoke();
            }
           
        }
        void EndJump()
        {
            _firstInputJump = false;
            OnInputJumpEnd?.Invoke();
        }
        void OnShot()
        {
            if (!_shot1)
            {
                _shot1 = true;
                //StartCoroutine(Shot());
            }
        }        
        void OnShotL()
        {
            if (!_shotL)
            {
                _shotL = true;
                //StartCoroutine(ShotL());
            }
        }
        void OnShotR()
        {
            if (!_shotR)
            {
                _shotR = true;
                //StartCoroutine(ShotR());
            }
        }
        void OnAttack()
        {
            if (!_attack)
            {
                _attack = true;
                OnFirstInputAttack?.Invoke();
            }
        }
        void OnJet()
        {
            OnFirstInputBooster?.Invoke();
        }
        void EndShot()
        {
            _shot1 = false;
            OnShotEnd?.Invoke();
        }
        void EndShotL()
        {
            _shotL = false;
            OnShotEnd?.Invoke();
        }
        void EndShotR()
        {
            _shotR = false;
            OnShotEnd?.Invoke();
        }
        void EndAttack()
        {
            _attack = false;
        }
        IEnumerator ActionUpdate()
        {
            while (true)
            {
                if (_shot1)
                {
                    OnFirstInputShot?.Invoke();
                }
                if (_shotL)
                {
                    OnFirstInputShotL?.Invoke();
                }
                if (_shotR)
                {
                    OnFirstInputShotR?.Invoke();
                }
                if (_attack)
                {
                    //OnFirstInputAttack?.Invoke();
                }
                if (_firstInputJump)
                {
                    OnInputJump?.Invoke();
                }
                yield return null;
            }
        }
       
    }
}
