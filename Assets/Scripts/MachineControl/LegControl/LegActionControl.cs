using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LegControl
{
    class LegActionControl
    {
        LegControl _legControl = default;
        IStateBase<LegControl> _currntState = default;
        public LegActionControl(LegControl control , IStateBase<LegControl> startState)
        {
            _legControl = control;
            _currntState = startState;
        }
        public void Update()
        {
            _currntState.OnUpdate(_legControl);
        }
        public bool ChengeState(IStateBase<LegControl> next)
        {
            if (_currntState == next)
            {
                return false;
            }
            if (_currntState.OnLeave(_legControl, next))
            {
                _currntState = next;
                _currntState.OnEnter(_legControl);
                return true;
            }
            return false;
        }
    }
}