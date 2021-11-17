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
        public event Action OnFirstInputJump;
        public event Action OnInputJump;
        public event Action OnInputJumpEnd;
        public event Action OnFirstInputBooster;
        public event Action OnFirstInputShot;
        public event Action OnFirstInputShotL;
        public event Action OnFirstInputShotR;
        public event Action OnFirstInputAttack;
        public event Action OnShotEnd;
        bool m_inputMove;
        bool m_inputCamera;
        bool m_firstInputJump;
        bool m_firstInputAxisRaw;
        bool m_shot1;
        bool m_shotL;
        bool m_shotR;
        bool m_attack;
        ControlAction m_inputActions;
        private void Awake()
        {
            Instance = this;
            InputActionsSet();
        }
        void InputActionsSet()
        {
            m_inputActions = new ControlAction();
            m_inputActions.Enable();
            m_inputActions.PlayerController.Move.performed += context => { StartMove(context); };
            m_inputActions.PlayerController.Move.canceled += context => { EndMove(); };
            m_inputActions.PlayerController.CameraMove.performed += context => { StartCamera(context); };
            m_inputActions.PlayerController.CameraMove.canceled += context => { EndCamera(); };
            m_inputActions.PlayerController.Jump.started += context => { OnJump(); };
            m_inputActions.PlayerController.Jump.canceled += context => { EndJump(); };
            m_inputActions.PlayerController.Shot1.started += context => { OnShot(); };
            m_inputActions.PlayerController.ShotL.started += context => { OnShotL(); };
            m_inputActions.PlayerController.ShotR.started += context => { OnShotR(); };
            m_inputActions.PlayerController.Attack1.started += context => { OnAttack(); };
            m_inputActions.PlayerController.Shot1.canceled += context => { EndShot(); };
            m_inputActions.PlayerController.ShotL.canceled += context => { EndShotL(); };
            m_inputActions.PlayerController.ShotR.canceled += context => { EndShotR(); };
            m_inputActions.PlayerController.Attack1.canceled += context => { EndAttack(); };
            m_inputActions.PlayerController.Jet.started += context => { OnJet(); };
            StartCoroutine(ActionUpdate());
        }
        void OnMove(Vector2 dir)
        {
            float h = dir.x;
            float v = dir.y;
            if (new Vector2(h, v) != Vector2.zero)
            {
                if (Math.Abs(v) < 0.2f)
                {
                    v = 0;
                }
                if (Math.Abs(h) < 0.2f)
                {
                    h = 0;
                }
                OnInputAxisRaw?.Invoke(h, v);
                if (!m_firstInputAxisRaw)
                {
                    m_firstInputAxisRaw = true;
                }
            }            
        }
        void EndMove()
        {
            if (m_firstInputAxisRaw)
            {
                m_firstInputAxisRaw = false;
                OnInputAxisRawExit?.Invoke();
            }
            m_inputMove = false;
        }
        void EndCamera()
        {
            m_inputCamera = false;
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
            if (!m_inputMove)
            {
                m_inputMove = true;
                StartCoroutine(Move(context));
            }
        }
        void StartCamera(InputAction.CallbackContext context)
        {
            if (!m_inputCamera)
            {
                m_inputCamera = true;
                StartCoroutine(Camera(context));
            }
        }
        IEnumerator Move(InputAction.CallbackContext context)
        {           
            while (m_inputMove)
            {
                OnMove(context);
                yield return null;
            }
        }
        IEnumerator Camera(InputAction.CallbackContext context)
        {
            while (m_inputCamera)
            {
                OnCamera(context);
                yield return null;
            }
        }
        void OnJump()
        {
            if (!m_firstInputJump)
            {
                m_firstInputJump = true;
                OnFirstInputJump?.Invoke();
            }
           
        }
        void EndJump()
        {
            m_firstInputJump = false;
            OnInputJumpEnd?.Invoke();
        }
        void OnShot()
        {
            if (!m_shot1)
            {
                m_shot1 = true;
                //StartCoroutine(Shot());
            }
        }        
        void OnShotL()
        {
            if (!m_shotL)
            {
                m_shotL = true;
                //StartCoroutine(ShotL());
            }
        }
        void OnShotR()
        {
            if (!m_shotR)
            {
                m_shotR = true;
                //StartCoroutine(ShotR());
            }
        }
        void OnAttack()
        {
            if (!m_attack)
            {
                m_attack = true;
                OnFirstInputAttack?.Invoke();
            }
        }
        void OnJet()
        {
            OnFirstInputBooster?.Invoke();
        }
        void EndShot()
        {
            m_shot1 = false;
            OnShotEnd?.Invoke();
        }
        void EndShotL()
        {
            m_shotL = false;
            OnShotEnd?.Invoke();
        }
        void EndShotR()
        {
            m_shotR = false;
            OnShotEnd?.Invoke();
        }
        void EndAttack()
        {
            m_attack = false;
        }
        IEnumerator ActionUpdate()
        {
            while (true)
            {
                if (m_shot1)
                {
                    OnFirstInputShot?.Invoke();
                }
                if (m_shotL)
                {
                    OnFirstInputShotL?.Invoke();
                }
                if (m_shotR)
                {
                    OnFirstInputShotR?.Invoke();
                }
                if (m_attack)
                {
                    //OnFirstInputAttack?.Invoke();
                }
                if (m_firstInputJump)
                {
                    OnInputJump?.Invoke();
                }
                yield return null;
            }
        }
       
    }
}
