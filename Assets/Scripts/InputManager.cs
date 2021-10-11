﻿using System;
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
        public event Action OnFirstInputJump;
        public event Action OnInputJump;
        bool m_firstInputJump;
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
                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");
                if (new Vector2(h, v).sqrMagnitude > 0.1f)
                {
                    OnInputAxisRaw?.Invoke(h, v);                    
                }
            }
        }
    }
}
