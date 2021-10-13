using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        bool m_firstInputJump;
        bool m_firstInputAxisRaw;
        private void Awake()
        {
            Instance = this;
        }
        void Update()
        {
            if (Input.GetButton("Jump"))
            {
                if (!m_firstInputJump)
                {
                    m_firstInputJump = true;
                    OnFirstInputJump?.Invoke();
                    return;
                }
                OnInputJump?.Invoke();
            }
            else if (m_firstInputJump)
            {
                m_firstInputJump = false;
            }
            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                if (!m_firstInputAxisRaw)
                {
                    m_firstInputAxisRaw = true;
                }
                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");
                if (new Vector2(h, v).sqrMagnitude > 0.1f)
                {
                    OnInputAxisRaw?.Invoke(h, v);                    
                }
            }
            else if (m_firstInputAxisRaw)
            {
                m_firstInputAxisRaw = false;
                OnInputAxisRawExit?.Invoke();
            }
            if (Input.GetButtonDown("Fire3"))
            {
                OnFirstInputBooster?.Invoke();
            }
        }
    }
}
