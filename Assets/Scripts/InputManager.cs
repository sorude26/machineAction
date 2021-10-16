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
        public event Action OnInputAxisRawExit;
        public event Action OnFirstInputJump;
        public event Action OnInputJump;
        public event Action OnFirstInputBooster;
        public event Action OnFirstInputShot;
        public event Action OnFirstInputShotL;
        public event Action OnFirstInputShotR;
        public event Action OnFirstInputAttack;
        bool m_inputMove;
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
        }
        void OnMove(Vector2 dir)
        {
            float h = dir.x;
            float v = dir.y;
            if (new Vector2(h, v) != Vector2.zero)
            {
                if (Math.Abs(v) < 0.02f)
                {
                    v = 0;
                }
                if (Math.Abs(h) < 0.02f)
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
        void OnMove(InputAction.CallbackContext context)
        {
            OnMove(context.ReadValue<Vector2>());
        }
        void StartMove(InputAction.CallbackContext context)
        {
            if (!m_inputMove)
            {
                m_inputMove = true;
                StartCoroutine(Move(context));
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
        void OnJump()
        {
            if (!m_firstInputJump)
            {
                m_firstInputJump = true;
                OnFirstInputJump?.Invoke();
            }
            OnInputJump?.Invoke();
        }
        void EndJump()
        {
            m_firstInputJump = false;
        }
        void OnShot()
        {
            if (!m_shot1)
            {
                m_shot1 = true;
                StartCoroutine(Shot());
            }
        }        
        void OnShotL()
        {
            if (!m_shotL)
            {
                m_shotL = true;
                StartCoroutine(ShotL());
            }
        }
        void OnShotR()
        {
            if (!m_shotR)
            {
                m_shotR = true;
                StartCoroutine(ShotR());
            }
        }
        void OnAttack()
        {
            if (!m_attack)
            {
                m_attack = true;
                StartCoroutine(Attack());
            }
        }
        void EndShot()
        {
            m_shot1 = false;
        }
        void EndShotL()
        {
            m_shotL = false;
        }
        void EndShotR()
        {
            m_shotR = false;
        }
        void EndAttack()
        {
            m_attack = false;
        }
        IEnumerator Shot()
        {
            while (m_shot1)
            {
                OnFirstInputShot?.Invoke();
                yield return new WaitForSeconds(0.05f);
            }
        }
        IEnumerator ShotL()
        {
            while (m_shotL)
            {
                OnFirstInputShotL?.Invoke();
                yield return new WaitForSeconds(0.05f);
            }
        }
        IEnumerator ShotR()
        {
            while (m_shotR)
            {
                OnFirstInputShotR?.Invoke();
                yield return new WaitForSeconds(0.05f);
            }
        }
        IEnumerator Attack()
        {
            while (m_attack)
            {
                OnFirstInputAttack?.Invoke();
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
